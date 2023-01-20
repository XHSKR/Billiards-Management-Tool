using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Demo
{
    public partial class Form13 : Form
    {
        public Form13()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            isAdmin = "true";
            Form2.frmObj.Size = new System.Drawing.Size(575, 769);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox2.Text = Form2.frmObj.card.ToString();
            textBox3.Text = Form2.frmObj.cash.ToString();
            textBox4.Text = Form2.frmObj.cashout.ToString();
            if (Form2.disableLock == "")
            {
                button2.Text = "Disable AutoLock";
            }
            else
            {
                button2.Text = "Enable AutoLock";
            }
            if (Form2.isScheduledReset == "true")
            {
                button5.Text = "Cancel";
            }
            else
            {
                button5.Text = "Auto Shutdown";
            }
        }

        private void Button25_Click(object sender, EventArgs e)
        {
            Form2.frmObj.card = decimal.Parse(textBox2.Text);
            Form2.frmObj.cash = decimal.Parse(textBox3.Text);
            Form2.frmObj.cashout = decimal.Parse(textBox4.Text);
            Form2.frmObj.total = Form2.frmObj.card + Form2.frmObj.cash + Form2.frmObj.cashout;
            Form2.frmObj.label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", Form2.frmObj.card, Form2.frmObj.cash, Form2.frmObj.cashout, Form2.frmObj.total);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration1 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb1 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb1 = "an hour";
                Form2.frmObj.timer1.Enabled = true;
                Form2.frmObj.timer1.Start();
                Form2.frmObj.duration1 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb1 = 0;
                Form2.frmObj.duration1 = 0;
                Form2.frmObj.timer1.Stop();
                Form2.frmObj.Table1.Text = "Table 1";
                Form2.gametypetb1 = "";
            }
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration2 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb2 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb2 = "an hour";
                Form2.frmObj.timer2.Enabled = true;
                Form2.frmObj.timer2.Start();
                Form2.frmObj.duration2 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb2 = 0;
                Form2.frmObj.duration2 = 0;
                Form2.frmObj.timer2.Stop();
                Form2.frmObj.Table2.Text = "Table 2";
                Form2.gametypetb2 = "";
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration3 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb3 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb3 = "an hour";
                Form2.frmObj.timer3.Enabled = true;
                Form2.frmObj.timer3.Start();
                Form2.frmObj.duration3 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb3 = 0;
                Form2.frmObj.duration3 = 0;
                Form2.frmObj.timer3.Stop();
                Form2.frmObj.Table3.Text = "Table 3";
                Form2.gametypetb3 = "";
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration4 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb4 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb4 = "an hour";
                Form2.frmObj.timer4.Enabled = true;
                Form2.frmObj.timer4.Start();
                Form2.frmObj.duration4 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb4 = 0;
                Form2.frmObj.duration4 = 0;
                Form2.frmObj.timer4.Stop();
                Form2.frmObj.Table4.Text = "Table 4";
                Form2.gametypetb4 = "";
            }
        }

        private void Button14_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration5 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb5 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb5 = "an hour";
                Form2.frmObj.timer5.Enabled = true;
                Form2.frmObj.timer5.Start();
                Form2.frmObj.duration5 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb5 = 0;
                Form2.frmObj.duration5 = 0;
                Form2.frmObj.timer5.Stop();
                Form2.frmObj.Table5.Text = "Table 5";
                Form2.gametypetb5 = "";
            }
        }

        private void Button15_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration6 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb6 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb6 = "an hour";
                Form2.frmObj.timer6.Enabled = true;
                Form2.frmObj.timer6.Start();
                Form2.frmObj.duration6 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb6 = 0;
                Form2.frmObj.duration6 = 0;
                Form2.frmObj.timer6.Stop();
                Form2.frmObj.Table6.Text = "Table 6";
                Form2.gametypetb6 = "";
            }
        }

        private void Button16_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration7 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb7 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb7 = "an hour";
                Form2.frmObj.timer7.Enabled = true;
                Form2.frmObj.timer7.Start();
                Form2.frmObj.duration7 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb7 = 0;
                Form2.frmObj.duration7 = 0;
                Form2.frmObj.timer7.Stop();
                Form2.frmObj.Table7.Text = "Table 7";
                Form2.gametypetb7 = "";
            }
        }

        private void Button17_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration8 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb8 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb8 = "an hour";
                Form2.frmObj.timer8.Enabled = true;
                Form2.frmObj.timer8.Start();
                Form2.frmObj.duration8 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb8 = 0;
                Form2.frmObj.duration8 = 0;
                Form2.frmObj.timer8.Stop();
                Form2.frmObj.Table8.Text = "Table 8";
                Form2.gametypetb8 = "";
            }
        }

        private void Button18_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration9 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb9 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb9 = "an hour";
                Form2.frmObj.timer9.Enabled = true;
                Form2.frmObj.timer9.Start();
                Form2.frmObj.duration9 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb9 = 0;
                Form2.frmObj.duration9 = 0;
                Form2.frmObj.timer9.Stop();
                Form2.frmObj.Table9.Text = "Table 9";
                Form2.gametypetb9 = "";
            }
        }

        private void Button19_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration10 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb10 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb10 = "an hour";
                Form2.frmObj.timer10.Enabled = true;
                Form2.frmObj.timer10.Start();
                Form2.frmObj.duration10 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb10 = 0;
                Form2.frmObj.duration10 = 0;
                Form2.frmObj.timer10.Stop();
                Form2.frmObj.Table10.Text = "Table 10";
                Form2.gametypetb10 = "";
            }
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration11 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb11 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb11 = "an hour";
                Form2.frmObj.timer11.Enabled = true;
                Form2.frmObj.timer11.Start();
                Form2.frmObj.duration11 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb11 = 0;
                Form2.frmObj.duration11 = 0;
                Form2.frmObj.timer11.Stop();
                Form2.frmObj.Table11.Text = "Table 11";
                Form2.gametypetb11 = "";
            }
        }

        private void Button21_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Playtime" && textBox1.Text != "")
            {
                Form2.frmObj.duration12 = Convert.ToInt32(textBox1.Text) * 60;
            }
            if (comboBox1.Text == "Beverage" && textBox1.Text != "")
            {
                Form2.beveragetb12 = decimal.Parse(textBox1.Text);
            }
            if (comboBox1.Text == "Start Upfront")
            {
                Form2.gametypetb12 = "an hour";
                Form2.frmObj.timer12.Enabled = true;
                Form2.frmObj.timer12.Start();
                Form2.frmObj.duration12 = 3600;

            }
            if (comboBox1.Text == "Cancel")
            {
                Form2.beveragetb12 = 0;
                Form2.frmObj.duration12 = 0;
                Form2.frmObj.timer12.Stop();
                Form2.frmObj.Table12.Text = "Table 12";
                Form2.gametypetb12 = "";
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Form2.formClosing = "true";
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Form2.disableLock == "")
            {
                Form2.disableLock = "true";
                button2.Text = "Enable AutoLock";
            }
            else
            {
                Form2.disableLock = "";
                button2.Text = "Disable AutoLock";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Form2.beveragetb1 = 0;
            Form2.frmObj.duration1 = 0;
            Form2.frmObj.timer1.Stop();
            Form2.frmObj.Table1.Text = "Table 1";
            Form2.gametypetb1 = "";
            Form2.beveragetb2 = 0;
            Form2.frmObj.duration2 = 0;
            Form2.frmObj.timer2.Stop();
            Form2.frmObj.Table2.Text = "Table 2";
            Form2.gametypetb2 = "";
            Form2.beveragetb3 = 0;
            Form2.frmObj.duration3 = 0;
            Form2.frmObj.timer3.Stop();
            Form2.frmObj.Table3.Text = "Table 3";
            Form2.gametypetb3 = "";
            Form2.beveragetb4 = 0;
            Form2.frmObj.duration4 = 0;
            Form2.frmObj.timer4.Stop();
            Form2.frmObj.Table4.Text = "Table 4";
            Form2.gametypetb4 = "";
            Form2.beveragetb5 = 0;
            Form2.frmObj.duration5 = 0;
            Form2.frmObj.timer5.Stop();
            Form2.frmObj.Table5.Text = "Table 5";
            Form2.gametypetb5 = "";
            Form2.beveragetb6 = 0;
            Form2.frmObj.duration6 = 0;
            Form2.frmObj.timer6.Stop();
            Form2.frmObj.Table6.Text = "Table 6";
            Form2.gametypetb6 = "";
            Form2.beveragetb7 = 0;
            Form2.frmObj.duration7 = 0;
            Form2.frmObj.timer7.Stop();
            Form2.frmObj.Table7.Text = "Table 7";
            Form2.gametypetb7 = "";
            Form2.beveragetb8 = 0;
            Form2.frmObj.duration8 = 0;
            Form2.frmObj.timer8.Stop();
            Form2.frmObj.Table8.Text = "Table 8";
            Form2.gametypetb8 = ""; Form2.beveragetb9 = 0;
            Form2.frmObj.duration9 = 0;
            Form2.frmObj.timer9.Stop();
            Form2.frmObj.Table9.Text = "Table 9";
            Form2.gametypetb9 = "";
            Form2.beveragetb10 = 0;
            Form2.frmObj.duration10 = 0;
            Form2.frmObj.timer10.Stop();
            Form2.frmObj.Table10.Text = "Table 10";
            Form2.gametypetb10 = "";
            Form2.beveragetb11 = 0;
            Form2.frmObj.duration11 = 0;
            Form2.frmObj.timer11.Stop();
            Form2.frmObj.Table11.Text = "Table 11";
            Form2.gametypetb11 = "";
            Form2.beveragetb12 = 0;
            Form2.frmObj.duration12 = 0;
            Form2.frmObj.timer12.Stop();
            Form2.frmObj.Table12.Text = "Table 12";
            Form2.gametypetb12 = "";
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            Form2.isReset2 = "";
            Form2.frmObj.Table1.Enabled = true;
            Form2.frmObj.Table2.Enabled = true;
            Form2.frmObj.Table3.Enabled = true;
            Form2.frmObj.Table4.Enabled = true;
            Form2.frmObj.Table5.Enabled = true;
            Form2.frmObj.Table6.Enabled = true;
            Form2.frmObj.Table7.Enabled = true;
            Form2.frmObj.Table8.Enabled = true;
            Form2.frmObj.Table9.Enabled = true;
            Form2.frmObj.Table10.Enabled = true;
            Form2.frmObj.Table11.Enabled = true;
            Form2.frmObj.Table12.Enabled = true;
            Form2.frmObj.groupBox2.Enabled = true;
            Form2.frmObj.groupBox3.Enabled = true;
            Form2.frmObj.groupBox4.Enabled = true;
            Form2.frmObj.button14.Enabled = true;
            try
            {
                Directory.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + Form2.todayDate, true);
            }
            catch
            {
                MessageBox.Show("The folder does not exist.");
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            Form2.sendAgain = "true";
        }

        public static string isAdmin = "";
        private void Button6_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }

        private void Form13_FormClosing(object sender, FormClosingEventArgs e)
        {
            isAdmin = "";
        }

        private void TextBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
            textBox3.SelectAll();
            textBox4.SelectAll();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Form2.frmObj.TopMost = false; // make the form always on top.
        }

        private void Button24_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"beverage.txt");
        }

        private void Button23_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"log.txt");
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"users.txt");
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
        }

        public static string EndDate2;

        private void Button5_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            string random1 = r.Next(45, 50).ToString();
            string random2 = r.Next(0, 59).ToString();
            DateTime StartDate = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
            DateTime ShutdownDate = Convert.ToDateTime("23:45:00");
            DateTime EndDate = Convert.ToDateTime("23:" + random1 + ":" + random2);
            TimeSpan dateDiff = EndDate - StartDate;
            int diffHour = dateDiff.Hours;
            int diffMinute = dateDiff.Minutes;
            int diffSecond = dateDiff.Seconds;
            int totaldiff = (diffHour * 3600) + (diffMinute * 60) + diffSecond;

            int value = DateTime.Compare(StartDate, ShutdownDate);
            if (value > 0)
            {
                MessageBox.Show("Unable to schedule the task since you are already able to shutdown.\nInstead, The system has scheduled the task to run in 1 second.");
                Form2.frmObj.timer16.Enabled = true;
                Form2.frmObj.timer16.Start();
                Form2.frmObj.duration13 = 1;
                return;
            }
            if (Form2.isScheduledReset == "")
            {
                EndDate2 = "11:" + random1 + ":" + random2;
                MessageBox.Show(string.Format("Current Time: {0}\nTargeted Time: {1}\nTime Difference: {2}", StartDate, EndDate, totaldiff));
                Form2.isScheduledReset = "true";
                Form2.frmObj.timer16.Enabled = true;
                Form2.frmObj.timer16.Start();
                Form2.frmObj.duration13 = totaldiff;
                button5.Text = "Cancel";
            }
            else
            {
                Form2.frmObj.timer16.Stop();
                Form2.isScheduledReset = "";
                Form2.frmObj.label17.Text = DateTime.Now.ToString("yyyy.MM.dd");
                button5.Text = "Auto Shutdown";
            }
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Start Upfront" || comboBox1.Text == "Cancel")
            {
                label4.Enabled = false;
                textBox1.Enabled = false;
            }
            else
            {
                label4.Enabled = true;
                textBox1.Enabled = true;
            }
        }

        private void TextBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

    }
}
