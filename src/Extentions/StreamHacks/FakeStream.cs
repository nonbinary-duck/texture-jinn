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

        public byte[] Data { get => m_Data.ToArray(); }
        public override long Position { get; set; }
        public override long Length => m_Data.Length;
        public int iLength => m_Data.Length;
        public int iPosition { get => (int)Position; set => Position = value; }
        public override bool CanRead { get => true; }
        public override bool CanSeek { get => true; }
        public override bool CanWrite { get => true; }

        public FakeStream() { }

        public FakeStream(byte[] data)
        {
            m_Data = data.AsMemory();
        }

        public override void Flush()
        {
            m_Data = null;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            count = Math.Min(count, m_Data.Length - iPosition);

            byte[] accessed = m_Data.Slice(iPosition, count).ToArray();

            for (int i = 0; i < count; i++)
            {
                buffer[i + offset] = accessed[i];
            }

            Position += count;

            return count;
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
                byte[] newData = buffer.AsMemory().Slice(offset, count).ToArray();

                List<byte> newMData = new List<byte>();

                newMData.AddRange(m_Data.ToArray());
                newMData.AddRange(newData);

                m_Data = newMData.ToArray();

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