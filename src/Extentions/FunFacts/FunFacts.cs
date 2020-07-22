using System;
using System.Collections.Generic;


namespace TextureJinn.Extentions.FunFacts
{
    public class FunFacts
    {
        public string FunFact { get => m_RandomFact(); }

        protected Random m_psrnd = new Random(Environment.TickCount);
        protected string[] m_DefaultFunFacts = FunFactDB.FunFacts;
        protected string[] m_CompiledFunFacts;
        protected List<string> m_ExtraFunFacts = new List<string>();
        protected bool m_UseDefaultFunFacts = true;

        public bool UseDefaultFunFacts {get => m_UseDefaultFunFacts; set => m_UpdateFunfacts((byte)((value)? 1 : 0));}

        public FunFacts()
        {
            m_UpdateFunfacts(1);
        }

        public void AddFunFacts(ICollection<string> facts)
        {
            m_ExtraFunFacts.AddRange(facts);

            m_UpdateFunfacts();
        }

        protected void m_UpdateFunfacts(byte newUseDefaultFunFactsValue = 3)
        {
            newUseDefaultFunFactsValue = (newUseDefaultFunFactsValue == 0 && m_ExtraFunFacts.Count > 0)? (byte)0 : (byte)1;

            m_UseDefaultFunFacts = (newUseDefaultFunFactsValue == 0)? false : true;

            // m_CompiledFunFacts = new string[((UseDefaultFunFacts)? m_DefaultFunFacts.Length : 0) + m_ExtraFunFacts.Count];
            List<string> tmp = new List<string>();
            
            if (UseDefaultFunFacts)
            {
                tmp.AddRange(m_DefaultFunFacts);
            }

            tmp.AddRange(m_ExtraFunFacts);

            m_CompiledFunFacts = tmp.ToArray();
        }

        protected string m_RandomFact()
        {
            return m_CompiledFunFacts[m_psrnd.Next(0, m_CompiledFunFacts.Length)];
        }
    }
}