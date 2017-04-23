using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PJE
{
    /// <summary>
    /// 文リストを管理するクラス
    /// </summary>
    class Control
    {
        private List<String> mEnglish;
        private List<String> mFootnote;
        private List<String> mJapanese;

        public Control()
        {
            mEnglish = new List<String>();
            mFootnote = new List<String>();
            mJapanese = new List<String>();
        }

        public void addSentence(String e, String f, String j)
        {
            mEnglish.Add(e);
            mFootnote.Add(f);
            mJapanese.Add(j);
        }

        public void setSentence(int i,String e, String f, String j)
        {
            mEnglish[i] = e;
            mFootnote[i] = f;
            mJapanese[i] = j;
        }

        public void removeSentence(int i)
        {
            mEnglish.RemoveAt(i);
            mFootnote.RemoveAt(i);
            mJapanese.RemoveAt(i);
        }

        public String getEnglish(int i)
        {
            return mEnglish[i];
        }
        public String getFootnote(int i)
        {
            return mFootnote[i];
        }
        public String getJapanese(int i)
        {
            return mJapanese[i];
        }

        public int getSentenceNum()
        {
            return mEnglish.Count;
        }

    }
}
