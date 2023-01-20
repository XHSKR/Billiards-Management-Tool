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
    public partial class Form3 : Form
    {
        //Form2로 파일전달
        static Form3 _frmObj;
        public static Form3 frmObj
        {
            get { return _frmObj; }
            set { _frmObj = value; }
        }
        //후불금액 전달받기
        private string Form2_value;
        public string Passvalue
        {
            get { return this.Form2_value; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_value = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }

        //테이블넘버감지
        private string Form2_value2;

        public string tablenumber
        {
            get { return this.Form2_value2; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_value2 = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }

        public Form3()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.

        }
        decimal price;
        private void Form3_Load(object sender, EventArgs e)
        {
            frmObj = this;

            if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
            { //폼 로드시 기본 선택 시간과 금액 포켓볼
                radioButton2.Checked = true;
                textBox9.Text = "21";
                price = decimal.Parse(textBox9.Text);
            }
            else
            { //폼 로드시 기본 선택 시간과 금액 4구
                radioButton2.Checked = true;
                textBox9.Text = "18";
                price = decimal.Parse(textBox9.Text);
            }
            try
            {
                textBox9.Text = Passvalue.ToString();
                price = decimal.Parse(textBox9.Text);
            }
            catch { }
            price = Form2.price;
            if (price == 0)
            {
                price = 15;
            }
            if (Form8.beveragepurchase == "yes") //음료만 계산일 경우
            {
                this.FormBorderStyle = FormBorderStyle.None;
                textBox9.Text = Form8.beverageprice.ToString();
            }
        }
        //숫자 이외 키 누름 방지 시작
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        //숫자 이외 키 누름 방지 끝

        private void button1_Click(object sender, EventArgs e)
        {
            decimal totalprice;
            totalprice = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text) + decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);

            if (Form8.beveragepurchase == "yes")
            {
                if (totalprice > decimal.Parse(textBox9.Text))
                {
                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (totalprice < decimal.Parse(textBox9.Text))
                {
                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (totalprice == decimal.Parse(textBox9.Text)) //음료계산 성공
                {
                    Form8.beveragepurchase = "";
                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                    decimal totalreceived = cardreceived + cashreceived;

                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase is occurred and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    this.Close();
                }
            }
            //Table 1 시작
            if (tablenumber == "1")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb1 = "paying after";
                    Form2.frmObj.duration1 = 0;
                    Form2.frmObj.timer1.Enabled = true;
                    Form2.frmObj.timer1.Start();
                    this.Close();
                }
                else if (Form2.gametypetb1 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb1 = 0;
                        Form2.frmObj.timer1.Stop();
                        Form2.frmObj.Table1.Text = "Table 1";
                        Form2.gametypetb1 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration1 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration1 = 0;
                        Form2.tablememo1 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration1 = totaldiff;

                                Form2.gametypetb1 = "daytime";
                                Form2.frmObj.timer1.Enabled = true;
                                Form2.frmObj.timer1.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb1 = "threeevent";
                                Form2.frmObj.timer1.Enabled = true;
                                Form2.frmObj.timer1.Start();
                                Form2.frmObj.duration1 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb1 = "half an hour";
                                    Form2.frmObj.timer1.Enabled = true;
                                    Form2.frmObj.timer1.Start();
                                    Form2.frmObj.duration1 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb1 = "an hour";
                                    Form2.frmObj.timer1.Enabled = true;
                                    Form2.frmObj.timer1.Start();
                                    Form2.frmObj.duration1 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb1 = "two hours";
                                    Form2.frmObj.timer1.Enabled = true;
                                    Form2.frmObj.timer1.Start();
                                    Form2.frmObj.duration1 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 1 끝

            //Table 2 시작
            if (tablenumber == "2")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb2 = "paying after";
                    Form2.frmObj.duration2 = 0;
                    Form2.frmObj.timer2.Enabled = true;
                    Form2.frmObj.timer2.Start();
                    this.Close();
                }
                else if (Form2.gametypetb2 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb2 = 0;
                        Form2.frmObj.timer2.Stop();
                        Form2.frmObj.Table2.Text = "Table 2";
                        Form2.gametypetb2 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration2 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration2 = 0;
                        Form2.tablememo2 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration2 = totaldiff;

                                Form2.gametypetb2 = "daytime";
                                Form2.frmObj.timer2.Enabled = true;
                                Form2.frmObj.timer2.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb2 = "threeevent";
                                Form2.frmObj.timer2.Enabled = true;
                                Form2.frmObj.timer2.Start();
                                Form2.frmObj.duration2 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb2 = "half an hour";
                                    Form2.frmObj.timer2.Enabled = true;
                                    Form2.frmObj.timer2.Start();
                                    Form2.frmObj.duration2 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb2 = "an hour";
                                    Form2.frmObj.timer2.Enabled = true;
                                    Form2.frmObj.timer2.Start();
                                    Form2.frmObj.duration2 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb2 = "two hours";
                                    Form2.frmObj.timer2.Enabled = true;
                                    Form2.frmObj.timer2.Start();
                                    Form2.frmObj.duration2 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 2 끝
            //Table 3 시작
            if (tablenumber == "3")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb3 = "paying after";
                    Form2.frmObj.duration3 = 0;
                    Form2.frmObj.timer3.Enabled = true;
                    Form2.frmObj.timer3.Start();
                    this.Close();
                }
                else if (Form2.gametypetb3 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb3 = 0;
                        Form2.frmObj.timer3.Stop();
                        Form2.frmObj.Table3.Text = "Table 3";
                        Form2.gametypetb3 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration3 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.tablememo3 = "";
                        Form2.frmObj.duration3 = 0;
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration3 = totaldiff;

                                Form2.gametypetb3 = "daytime";
                                Form2.frmObj.timer3.Enabled = true;
                                Form2.frmObj.timer3.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb3 = "threeevent";
                                Form2.frmObj.timer3.Enabled = true;
                                Form2.frmObj.timer3.Start();
                                Form2.frmObj.duration3 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb3 = "half an hour";
                                    Form2.frmObj.timer3.Enabled = true;
                                    Form2.frmObj.timer3.Start();
                                    Form2.frmObj.duration3 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb3 = "an hour";
                                    Form2.frmObj.timer3.Enabled = true;
                                    Form2.frmObj.timer3.Start();
                                    Form2.frmObj.duration3 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb3 = "two hours";
                                    Form2.frmObj.timer3.Enabled = true;
                                    Form2.frmObj.timer3.Start();
                                    Form2.frmObj.duration3 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 3 끝
            //Table 4 시작
            if (tablenumber == "4")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb4 = "paying after";
                    Form2.frmObj.duration4 = 0;
                    Form2.frmObj.timer4.Enabled = true;
                    Form2.frmObj.timer4.Start();
                    this.Close();
                }
                else if (Form2.gametypetb4 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb4 = 0;
                        Form2.frmObj.timer4.Stop();
                        Form2.frmObj.Table4.Text = "Table 4";
                        Form2.gametypetb4 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration4 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration4 = 0;
                        Form2.tablememo4 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration4 = totaldiff;

                                Form2.gametypetb4 = "daytime";
                                Form2.frmObj.timer4.Enabled = true;
                                Form2.frmObj.timer4.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb4 = "threeevent";
                                Form2.frmObj.timer4.Enabled = true;
                                Form2.frmObj.timer4.Start();
                                Form2.frmObj.duration4 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb4 = "half an hour";
                                    Form2.frmObj.timer4.Enabled = true;
                                    Form2.frmObj.timer4.Start();
                                    Form2.frmObj.duration4 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb4 = "an hour";
                                    Form2.frmObj.timer4.Enabled = true;
                                    Form2.frmObj.timer4.Start();
                                    Form2.frmObj.duration4 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb4 = "two hours";
                                    Form2.frmObj.timer4.Enabled = true;
                                    Form2.frmObj.timer4.Start();
                                    Form2.frmObj.duration4 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 4 끝
            //Table 5 시작
            if (tablenumber == "5")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb5 = "paying after";
                    Form2.frmObj.duration5 = 0;
                    Form2.frmObj.timer5.Enabled = true;
                    Form2.frmObj.timer5.Start();
                    this.Close();
                }
                else if (Form2.gametypetb5 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb5 = 0;
                        Form2.frmObj.timer5.Stop();
                        Form2.frmObj.Table5.Text = "Table 5";
                        Form2.gametypetb5 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration5 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration5 = 0;
                        Form2.tablememo5 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration5 = totaldiff;

                                Form2.gametypetb5 = "daytime";
                                Form2.frmObj.timer5.Enabled = true;
                                Form2.frmObj.timer5.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb5 = "threeevent";
                                Form2.frmObj.timer5.Enabled = true;
                                Form2.frmObj.timer5.Start();
                                Form2.frmObj.duration5 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb5 = "half an hour";
                                    Form2.frmObj.timer5.Enabled = true;
                                    Form2.frmObj.timer5.Start();
                                    Form2.frmObj.duration5 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb5 = "an hour";
                                    Form2.frmObj.timer5.Enabled = true;
                                    Form2.frmObj.timer5.Start();
                                    Form2.frmObj.duration5 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb5 = "two hours";
                                    Form2.frmObj.timer5.Enabled = true;
                                    Form2.frmObj.timer5.Start();
                                    Form2.frmObj.duration5 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 5 끝
            //Table 6 시작
            if (tablenumber == "6")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb6 = "paying after";
                    Form2.frmObj.duration6 = 0;
                    Form2.frmObj.timer6.Enabled = true;
                    Form2.frmObj.timer6.Start();
                    this.Close();
                }
                else if (Form2.gametypetb6 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb6 = 0;
                        Form2.frmObj.timer6.Stop();
                        Form2.frmObj.Table6.Text = "Table 6";
                        Form2.gametypetb6 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration6 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration6 = 0;
                        Form2.tablememo6 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration6 = totaldiff;

                                Form2.gametypetb6 = "daytime";
                                Form2.frmObj.timer6.Enabled = true;
                                Form2.frmObj.timer6.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb6 = "threeevent";
                                Form2.frmObj.timer6.Enabled = true;
                                Form2.frmObj.timer6.Start();
                                Form2.frmObj.duration6 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb6 = "half an hour";
                                    Form2.frmObj.timer6.Enabled = true;
                                    Form2.frmObj.timer6.Start();
                                    Form2.frmObj.duration6 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb6 = "an hour";
                                    Form2.frmObj.timer6.Enabled = true;
                                    Form2.frmObj.timer6.Start();
                                    Form2.frmObj.duration6 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb6 = "two hours";
                                    Form2.frmObj.timer6.Enabled = true;
                                    Form2.frmObj.timer6.Start();
                                    Form2.frmObj.duration6 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 6 끝
            //Table 7 시작
            if (tablenumber == "7")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb7 = "paying after";
                    Form2.frmObj.duration7 = 0;
                    Form2.frmObj.timer7.Enabled = true;
                    Form2.frmObj.timer7.Start();
                    this.Close();
                }
                else if (Form2.gametypetb7 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb7 = 0;
                        Form2.frmObj.timer7.Stop();
                        Form2.frmObj.Table7.Text = "Table 7";
                        Form2.gametypetb7 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration7 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration7 = 0;
                        Form2.tablememo7 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration7 = totaldiff;

                                Form2.gametypetb7 = "daytime";
                                Form2.frmObj.timer7.Enabled = true;
                                Form2.frmObj.timer7.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb7 = "threeevent";
                                Form2.frmObj.timer7.Enabled = true;
                                Form2.frmObj.timer7.Start();
                                Form2.frmObj.duration7 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb7 = "half an hour";
                                    Form2.frmObj.timer7.Enabled = true;
                                    Form2.frmObj.timer7.Start();
                                    Form2.frmObj.duration7 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb7 = "an hour";
                                    Form2.frmObj.timer7.Enabled = true;
                                    Form2.frmObj.timer7.Start();
                                    Form2.frmObj.duration7 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb7 = "two hours";
                                    Form2.frmObj.timer7.Enabled = true;
                                    Form2.frmObj.timer7.Start();
                                    Form2.frmObj.duration7 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 7 끝
            //Table 8 시작
            if (tablenumber == "8")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb8 = "paying after";
                    Form2.frmObj.duration8 = 0;
                    Form2.frmObj.timer8.Enabled = true;
                    Form2.frmObj.timer8.Start();
                    this.Close();
                }
                else if (Form2.gametypetb8 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb8 = 0;
                        Form2.frmObj.timer8.Stop();
                        Form2.frmObj.Table8.Text = "Table 8";
                        Form2.gametypetb8 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration8 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration8 = 0;
                        Form2.tablememo8 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration8 = totaldiff;

                                Form2.gametypetb8 = "daytime";
                                Form2.frmObj.timer8.Enabled = true;
                                Form2.frmObj.timer8.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb8 = "threeevent";
                                Form2.frmObj.timer8.Enabled = true;
                                Form2.frmObj.timer8.Start();
                                Form2.frmObj.duration8 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb8 = "half an hour";
                                    Form2.frmObj.timer8.Enabled = true;
                                    Form2.frmObj.timer8.Start();
                                    Form2.frmObj.duration8 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb8 = "an hour";
                                    Form2.frmObj.timer8.Enabled = true;
                                    Form2.frmObj.timer8.Start();
                                    Form2.frmObj.duration8 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb8 = "two hours";
                                    Form2.frmObj.timer8.Enabled = true;
                                    Form2.frmObj.timer8.Start();
                                    Form2.frmObj.duration8 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 8 끝
            //Table 9 시작
            if (tablenumber == "9")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb9 = "paying after";
                    Form2.frmObj.duration9 = 0;
                    Form2.frmObj.timer9.Enabled = true;
                    Form2.frmObj.timer9.Start();
                    this.Close();
                }
                else if (Form2.gametypetb9 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb9 = 0;
                        Form2.frmObj.timer9.Stop();
                        Form2.frmObj.Table9.Text = "Table 9";
                        Form2.gametypetb9 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration9 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration9 = 0;
                        Form2.tablememo9 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration9 = totaldiff;

                                Form2.gametypetb9 = "daytime";
                                Form2.frmObj.timer9.Enabled = true;
                                Form2.frmObj.timer9.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb9 = "threeevent";
                                Form2.frmObj.timer9.Enabled = true;
                                Form2.frmObj.timer9.Start();
                                Form2.frmObj.duration9 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb9 = "half an hour";
                                    Form2.frmObj.timer9.Enabled = true;
                                    Form2.frmObj.timer9.Start();
                                    Form2.frmObj.duration9 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb9 = "an hour";
                                    Form2.frmObj.timer9.Enabled = true;
                                    Form2.frmObj.timer9.Start();
                                    Form2.frmObj.duration9 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb9 = "two hours";
                                    Form2.frmObj.timer9.Enabled = true;
                                    Form2.frmObj.timer9.Start();
                                    Form2.frmObj.duration9 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 9 끝
            //Table 10 시작
            if (tablenumber == "10")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb10 = "paying after";
                    Form2.frmObj.duration10 = 0;
                    Form2.frmObj.timer10.Enabled = true;
                    Form2.frmObj.timer10.Start();
                    this.Close();
                }
                else if (Form2.gametypetb10 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb10 = 0;
                        Form2.frmObj.timer10.Stop();
                        Form2.frmObj.Table10.Text = "Table 10";
                        Form2.gametypetb10 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration10 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration10 = 0;
                        Form2.tablememo10 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration10 = totaldiff;

                                Form2.gametypetb10 = "daytime";
                                Form2.frmObj.timer10.Enabled = true;
                                Form2.frmObj.timer10.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb10 = "threeevent";
                                Form2.frmObj.timer10.Enabled = true;
                                Form2.frmObj.timer10.Start();
                                Form2.frmObj.duration10 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb10 = "half an hour";
                                    Form2.frmObj.timer10.Enabled = true;
                                    Form2.frmObj.timer10.Start();
                                    Form2.frmObj.duration10 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb10 = "an hour";
                                    Form2.frmObj.timer10.Enabled = true;
                                    Form2.frmObj.timer10.Start();
                                    Form2.frmObj.duration10 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb10 = "two hours";
                                    Form2.frmObj.timer10.Enabled = true;
                                    Form2.frmObj.timer10.Start();
                                    Form2.frmObj.duration10 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 10 끝
            //Table 11 시작
            if (tablenumber == "11")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb11 = "paying after";
                    Form2.frmObj.duration11 = 0;
                    Form2.frmObj.timer11.Enabled = true;
                    Form2.frmObj.timer11.Start();
                    this.Close();
                }
                else if (Form2.gametypetb11 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb11 = 0;
                        Form2.frmObj.timer11.Stop();
                        Form2.frmObj.Table11.Text = "Table 11";
                        Form2.gametypetb11 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration11 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration11 = 0;
                        Form2.tablememo11 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration11 = totaldiff;

                                Form2.gametypetb11 = "daytime";
                                Form2.frmObj.timer11.Enabled = true;
                                Form2.frmObj.timer11.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb11 = "threeevent";
                                Form2.frmObj.timer11.Enabled = true;
                                Form2.frmObj.timer11.Start();
                                Form2.frmObj.duration11 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb11 = "half an hour";
                                    Form2.frmObj.timer11.Enabled = true;
                                    Form2.frmObj.timer11.Start();
                                    Form2.frmObj.duration11 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb11 = "an hour";
                                    Form2.frmObj.timer11.Enabled = true;
                                    Form2.frmObj.timer11.Start();
                                    Form2.frmObj.duration11 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb11 = "two hours";
                                    Form2.frmObj.timer11.Enabled = true;
                                    Form2.frmObj.timer11.Start();
                                    Form2.frmObj.duration11 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 11 끝
            //Table 12 시작
            if (tablenumber == "12")
            {
                //후불 시작

                if (checkBox1.Checked)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as paying after.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    Form2.gametypetb12 = "paying after";
                    Form2.frmObj.duration12 = 0;
                    Form2.frmObj.timer12.Enabled = true;
                    Form2.frmObj.timer12.Start();
                    this.Close();
                }
                else if (Form2.gametypetb12 == "paying after")
                {
                    if (totalprice > decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice < decimal.Parse(textBox9.Text))
                    {
                        MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (totalprice == decimal.Parse(textBox9.Text)) //후불 세션종료
                    {
                        Form2.beveragetb12 = 0;
                        Form2.frmObj.timer12.Stop();
                        Form2.frmObj.Table12.Text = "Table 12";
                        Form2.gametypetb12 = "";
                        label12.Text = "";
                        decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                        decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                        decimal totalreceived = cardreceived + cashreceived;

                        Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                        Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                        Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                        Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 stopped as paying after and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully. (Playtime: {4} minutes.)\n", Form2.userinfo, totalreceived, cardreceived, cashreceived, Form2.frmObj.duration12 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form2.frmObj.duration12 = 0;
                        Form2.tablememo12 = "";
                        this.Close();
                    }
                }
                //후불 끝
                else
                {

                    //데이타임 이벤트 시작
                    if (comboBox1.Text == "DAYTIME")
                    {
                        if (textBox8.Text == "")
                        {
                            textBox8.Text = "0";
                        }
                        if (Convert.ToInt32(textBox8.Text) <= 1)
                        {
                            MessageBox.Show("Players need to be 2 or more.", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                                DateTime EndDate = Convert.ToDateTime("18:00:00");
                                TimeSpan dateDiff = EndDate - StartDate;
                                int diffHour = dateDiff.Hours;
                                int diffMinute = dateDiff.Minutes;
                                int diffSecond = dateDiff.Seconds;
                                int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;
                                if (totaldiff <= 0)
                                {
                                    MessageBox.Show("Unable to start the event session since it's past 6pm.");
                                    return;
                                }
                                Form2.frmObj.duration12 = totaldiff;

                                Form2.gametypetb12 = "daytime";
                                Form2.frmObj.timer12.Enabled = true;
                                Form2.frmObj.timer12.Start();
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as DAYTIME and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                    }
                    //데이타임 이벤트 끝
                    else
                    {
                        //3시간 Event 시작
                        if (comboBox1.Text == "3 HOURS EVENT")
                        {
                            if (totalprice > Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice < Convert.ToInt32(textBox9.Text))
                            {
                                MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                            {
                                Form2.gametypetb12 = "threeevent";
                                Form2.frmObj.timer12.Enabled = true;
                                Form2.frmObj.timer12.Start();
                                Form2.frmObj.duration12 = 10800;
                                decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                decimal totalreceived = cardreceived + cashreceived;
                                //로그 시작
                                string savePath = @"log.txt";
                                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as 3 HOURS EVENT and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                this.Close();
                            }
                        }
                        //3시간 Event 끝
                        else
                        {
                            //선불 시작
                            if (radioButton1.Checked) //30분
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb12 = "half an hour";
                                    Form2.frmObj.timer12.Enabled = true;
                                    Form2.frmObj.timer12.Start();
                                    Form2.frmObj.duration12 = 1800;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as paying first for half an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton2.Checked) //1시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb12 = "an hour";
                                    Form2.frmObj.timer12.Enabled = true;
                                    Form2.frmObj.timer12.Start();
                                    Form2.frmObj.duration12 = 3600;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as paying first for an hour and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }
                            if (radioButton3.Checked) //2시간
                            {
                                if (totalprice > Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received much money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice < Convert.ToInt32(textBox9.Text))
                                {
                                    MessageBox.Show("You received less money than it should be paid.\n\nPayable amount: $" + textBox9.Text, "Error !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                if (totalprice == Convert.ToInt32(textBox9.Text)) //결제성공 및 세션시작
                                {
                                    Form2.gametypetb12 = "two hours";
                                    Form2.frmObj.timer12.Enabled = true;
                                    Form2.frmObj.timer12.Start();
                                    Form2.frmObj.duration12 = 7200;
                                    decimal cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                                    decimal cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                                    decimal totalreceived = cardreceived + cashreceived;
                                    //로그 시작
                                    string savePath = @"log.txt";
                                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started as paying first for two hours and the payment ${1}(Card: ${2}, Cash: ${3}) went through successfully.\n", Form2.userinfo, totalreceived, cardreceived, cashreceived);
                                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                                    Form2.frmObj.card = Form2.frmObj.card + cardreceived;
                                    Form2.frmObj.cash = Form2.frmObj.cash + cashreceived;
                                    Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
                                    Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
                                    this.Close();
                                }
                            }

                            //선불 끝
                        }
                    }
                }
            }
            //Table 12 끝

        }





        // 후불이 선택되었을 경우 텍스트 비활성화
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label10.Visible = false;
                textBox8.Visible = false;
                textBox8.Text = "";
                textBox7.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox6.Enabled = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                groupBox1.Enabled = false;
                textBox9.Text = "0";
                price = decimal.Parse(textBox9.Text);
            }
            else
            {
                comboBox1.Enabled = true;
                comboBox1.SelectedIndex = -1;
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                textBox6.Enabled = true;
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                radioButton3.Enabled = true;
                radioButton4.Enabled = true;
                radioButton5.Enabled = true;
                radioButton6.Enabled = true;
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                groupBox1.Enabled = true;
                if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
                {
                    textBox9.Text = "21";
                    price = decimal.Parse(textBox9.Text);
                }
                else
                {
                    textBox9.Text = "18";
                    price = decimal.Parse(textBox9.Text);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "DAYTIME")
            {
                label10.Visible = true;
                textBox8.Visible = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
                textBox9.Text = "TBD";
            }
            else
            {
                label10.Visible = false;
                textBox8.Visible = false;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                radioButton3.Enabled = false;
                radioButton4.Enabled = false;
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                radioButton4.Checked = false;
                radioButton5.Checked = false;
                radioButton6.Checked = false;
            }
            if (comboBox1.Text == "3 HOURS EVENT")
            {
                if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
                {
                    textBox8.Text = "";
                    textBox9.Text = "35";
                    price = decimal.Parse(textBox9.Text);
                }
                else
                {
                    textBox8.Text = "";
                    textBox9.Text = "30";
                    price = decimal.Parse(textBox9.Text);
                }
            }
        }
        private void textBox8_KeyUp(object sender, KeyEventArgs e) //데이타임관련
        {
            if (textBox8.Text == "")
            {
                textBox8.Text = "0";
            }
            int a = (Convert.ToInt32(textBox8.Text) * 10);
            textBox9.Text = a.ToString();
            price = decimal.Parse(textBox9.Text);
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            decimal cardreceived;
            decimal cashreceived;
            decimal totalreceived;
            try
            {
                cardreceived = decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text);
                cashreceived = decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text);
                totalreceived = cardreceived + cashreceived;
                textBox7.Text = totalreceived.ToString();
                textBox10.Text = (totalreceived - decimal.Parse(textBox9.Text)).ToString();
                if (cashreceived == 0)
                {
                    textBox10.Text = "NULL";
                }
                if (textBox10.Text == "0.0")
                {
                    textBox10.Text = "0";
                }
            }
            catch { }
        }


        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
            {
                textBox9.Text = "11";
                price = decimal.Parse(textBox9.Text);
            }
            else
            {
                textBox9.Text = "9";
                price = decimal.Parse(textBox9.Text);
            }
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
            {
                textBox9.Text = "21";
                price = decimal.Parse(textBox9.Text);
            }
            else
            {
                textBox9.Text = "18";
                price = decimal.Parse(textBox9.Text);
            }
        }

        private void radioButton3_MouseClick(object sender, MouseEventArgs e)
        {
            if (tablenumber == "1" || tablenumber == "2" || tablenumber == "3" || tablenumber == "4" || tablenumber == "5" || tablenumber == "6" || tablenumber == "7")
            {
                textBox9.Text = "42";
                price = decimal.Parse(textBox9.Text);
            }
            else
            {
                textBox9.Text = "36";
                price = decimal.Parse(textBox9.Text);
            }
        }

        private void textBox1_Click_1(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox2.SelectAll();
            textBox3.SelectAll();
            textBox4.SelectAll();
            textBox5.SelectAll();
            textBox6.SelectAll();
            textBox8.SelectAll();
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (e.KeyValue == 13)
            {
                button1_Click(null, null);
            }
        }

        private void RadioButton4_MouseClick(object sender, MouseEventArgs e)
        {
            int totalpurchase = Convert.ToInt32(price * (decimal)0.9);
            textBox9.Text = (totalpurchase + Form2.passbeverage).ToString();
        }

        private void RadioButton5_MouseClick(object sender, MouseEventArgs e)
        {
            int totalpurchase = Convert.ToInt32(price * (decimal)0.8);
            textBox9.Text = (totalpurchase + Form2.passbeverage).ToString();
        }

        private void RadioButton6_MouseClick(object sender, MouseEventArgs e)
        {
            int totalpurchase = Convert.ToInt32(price * (decimal)0.7);
            textBox9.Text = (totalpurchase + Form2.passbeverage).ToString();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2.price = 0;
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == textBox9.Text)
                {
                    textBox4.Text = "0";
                }
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox1.Text = (decimal.Parse(textBox9.Text) - (decimal.Parse(textBox4.Text) + decimal.Parse(textBox5.Text) + decimal.Parse(textBox6.Text))).ToString();
            }
            catch { }
        }

        private void Label2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == textBox9.Text)
                {
                    textBox1.Text = "0";
                }
                textBox4.Text = "0";
                textBox5.Text = "0";
                textBox6.Text = "0";
                textBox4.Text = (decimal.Parse(textBox9.Text) - (decimal.Parse(textBox1.Text) + decimal.Parse(textBox2.Text) + decimal.Parse(textBox3.Text))).ToString();
            }
            catch { }
        }

    }
}