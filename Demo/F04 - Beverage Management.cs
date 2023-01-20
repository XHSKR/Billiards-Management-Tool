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
using System.Collections;

namespace Demo
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            MaximizeBox = false; // prevent users from maximising the software.
            TopMost = true; // make the form always on top.
        }

        public void readFile() //txt파일을 읽는 함수
        {
            lv.Items.Clear();
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
                    for (int j = 1; j < result.Length; j++)
                    {
                        lvi.SubItems.Add(result[j]);
                    }
                    lv.Items.Add(lvi);
                }
            }
        }

        public void saveFile() //txt파일을 저장하는 함수
        {
            const string sPath = @"beverage.txt";

            System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(sPath);
            foreach (ListViewItem item in lv.Items)
            {

                SaveFile.WriteLine(item.SubItems[1].Text + "/" + item.SubItems[2].Text + "/" + item.SubItems[3].Text + "/" + item.SubItems[4].Text + "/" + item.SubItems[5].Text);
            }
            SaveFile.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Please fill out all forms.");
                return;
            }
            try
            {
                if (lv.SelectedItems[0].SubItems[1].Text == textBox1.Text) //선택된 listview에서 변경된 수치를 row를 새로 추가하지않고 수정
                {
                    string price1 = "$" + textBox2.Text;
                    int instock1 = Convert.ToInt32(textBox3.Text);
                    int sold1 = Convert.ToInt32(textBox4.Text);
                    int currentinstock1 = instock1 - sold1;
                    if (currentinstock1 < 0)
                    {
                        MessageBox.Show(string.Format("Current stock cannot be {0}.", currentinstock1.ToString()));
                        return;
                    }
                    lv.SelectedItems[0].SubItems[2].Text = price1;
                    lv.SelectedItems[0].SubItems[3].Text = instock1.ToString();
                    lv.SelectedItems[0].SubItems[4].Text = sold1.ToString();
                    lv.SelectedItems[0].SubItems[5].Text = currentinstock1.ToString();
                    saveFile();
                    lv.Focus();
                    return;
                }
            }
            catch { }
            string price = "$" + textBox2.Text;
            int instock = Convert.ToInt32(textBox3.Text);
            if (textBox4.Text == "")
            {
                textBox4.Text = "0";
            }
            int sold = Convert.ToInt32(textBox4.Text);
            int currentinstock = instock - sold;
            if (currentinstock < 0)
            {
                MessageBox.Show(string.Format("Current stock cannot be {0}.", currentinstock.ToString()));
                return;
            }
            ListViewItem lvi = new ListViewItem(); //일반적인 row 추가
            lvi.SubItems.Add(textBox1.Text);
            lvi.SubItems.Add(price);
            lvi.SubItems.Add(instock.ToString());
            lvi.SubItems.Add(sold.ToString());
            lvi.SubItems.Add(currentinstock.ToString());
            lv.Items.Add(lvi);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
            saveFile();
        }

        private void Lv_Click(object sender, EventArgs e)
        {
            string beverage = lv.SelectedItems[0].SubItems[1].Text;
            string price = lv.SelectedItems[0].SubItems[2].Text;
            string instock = lv.SelectedItems[0].SubItems[3].Text;
            string sold = lv.SelectedItems[0].SubItems[4].Text;
            textBox1.Text = beverage;
            textBox2.Text = price.Substring(1);
            textBox3.Text = instock;
            textBox4.Text = sold;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0) //listview 미 선택시 오류를 발생시킴
            {
                MessageBox.Show("Select a beverage to delete!");
                return;
            }
            int index = lv.SelectedItems[0].Index;
            lv.Items.RemoveAt(index);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox1.Focus();
            /* 번호 조정 */
            if (index < lv.Items.Count)
            {
                for (int i = index; i < lv.Items.Count; i++)
                {
                    lv.Items[i].SubItems[0].Text = (i + 1).ToString();
                }
            }
            saveFile();
        }

        private void TextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
            {
                e.Handled = true;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            readFile();
            if (Form2.userinfo != "사장님" && Form13.isAdmin == "")
            {
                textBox4.Enabled = false;
                button2.Enabled = false;
            }

            if (Form2.isReset == "true") //음료매출 초기화
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
                foreach (ListViewItem item in lv.Items)
                {
                    item.SubItems[3] = item.SubItems[5];
                    item.SubItems[4].Text = "0";
                }
                saveFile();
                readFile();
                this.Close();
            }
        }

        private void Lv_KeyUp(object sender, KeyEventArgs e)
        {
            string beverage = lv.SelectedItems[0].SubItems[1].Text;
            string price = lv.SelectedItems[0].SubItems[2].Text;
            string instock = lv.SelectedItems[0].SubItems[3].Text;
            string sold = lv.SelectedItems[0].SubItems[4].Text;
            textBox1.Text = beverage;
            textBox2.Text = price.Substring(1);
            textBox3.Text = instock;
            textBox4.Text = sold;
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e) //Enter키 누를시 버튼1 트리거
        {
            if (e.KeyValue == 13)
            {
                Button1_Click(null, null);
            }
        }

        private void TextBox4_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
            textBox2.SelectAll();
            textBox3.SelectAll();
            textBox4.SelectAll();
        }
    }
}
