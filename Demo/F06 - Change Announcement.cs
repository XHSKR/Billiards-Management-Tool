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
    public partial class Form6 : Form
    {
        private string Form2_value;

        public string Passvalue
        {
            get { return this.Form2_value; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_value = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }

        public Form6()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string savePath = @"announcement.txt";
            string textValue = richTextBox1.Text.ToString();
            System.IO.File.WriteAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            if (richTextBox1.Text == string.Empty)
            {
                MessageBox.Show("No input was entered.");
                return;
            }
            else
            Passvalue = richTextBox1.Text; // Form1 으로 값을 전달하기 위해
            this.Close();
        }

         
        private void Form6_Load(object sender, EventArgs e)
        {
            string textValue = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "announcement.txt", Encoding.GetEncoding("UTF-8"));
            richTextBox1.Text = textValue;
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Currently there are no announcements.";
            string savePath = @"announcement.txt";
            string textValue = richTextBox1.Text.ToString();
            System.IO.File.WriteAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            Passvalue = richTextBox1.Text; // Form1 으로 값을 전달하기 위해
            this.Close();
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (checkBox1.Checked)
            {
                return;
            }
            else
            {
                if (e.KeyValue == 13)
                {
                    button1_Click(null, null);
                }
            }
        }

        private void Form6_FormClosing(object sender, FormClosingEventArgs e)
        {
            string textValue = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "announcement.txt", Encoding.GetEncoding("UTF-8"));
            Passvalue = textValue; // Form1 으로 값을 전달하기 위해
        }
    }
}
