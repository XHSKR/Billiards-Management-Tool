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
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e) //숫자와 .만 입력가능
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        decimal cashout;
        decimal cardreceived;

        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            textBox2.Text = (decimal.Parse(textBox1.Text) + (decimal)0.5).ToString();
            cashout = decimal.Parse(textBox1.Text);
            cardreceived = decimal.Parse(textBox2.Text);
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (decimal.Parse(textBox1.Text) > 0)
            {
                if (Form2.frmObj.cash >= decimal.Parse(textBox1.Text))
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Cash out ${1} is occurred and received ${2} by debit card.\n", Form2.userinfo, cashout, cardreceived);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));


                    Form2.frmObj.card = Form2.frmObj.card + cashout; //카드이지만 캐시만큼 매출로 잡고 나머지 수수료를 cashout에 나타나도록 하게함.
                    Form2.frmObj.cash = Form2.frmObj.cash - cashout;
                    Form2.frmObj.cashout = Form2.frmObj.cashout + (decimal)0.5;
                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(string.Format("Cash out is available within the cash income of the day.\nMaximum cash out available: ${0}.", Form2.frmObj.cash));
                    return;
                }
            }
            else
            {
                MessageBox.Show("Cash out is available for purchases of at least $1");
                return;
            }
        }


        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (e.KeyValue == 13)
            {
                Button1_Click(null, null);
            }
        }
    }
}
