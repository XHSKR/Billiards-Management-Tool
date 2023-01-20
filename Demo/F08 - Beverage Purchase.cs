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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        public void readFile() //txt파일을 읽는 함수
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            int counter = 0;
            string line;

            // Find how many lines there are in txt file  
            System.IO.StreamReader file = new System.IO.StreamReader(@"beverage.txt");
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                counter++;
            }
            file.Close();
            using (var reader = new StreamReader(@"beverage.txt")) //text파일을 읽는다
            {
                for (int i = 0; i < counter; i++)
                {
                    string str = reader.ReadLine();
                    string[] result = str.Split(new char[] { '/' });
                    ListViewItem lvi = new ListViewItem();
                    for (int j = 0; j < result.Length; j++)
                    {
                        lvi.SubItems.Add(result[j]);
                    }
                    listView1.Items.Add(lvi);
                }
            }
        }

        public void saveFile() //txt파일을 저장하는 함수
        {
            const string sPath = @"beverage.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (ListViewItem item in listView1.Items)
            {

                SaveFile.WriteLine(item.SubItems[1].Text + "/" + item.SubItems[2].Text + "/" + item.SubItems[3].Text + "/" + item.SubItems[4].Text + "/" + item.SubItems[5].Text);
            }
            SaveFile.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) //listview 미 선택시 오류를 발생시킴
            {
                MessageBox.Show("Select a beverage to purchase.");
                return;
            }
            if (listView1.SelectedItems.Count > 0)
            {
                ListView1_DoubleClick(null, null);
            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            string beverage = listView1.SelectedItems[0].SubItems[1].Text;
            string price = listView1.SelectedItems[0].SubItems[2].Text;
            int sold = Convert.ToInt32(listView1.SelectedItems[0].SubItems[4].Text);
            int currentinstock = Convert.ToInt32(listView1.SelectedItems[0].SubItems[5].Text);
            if (currentinstock <= 0)
            {
                MessageBox.Show("You can't add more.");
                return;
            }
            sold++;
            currentinstock--;
            listView1.SelectedItems[0].SubItems[4].Text = sold.ToString();
            listView1.SelectedItems[0].SubItems[5].Text = currentinstock.ToString();
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(beverage);
            lvi.SubItems.Add(price);
            listView2.Items.Add(lvi);

            decimal totalprice = 0;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                totalprice += decimal.Parse(listView2.Items[i].SubItems[2].Text.Substring(1));
            }
            textBox1.Text = totalprice.ToString();
        }

        public static string beveragepurchase;
        public static decimal beverageprice = 0;

        private void Button3_Click(object sender, EventArgs e) //총 결제금액 계산
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You've selected nothing.");
                return;
            }
            decimal totalprice = 0;
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                totalprice += decimal.Parse(listView2.Items[i].SubItems[2].Text.Substring(1));
            }

            if (Form2.beveragepaytb1 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb1 = Form2.beveragetb1 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 1.\n", Form2.userinfo, Form2.beveragetb1);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb2 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb2 = Form2.beveragetb2 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 2.\n", Form2.userinfo, Form2.beveragetb2);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb3 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb3 = Form2.beveragetb3 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 3.\n", Form2.userinfo, Form2.beveragetb3);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb4 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb4 = Form2.beveragetb4 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 4.\n", Form2.userinfo, Form2.beveragetb4);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb5 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb5 = Form2.beveragetb5 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 5.\n", Form2.userinfo, Form2.beveragetb5);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb6 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb6 = Form2.beveragetb6 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 6.\n", Form2.userinfo, Form2.beveragetb6);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb7 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb7 = Form2.beveragetb7 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 7.\n", Form2.userinfo, Form2.beveragetb7);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb8 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb8 = Form2.beveragetb8 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 8.\n", Form2.userinfo, Form2.beveragetb8);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb9 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb9 = Form2.beveragetb9 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 9.\n", Form2.userinfo, Form2.beveragetb9);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb10 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb10 = Form2.beveragetb10 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 10.\n", Form2.userinfo, Form2.beveragetb10);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb11 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb11 = Form2.beveragetb11 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 11.\n", Form2.userinfo, Form2.beveragetb11);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }
            if (Form2.beveragepaytb12 == "yes")
            {
                DialogResult dialogResult1 = MessageBox.Show("The beverage purchase $" + totalprice + " will be added to the table.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult1 == DialogResult.Yes)
                {
                    beverageprice = totalprice;
                    Form2.beveragetb12 = Form2.beveragetb12 + totalprice;
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage purchase ${1} is applied to Table 12.\n", Form2.userinfo, Form2.beveragetb12);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                    saveFile();
                    this.Close();
                    return;
                }
                if (dialogResult1 == DialogResult.No)
                {
                    return;
                }
            }


            DialogResult dialogResult = MessageBox.Show("The total payable amount is $" + totalprice + ".\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                beverageprice = totalprice;
                beveragepurchase = "yes";
                Form3 frm3 = new Form3();
                frm3.checkBox1.Visible = false;
                frm3.comboBox1.Visible = false;
                frm3.label10.Visible = false;
                frm3.textBox8.Visible = false;
                frm3.radioButton1.Visible = false;
                frm3.radioButton2.Visible = false;
                frm3.radioButton3.Visible = false;
                frm3.groupBox1.Enabled = false;
                frm3.ShowInTaskbar = false;

                frm3.Passvalue = totalprice.ToString();
                frm3.ShowDialog();
                saveFile();
                this.Close();
            }
            if (dialogResult == DialogResult.No)
            {
                return;
            }

        }
        private void Form8_Load(object sender, EventArgs e)
        {
            readFile();
            if (Form2.beveragepaytb1 != "" || Form2.beveragepaytb2 != "" || Form2.beveragepaytb3 != "" || Form2.beveragepaytb4 != "" || Form2.beveragepaytb5 != "" || Form2.beveragepaytb6 != ""
            || Form2.beveragepaytb7 != "" || Form2.beveragepaytb8 != "" || Form2.beveragepaytb9 != "" || Form2.beveragepaytb10 != "" || Form2.beveragepaytb11 != "" || Form2.beveragepaytb12 != "")
            {
                button1.Enabled = false;
            }
            if (Form13.isAdmin == "true")
            {
                button3.Enabled = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            readFile();
        }


        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2.beveragepaytb1 = "";
            Form2.beveragepaytb2 = "";
            Form2.beveragepaytb3 = "";
            Form2.beveragepaytb4 = "";
            Form2.beveragepaytb5 = "";
            Form2.beveragepaytb6 = "";
            Form2.beveragepaytb7 = "";
            Form2.beveragepaytb8 = "";
            Form2.beveragepaytb9 = "";
            Form2.beveragepaytb10 = "";
            Form2.beveragepaytb11 = "";
            Form2.beveragepaytb2 = "";
        }

        private void ListView1_KeyDown(object sender, KeyEventArgs e) //Enter키 누를 시 button1_click 트리거
        {
            if (e.KeyValue == 13)
            {
                Button3_Click(null, null);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You've selected nothing.");
                return;
            }
            DialogResult dialogResult1 = MessageBox.Show("It will not be charged for the beverage you have selected.\nIf clicked yes, it cannot be reverted. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult1 == DialogResult.Yes)
            {
                decimal totalprice = 0;
                for (int i = 0; i < listView2.Items.Count; i++)
                {
                    totalprice += decimal.Parse(listView2.Items[i].SubItems[2].Text.Substring(1));
                }
                if (Form13.isAdmin != "true")
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Beverage ${1} is served at no charge.\n", Form2.userinfo, totalprice);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                }
                saveFile();
                this.Close();
                return;
            }
            if (dialogResult1 == DialogResult.No)
            {
                return;
            }
        }
    }
}

