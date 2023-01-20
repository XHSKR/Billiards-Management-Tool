using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form12 : Form
    {
        public Form12()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            string texts = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "log.txt");
            richTextBox1.Text = texts;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            ((RichTextBox)sender).Font = new Font("Calibri", 13f);
        }
    }
}
