using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Demo
{
    public partial class Form1 : Form
    {
        public void readFile() //txt파일을 읽는 함수
        {
            int counter = 0;
            string line;

            // Find how many lines there are in txt file  
            System.IO.StreamReader file = new System.IO.StreamReader(@"users.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                counter++;
            }
            file.Close();
            using (var reader = new StreamReader(@"users.txt")) //text파일을 읽는다
            {
                for (int i = 0; i < counter; i++)
                {
                    string str = reader.ReadLine();
                    string[] result = str.Split(new string[] { "\n" }, StringSplitOptions.None);
                    for (int j = 0; j < result.Length; j++)
                    {
                        comboBox1.Items.Add(result[j]);
                    }
                }
            }
        }

        static Form1 _frmObj;
        public static Form1 frmObj
        {
            get { return _frmObj; }
            set { _frmObj = value; }
        }

        public Form1()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
            this.ShowInTaskbar = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            if (Form2.ifIdle == "true")
            {
                this.FormBorderStyle = FormBorderStyle.None;
                textBox1.Visible = true;
                comboBox1.Visible = false;
                label1.Text = "Password";
                textBox1.Focus();
            }
            else
            {
                comboBox1.Visible = true;
                label1.Text = "Username";
            }
            readFile();
            comboBox1.SelectedIndex = comboBox1.Items.IndexOf("사장님");
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form2.ifIdle == "true")
            {
                if (CreateMD5(textBox1.Text) == "") //unlock key
                {
                    this.Close();
                }
                else if (CreateMD5(textBox1.Text) == "") //administrator
                {
                    Opacity = 0;
                    Form13 frm13 = new Form13();
                    frm13.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect passphrase. Try again.");
                    textBox1.Clear();
                    textBox1.Focus();
                    return;
                }
            }
            else if (Form2.logstatus == "second")
            {
                Form2.userinfo = comboBox1.Text;
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] User is changed.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));


                this.Close();
            }
            else
            {
                Form2.userinfo = comboBox1.Text;
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] User is logged in.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                this.Hide();
                Form2 frm2 = new Form2(); // Form2형 frm2 인스턴스화(객체 생성)
                frm2.Passvalue = comboBox1.Text; // 전달자(Passvalue)를 통해서 Form2 로 전달
                frm2.ShowDialog();
                this.Close();
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