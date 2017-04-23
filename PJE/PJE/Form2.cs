using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PJE
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            numericEFont.Value = Properties.Settings.Default.e_font;
            numericELine.Value = Properties.Settings.Default.e_line;
            numericJFont.Value = Properties.Settings.Default.j_font;
            numericJLine.Value = Properties.Settings.Default.j_line;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.e_font = (int)numericEFont.Value;
            Properties.Settings.Default.e_line = (int)numericELine.Value;
            Properties.Settings.Default.j_font = (int)numericJFont.Value;
            Properties.Settings.Default.j_line = (int)numericJLine.Value;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
