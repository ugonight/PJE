using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//英語演習の和訳を生成するプログラム
//Program to generate Japanese translation of English exercises

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PJE
{
    public partial class Form1 : Form
    {
        Control control;
        bool mEditNow;
        int mEditIndex;
        String mFileName;

        public Form1()
        {
            InitializeComponent();
            control = new Control();
            mEditNow = false;
            mEditIndex = 0;
            mFileName = "";
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (mEditNow)
            {
                control.setSentence(mEditIndex, textEnglish.Text, textFootnote.Text, textJapanese.Text);
                listSentence.Items[mEditIndex] = textEnglish.Text;
                textEnglish.Text = ""; textFootnote.Text = ""; textJapanese.Text = "";
                mEditNow = false;
                buttonAdd.Text = "追加";
            }
            else
            {
                control.addSentence(textEnglish.Text, textFootnote.Text, textJapanese.Text);
                listSentence.Items.Add(textEnglish.Text);
                textEnglish.Text = ""; textFootnote.Text=""; textJapanese.Text = "";
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (listSentence.SelectedIndex >= 0)
            {
                textEnglish.Text = control.getEnglish(listSentence.SelectedIndex);
                textFootnote.Text = control.getFootnote(listSentence.SelectedIndex);
                textJapanese.Text = control.getJapanese(listSentence.SelectedIndex);
                mEditNow = true;
                mEditIndex = listSentence.SelectedIndex;
                buttonAdd.Text = "適用";
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (listSentence.SelectedIndex >= 0)
            {
                control.removeSentence(listSentence.SelectedIndex);
                listSentence.Items.RemoveAt(listSentence.SelectedIndex);  
            }
        }

        private void ToolStripSplit_Click(object sender, EventArgs e)
        {
            string[] stArrayData = textEnglish.Text.Split('.');
            foreach (string stData in stArrayData)
            {
                // MessageBox.Show(stData);
                control.addSentence(stData + ".", "", "");
                listSentence.Items.Add(stData + ".");
            }
            textEnglish.Text = "";
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            String output;
            output = @"\documentclass[11pt,a4paper]{jsarticle} 
%
\usepackage{amsmath,amssymb}
\usepackage{bm}
\usepackage{graphicx}
\usepackage{ascmac}
%
\setlength{\textwidth}{\fullwidth}
\setlength{\textheight}{39\baselineskip}
\addtolength{\textheight}{\topskip}
\setlength{\voffset}{-0.5in}
\setlength{\headsep}{0.3in}
%
\newcommand{\divergence}{\mathrm{div}\,}  % ダイバージェンス
\newcommand{\grad}{\mathrm{grad}\,}  % グラディエント
\newcommand{\rot}{\mathrm{rot}\,}  % ローテーション
%
\pagestyle{myheadings}
\markright{\footnotesize \sf ";
            output += textHeader.Text + @"}
\begin{document}
%
% 
";
            for (int i=0; i< control.getSentenceNum(); i++)
            {
                output += "\\fontsize{" + Properties.Settings.Default.e_font + "pt}{" + Properties.Settings.Default.e_line + "pt}\\selectfont\r\n";
                output += control.getEnglish(i) + "\r\n";
                if (control.getFootnote(i) != "") output += "\\footnote{" + control.getFootnote(i) + "}\\\\\r\n";
                output += "\\fontsize{" + Properties.Settings.Default.j_font + "pt}{" + Properties.Settings.Default.j_line + "pt}\\selectfont\r\n";
                output += control.getJapanese(i) + "\\\\\r\n\\\\\r\n";
            }
            output += @"%
%
\end{document}";
            textOutput.Text = output;
      
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (mFileName == "")
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "report.tex";
                sfd.Filter = "TeXファイル(*.tex)|*.tex|すべてのファイル(*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "保存先のファイルを選択してください";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    mFileName = sfd.FileName;
                }
                else
                {
                    return;
                }
            }


            System.IO.StreamWriter sw = new System.IO.StreamWriter(
               mFileName,
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
            sw.Write(textOutput.Text);
            sw.Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textOutput.Text);
        }

        private void 名前を付けて保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "report.tex";
            sfd.Filter = "TeXファイル(*.tex)|*.tex|すべてのファイル(*.*)|*.*";
            sfd.FilterIndex = 1;
            sfd.Title = "保存先のファイルを選択してください";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                mFileName = sfd.FileName;
            }
            else
            {
                return;
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
               mFileName,
                false,
                System.Text.Encoding.GetEncoding("shift_jis"));
            sw.Write(textOutput.Text);
            sw.Close();
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "default.html";
            ofd.Filter = "TeXファイル(*.tex)|*.tex|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "開くファイルを選択してください";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
               mFileName = ofd.FileName;
            }
            else
            {
                return;
            }

            System.IO.StreamReader sr = new System.IO.StreamReader(
               mFileName,
                System.Text.Encoding.GetEncoding("shift_jis"));

            control = new Control();
            listSentence.Items.Clear();
            textEnglish.Text = ""; textFootnote.Text = ""; textJapanese.Text = "";

            //for (int i=0; i< 22; i++) sr.ReadLine();
            string str;
            while (sr.Peek() > -1)
            {
                str = sr.ReadLine();
                if (str.IndexOf("\\markright") != -1)
                {
                    int begin = str.IndexOf("\\sf") + 4, end = str.IndexOf("}");
                    textHeader.Text = str.Substring(begin,end - begin);
                    break;
                }
            }
            while (sr.Peek() > -1)
            {
                if (sr.ReadLine() == "%")
                {
                    sr.ReadLine();
                    break;
                }
            }

                string en, fn, jp;
            while (sr.Peek() > -1)
            {
                if (sr.ReadLine() == "%") break;
                en = sr.ReadLine();
                if (en.IndexOf("\\") != -1)
                    en = en.Substring(0,en.IndexOf("\\"));
                fn = sr.ReadLine();
                if (fn.IndexOf("fontsize") == -1)
                {
                    int begin = fn.IndexOf("{") + 1, end = fn.IndexOf("}") - 1;
                    fn = fn.Substring(begin,end - begin + 1);
                    sr.ReadLine();
                }
                else
                {
                    fn = "";
                }
                jp = sr.ReadLine();
                jp = jp.Substring(0, jp.IndexOf("\\"));
                sr.ReadLine();
                control.addSentence(en, fn, jp);
                listSentence.Items.Add(en);
            }
            sr.Close();
        }

        private void 新規作成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control = new Control();
            mEditNow = false;
            mEditIndex = 0;
            mFileName = "";
            listSentence.Items.Clear();
            textEnglish.Text = ""; textFootnote.Text = ""; textJapanese.Text = "";
        }

        private void textOutput_TextChanged(object sender, EventArgs e)
        {
            if (textOutput.Text != "")
            {
                buttonSave.Enabled = true;
                buttonCopy.Enabled = true;
            }
            else
            {
                buttonSave.Enabled = false;
                buttonCopy.Enabled = false;
            }
        }

        private void 設定OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog(this);
            f.Dispose();
        }
    }
}
