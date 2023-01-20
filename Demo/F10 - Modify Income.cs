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
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc");
            textBox1.Text = Form2.frmObj.card.ToString();
            textBox2.Text = Form2.frmObj.cash.ToString();
            textBox3.Text = Form2.frmObj.cashout.ToString();
            textBox4.Text = Form2.frmObj.total.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                decimal wascard = Form2.frmObj.card;
                decimal wascash = Form2.frmObj.cash;
                decimal wascashout = Form2.frmObj.cashout;
                decimal wastotal = Form2.frmObj.total;
                Form2.frmObj.card = decimal.Parse(textBox1.Text);
                Form2.frmObj.cash = decimal.Parse(textBox2.Text);
                Form2.frmObj.cashout = decimal.Parse(textBox3.Text);
                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Income is manually changed to Card: ${1} + Cash: ${2} + Cash Out: ${3} = Total: ${4}. Reason: {9}" +
                    " (was Card: ${5} + Cash: ${6} + Cash Out: ${7} = Total: ${8}.)\n", Form2.userinfo, Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total, wascard, wascash,
                    wascashout, wastotal, richTextBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                this.Close();
            }
            else
            {
                MessageBox.Show("You cannot leave the reason form empty.");
                richTextBox1.Text = "";
                return;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e) //숫자와 .만 입력가능
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox2.SelectAll();
            textBox3.SelectAll();
        }

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                Button1_Click(null, null);
            }
        }

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    textBox1.Text = "0";
                }
                if (textBox2.Text == "")
                {
                    textBox2.Text = "0";
                }
                if (textBox3.Text == "")
                {
                    textBox3.Text = "0";
                }
                decimal totalprice;
                totalprice = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                textBox4.Text = totalprice.ToString();
            }
            catch { }
        }

        private void RichTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Char.IsPunctuation(e.KeyChar) || Char.IsDigit(e.KeyChar) || Char.IsLetter(e.KeyChar) || Char.IsSymbol(e.KeyChar)) && e.KeyChar != 8)
            {
                e.Handled = true;
                MessageBox.Show("Korean Only.\nIf the error persists, press 한/영 key once.");
            }
        }
    }
}
