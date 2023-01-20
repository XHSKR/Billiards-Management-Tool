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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.tablememoindicator == "1")
            {
                Form2.tablememo1 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "2")
            {
                Form2.tablememo2 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "3")
            {
                Form2.tablememo3 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "4")
            {
                Form2.tablememo4 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "5")
            {
                Form2.tablememo5 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "6")
            {
                Form2.tablememo6 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "7")
            {
                Form2.tablememo7 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "8")
            {
                Form2.tablememo8 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "9")
            {
                Form2.tablememo9 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "10")
            {
                Form2.tablememo10 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "11")
            {
                Form2.tablememo11 = richTextBox1.Text;
                this.Close();
            }
            if (Form2.tablememoindicator == "12")
            {
                Form2.tablememo12 = richTextBox1.Text;
                this.Close();
            }

        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (e.KeyValue == 13)
            {
                button1_Click(null, null);
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (Form2.tablememoindicator == "1")
            {
                richTextBox1.Text = Form2.tablememo1;
            }
            if (Form2.tablememoindicator == "2")
            {
                richTextBox1.Text = Form2.tablememo2;
            }
            if (Form2.tablememoindicator == "3")
            {
                richTextBox1.Text = Form2.tablememo3;
            }
            if (Form2.tablememoindicator == "4")
            {
                richTextBox1.Text = Form2.tablememo4;
            }
            if (Form2.tablememoindicator == "5")
            {
                richTextBox1.Text = Form2.tablememo5;
            }
            if (Form2.tablememoindicator == "6")
            {
                richTextBox1.Text = Form2.tablememo6;
            }
            if (Form2.tablememoindicator == "7")
            {
                richTextBox1.Text = Form2.tablememo7;
            }
            if (Form2.tablememoindicator == "8")
            {
                richTextBox1.Text = Form2.tablememo8;
            }
            if (Form2.tablememoindicator == "9")
            {
                richTextBox1.Text = Form2.tablememo9;
            }
            if (Form2.tablememoindicator == "10")
            {
                richTextBox1.Text = Form2.tablememo10;
            }
            if (Form2.tablememoindicator == "11")
            {
                richTextBox1.Text = Form2.tablememo11;
            }
            if (Form2.tablememoindicator == "12")
            {
                richTextBox1.Text = Form2.tablememo12;
            }
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }
    }
}
