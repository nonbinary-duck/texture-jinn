using System;
using System.IO;
using System.Collections.Generic;


namespace TextureJinn.Extentions.StreamHacks
{
    /// <summary>
    /// An in-memory stream which can be manipulated
    /// Usefull for args which require a stream but you need to provide something malleable
    /// </summary>
    public class FakeStream : Stream
    {
        protected Memory<byte> m_Data;
        protected bool m_isWriting { get; set; }
        protected Queue<Action> m_WriteQue = new Queue<Action>();

        public override long Position { get; set; }
        public override long Length => m_Data.Length;
        public int iLength => m_Data.Length;
        public int iPosition { get => (int)Position; }
        public override bool CanRead { get => true; }
        public override bool CanSeek { get => true; }
        public override bool CanWrite { get => true; }

        public FakeStream() { }

        public FakeStream(byte[] data)
        {
            m_Data = data.AsMemory();
        }


        protected void CalculatePositions(int origin, int count, out int endPos, out int startPos, out int len)
        {
            // Allow for a negative count, no idea if filestream has something similar
            endPos = origin + count;
            startPos = Math.Min(endPos, origin);
            if (startPos < 0) throw new IndexOutOfRangeException("startPos on read is negative");
            endPos = Math.Max(endPos, origin);
            len = Math.Min(endPos - startPos, m_Data.Length - startPos);
        }

        public override void Flush()
        {
            m_Data = null;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            offset += iPosition;

            int endPos, startPos, len;
            CalculatePositions(offset, count, out endPos, out startPos, out len);
            if (startPos >= m_Data.Length) return 0;

            // buffer = m_Data.Slice(startPos, len).ToArray();
            byte[] nBuffer = m_Data.Slice(startPos, len).ToArray();

            for (int i = 0; i < len; i++)
            {
                buffer[i] = nBuffer[i];
            }

            Position += len;

            return len;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            int pos = (origin == SeekOrigin.Begin) ? 0 : ((origin == SeekOrigin.Current) ? iPosition : m_Data.Length - 1);

            long newPos = Math.Min(pos + offset, m_Data.Length);

            Position = (newPos < 0) ? Position : newPos;

            return Position;
        }

        public override void SetLength(long value)
        {
            int iValue = (int)value;

            if (m_Data.Length < iValue)
            {
                m_Data = m_Data.Slice(0, iValue);
            }
            else
            {
                m_Data = new Memory<byte>(m_Data.ToArray(), 0, iValue);
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            m_WriteQue.Enqueue(() =>
            {
                count = 131072;

                buffer = buffer.AsMemory().Slice(offset, count).ToArray();

                byte[] newData = new byte[m_Data.Length + count];

                m_Data.CopyTo(newData);
                buffer.AsMemory().CopyTo(newData);

                m_Data = newData;

                Position += count;
            });

            if (!m_isWriting)
            {
                m_isWriting = true;

                while (m_WriteQue.Count != 0)
                {
                    m_WriteQue.Dequeue().Invoke();
                }

                m_isWriting = false;
            }
        }
    }
}