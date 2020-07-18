using System;
using System.Text;


namespace TextureJinn.Extentions.StreamHacks
{
    /// <summary>
    /// A fake, in-memory stream, imitating FileStream
    /// </summary>
    public class FakeUTF8Stream : FakeStream
    {
        protected string m_Text
        {
            get => Encoding.UTF8.GetString(m_Data.ToArray());
            set => m_Data = Encoding.UTF8.GetBytes(value);
        }

        public FakeUTF8Stream(string str)
        {
            m_Text = str;
        }

        public FakeUTF8Stream(byte[] data) : base(data) { }

        public void ReplaceAll(string search, string replace, StringComparison comparison = StringComparison.InvariantCulture)
        {
            m_Text = m_Text.Replace(search, replace, comparison);
        }

        public void ReplaceAll(string search, string replace, bool caseSensitive)
        {
            m_Text = m_Text.Replace(search, replace, StringComparison.InvariantCultureIgnoreCase);
        }

        public void ReplaceFirst(string search, string replace) {
            string cache = m_Text;
            
            string[] sections = cache.Split(search);

            for (int i = 0; i < sections.Length; i++)
            {
                if (i == 0)
                {
                    cache = sections[0] + replace + sections[1];
                    i++;
                }
                else
                {
                    cache += search + sections[i];
                }
            }

            m_Text = cache;
        }
    }
}