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
    public partial class Form5 : Form
    {

        public Form5()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.modifytime == "1")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration1 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "2")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration2 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "3")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration3 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "4")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration4 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "5")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration5 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "6")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration6 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "7")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration7 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "8")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration8 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "9")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration9 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "10")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration10 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "11")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration11 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }
            if (Form2.modifytime == "12")
            {
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 timer is altered to {1} minutes.\n", Form2.userinfo, textBox1.Text);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                Form2.frmObj.duration12 = Convert.ToInt32(textBox1.Text) * 60;
                this.Close();
            }

        }


        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e) //숫자 이외 키 누름 방지
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (e.KeyValue == 13)
            {
                button1_Click(null, null);
            }
        }
    }
}
