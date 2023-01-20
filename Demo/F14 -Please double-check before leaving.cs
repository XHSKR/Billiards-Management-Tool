using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Demo
{
    public partial class Form14 : Form
    {
        public Form14()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked && checkBox5.Checked && checkBox6.Checked && checkBox7.Checked && checkBox8.Checked)
            {
                Form2.frmObj.SendKatalk("퇴근해보겠습니다.");
                if (Form2.isRoomopen == "no") //카톡방이 안열려있으면
                {
                    MessageBox.Show("KakaoTalk chat room needs be launched to perform the task.");
                    Form2.isRoomopen = "";
                    return;
                }
                Process.Start("shutdown", "/s /t 1");
            }
            checkBox7.Focus();
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            if (Form2.isScheduledReset == "true")
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
                checkBox6.Checked = true;
                checkBox7.Checked = true;
                checkBox8.Checked = true;
                Button1_Click(null, null);
            }
        }


        private void CheckBox8_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 32)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
            }
        }
    }
}
