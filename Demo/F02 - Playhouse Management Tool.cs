using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;

namespace Demo
{
    public partial class Form2 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int FindWindowEx(int hWnd1, int hWnd2, string lpsz1, string lpsz2);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int SendMessage(int hwnd, int wMsg, int wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint PostMessage(int hwnd, int wMsg, int wParam, int lParam);
        public static string katalkFocus = "";
        public static string isRoomopen = "";
        public static string katalkRoom = "Playhouse Chat"; //Playhouse Chat
        public void SendKatalk(string msg)
        {
            katalkFocus = "true";
            Form11 f11 = new Form11();
            f11.ShowDialog();
            katalkFocus = "";
            int hd01 = FindWindow(null, katalkRoom);
            int hd03 = FindWindowEx(hd01, 0, "RichEdit20W", "");
            SendMessage(hd03, 0x000c, 0, msg);
            PostMessage(hd03, 0x0100, 0xD, 0x1C001);
            Thread.Sleep(50);
            PostMessage(hd03, 0x0100, 0xD, 0x1C001);
        }
        public static string disableLock = "";
        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (ifIdle == "true" && disableLock == "")
            {
                MessageBox.Show(string.Format("You were away for {0} minutes.\nThe system has timed out after 20 minutes of inactivity for security.", awaytime / 60));
                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] The system timed out the user due to inactivity. (Inactivity time: {1} minutes.)\n", Form2.userinfo, awaytime / 60);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                button14_Click(null, null);
                ifIdle = "";
            }
        }
        public static string ifIdle;
        // inactivity time 감지 시작
        int totaltime = 0;
        int awaytime = 0;
        LASTINPUTINFO lastInputInf = new LASTINPUTINFO();
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public int dwTime;
        }
        public void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DisplayTime();
        }
        public int GetLastInputTime()
        {
            int idletime = 0;
            idletime = 0;
            lastInputInf.cbSize = Marshal.SizeOf(lastInputInf);
            lastInputInf.dwTime = 0;

            if (GetLastInputInfo(ref lastInputInf))
            {
                idletime = Environment.TickCount - lastInputInf.dwTime;
            }

            if (idletime != 0)
            {
                return idletime / 1000;
            }
            else
            {
                return 0;
            }
        }
        private void DisplayTime()
        {
            totaltime = GetLastInputTime();
            if (totaltime >= 1200) //20분 = 1200
            {
                awaytime = totaltime;
                ifIdle = "true";
            }
        }
        // inactivity time 감지 끝
        //Form6에서 데이터전달받음
        private string Form2_value;
        public string Passvalue
        {
            get { return this.Form2_value; } // Form2에서 얻은(get) 값을 다른폼(Form1)으로 전달 목적
            set { this.Form2_value = value; }  // 다른폼(Form1)에서 전달받은 값을 쓰기
        }

        static Form2 _frmObj;
        public static Form2 frmObj
        {
            get { return _frmObj; }
            set { _frmObj = value; }
        }

        public Form2()
        {
            InitializeComponent();
            InitBrowser();
            MaximizeBox = false; // prevent users from maximising the software.
            //TopMost = true; // make the form always on top.
        }

        public ChromiumWebBrowser browser;
        public void InitBrowser()
        {
            Cef.Initialize(new CefSettings());
            browser = new ChromiumWebBrowser("http://192.168.1.3:8080/stream/video/mjpeg?resolution=VGA&&Username=admin&&Password=cGxheTYxMTI=&");
            this.panel1.Controls.Add(browser);
            browser.Dock = DockStyle.Fill;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            browser.Load("http://192.168.1.3:8080/stream/video/mjpeg?resolution=VGA&&Username=admin&&Password=cGxheTYxMTI=&");
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label18.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            browser.Load("http://192.168.1.11:8080/stream/video/mjpeg?resolution=VGA&&Username=admin&&Password=cGxheTYxMTI=&");
            label12.Visible = true;
            label13.Visible = true;
            label14.Visible = true;
            label15.Visible = true;
            label16.Visible = true;
            label18.Visible = true;
        }
        public static string todayDate;
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(575, 769);
            label1.Text = "You have been logged in as " + Passvalue; //다른폼(Form1)에서 전달받은 값을 변수에 저장
            frmObj = this;
            timer15.Start();
            //try
            //{
            //    string name = GetHtmlString("http://playhousecam.tistory.com");
            //    if (name == "valid</b></b></b></b></b></b></b></b></b></b></b></b></b>") //라이센스 검증 통과
            //    {
            //        //License is valid, do nothing
            //    }
            //    else if (name == "invalid</b></b></b></b></b></b></b></b></b></b></b></b></b>") //라이센스 검증 실패
            //    {
            //        MessageBox.Show("XHSKR authentication server has indicated your license is invalid.");
            //        formClosing = "true";
            //        this.Close();
            //    }
            //    else //통과나 실패가 아닐경우
            //    {
            //        MessageBox.Show("Received unidentified authorisation codes from XHSKR authentication server.");
            //        formClosing = "true";
            //        this.Close();
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("Unable to reach XHSKR authentication server.\nTry again in a few minutes.");
            //    formClosing = "true";
            //    this.Close();
            //}

            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label18.Visible = false;
            label17.Text = DateTime.Now.ToString("yyyy.MM.dd");
            todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                //announcement파일 읽기
                string textValue = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "announcement.txt", Encoding.GetEncoding("UTF-8"));
                label4.Text = textValue;
                //income파일 읽기
                using (var reader = new StreamReader(@"income.txt")) //text파일을 읽는다
                {
                    card = decimal.Parse(reader.ReadLine()); //첫째 줄 숫자를 card로 저장
                    cash = decimal.Parse(reader.ReadLine()); //둘째 줄 숫자를 cash로 저장
                    cashout = decimal.Parse(reader.ReadLine());
                    total = card + cash + cashout; //넷째 줄에도 integer가 있으나 calculation으로 해도 무방
                }
            }
            catch //실패시 파일생성
            {
                //income.txt 파일저장
                string savePath = @"income.txt";
                string textValue = string.Format("0\n0\n0\n0");
                System.IO.File.WriteAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
                //announcement.txt 파일저장
                string savePath2 = @"announcement.txt";
                string textValue2 = string.Format("Currently there are no announcements.");
                System.IO.File.WriteAllText(savePath2, textValue2, Encoding.GetEncoding("UTF-8"));
                MessageBox.Show("File is missing or corrupted and the system tried to fix the issue.\nPlease restart the program.");
                System.Environment.Exit(0);
            }
            label2.Text = string.Format("Card ${0} + Cash ${1} + Cash Out ${2} = Total: ${3}", card, cash, cashout, total);
            timer13_Tick(sender, e);
            timer13.Interval = 5000; //5초마다 card, cash, cashout, total을 파일(income.txt)에 저장
            timer13.Start(); //타이머를 발동시킨다.
            timer14.Start();
            DispatcherTimer dt = new DispatcherTimer();
            dt.Tick += dispatcherTimer_Tick;
            dt.Interval = new TimeSpan(0, 0, 1);
            dt.Start();
            toolTip13.AutoPopDelay = 32767;
        }
        private string GetHtmlString(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.Default);
            string strHtml = reader.ReadToEnd();
            reader.Close();
            response.Close();
            return strHtml;
        }

        public decimal card = 0;
        public decimal cash = 0;
        public decimal cashout = 0;
        public decimal total = 0;

        public static string logstatus;

        private void Button8_Click(object sender, EventArgs e) //Modify Income
        {
            Form10 f10 = new Form10();
            f10.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e) //Change Announcement
        {
            Form6 f6 = new Form6();
            f6.ShowDialog();
            label4.Text = f6.Passvalue;
        }
        private void button16_Click(object sender, EventArgs e) //Beverage Purchase
        {
            Form8 f8 = new Form8();
            f8.ShowDialog();
        }
        private void button15_Click(object sender, EventArgs e) //Show Logs
        {
            Form12 f12 = new Form12();
            f12.ShowDialog();
        }
        private void Button13_Click(object sender, EventArgs e) //Manage Stocks
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
        private void Button7_Click(object sender, EventArgs e) //Cash Out
        {
            Form9 f9 = new Form9();
            f9.ShowDialog();
        }
        private void Button6_Click(object sender, EventArgs e) //Launch Excel
        {

            if (Convert.ToInt32(liveTime.Substring(0, 2)) >= 12 && Convert.ToInt32(liveTime.Substring(0, 2)) < 18)
            {
                Clipboard.SetText("12:00pm ~ 6:00pm	6 " + userinfo);
                System.Diagnostics.Process.Start("C:/Users/Playhouse/OneDrive/Documents/PlayHouse 직원 Working Times.xlsx");
            }
            else
            {
                //Clipboard.SetText("6:00pm ~ 12:00	6 " + userinfo);
                if (cashout == 0)
                {
                    Clipboard.SetText(string.Format("${0}	${1}", card, cash));
                }
                else
                {
                    Clipboard.SetText(string.Format("${0}	${1}	${2}", card, cash, cashout));
                }
                //code below is obsolete because you no longer have to see and compare income of the day
                //TopMost = true; // make the form always on top.
                //this.Location = new Point(300, 850); //위치변경
                System.Diagnostics.Process.Start("C:/Users/Playhouse/OneDrive/Documents/PlayHouse 직원 Working Times.xlsx");
                System.Diagnostics.Process.Start("C:/Users/Playhouse/OneDrive/Documents/Playhouse 매출표.xlsx");
            }
        }
        private void button14_Click(object sender, EventArgs e) //Switch User
        {
            logstatus = "second";
            Form1 f1 = new Form1();
            f1.ShowDialog();
            Passvalue = userinfo;
            label1.Text = "You have been logged in as " + Passvalue; // 다른폼(Form1)에서 전달받은 값을 변수에 저장
        }

        private void button3_Click(object sender, EventArgs e) //Surveil Camera
        {
            if (this.Size == new System.Drawing.Size(1272, 769))
            {
                this.Size = new System.Drawing.Size(575, 769);
            }
            else
                this.Size = new System.Drawing.Size(1272, 769);
        }
        public void Reset()
        {
            //텍스트파일 이동
            string today = todayDate;
            string fileToCopy = @"log.txt";
            string newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + today + @"\log.txt";
            string folderLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + today;
            if (Directory.Exists(folderLocation))
            {
                MessageBox.Show(string.Format("Error ! The file already exists. Please check if you've already performed this task today and make sure nothing on the following folder.\n{0}", folderLocation));
                return;
            }
            else
            {
                // 카톡전송
                string report;
                if (cashout == 0)
                {
                    report = string.Format("마감보고입니다.\n토탈: ${0}\n카드: ${1}\n현금: ${2}", total, card, cash, cashout);
                }
                else
                {
                    report = string.Format("마감보고입니다.\n토탈: ${0}\n카드: ${1}\n현금: ${2}\n캐시아웃: ${3}", total, card, cash, cashout);
                }
                SendKatalk(report);
                if (isRoomopen == "no") //카톡방이 안열려있으면
                {
                    MessageBox.Show("KakaoTalk chat room needs be launched to perform the task.");
                    isRoomopen = "";
                    return;
                }

                //로그 시작
                string savePath1 = @"log.txt";
                string textValue1 = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Resetting the income of the day is performed.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath1, textValue1, Encoding.GetEncoding("UTF-8"));

                DirectoryInfo di = Directory.CreateDirectory(folderLocation);
                File.Move(fileToCopy, newLocation);
            }

            fileToCopy = @"income.txt";
            newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + today + @"\income.txt";
            if (Directory.Exists(folderLocation))
            {
                if (File.Exists(fileToCopy))
                {
                    File.Copy(fileToCopy, newLocation, true);
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(folderLocation);
                File.Copy(fileToCopy, newLocation, true);
            }

            fileToCopy = @"announcement.txt";
            newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + today + @"\announcement.txt";
            if (Directory.Exists(folderLocation))
            {
                if (File.Exists(fileToCopy))
                {
                    File.Copy(fileToCopy, newLocation, true);
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(folderLocation);
                File.Copy(fileToCopy, newLocation, true);
            }

            fileToCopy = @"beverage.txt";
            newLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Playhouse\" + today + @"\beverage.txt";
            if (Directory.Exists(folderLocation))
            {
                if (File.Exists(fileToCopy))
                {
                    File.Copy(fileToCopy, newLocation, true);
                }
            }
            else
            {
                DirectoryInfo di = Directory.CreateDirectory(folderLocation);
                File.Copy(fileToCopy, newLocation, true);
            }
            // 음료초기화
            isReset = "true";
            isReset2 = "true";
            Form4 f4 = new Form4();
            f4.ShowDialog();
            f4.Hide();
            isReset = "";
            // 매출초기화
            string savePath = @"income.txt";
            label2.Text = "Card $0 + Cash $0 + Cash Out $0 = Total: $0";
            string textValue = "0\n0\n0";
            cash = 0;
            card = 0;
            cashout = 0;
            total = 0;
            System.IO.File.WriteAllText(savePath, textValue, Encoding.GetEncoding("ks_c_5601-1987"));
            this.Size = new System.Drawing.Size(575, 769);

            Table1.Enabled = false;
            Table2.Enabled = false;
            Table3.Enabled = false;
            Table4.Enabled = false;
            Table5.Enabled = false;
            Table6.Enabled = false;
            Table7.Enabled = false;
            Table8.Enabled = false;
            Table9.Enabled = false;
            Table10.Enabled = false;
            Table11.Enabled = false;
            Table12.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            button14.Enabled = false;

            Form14 f14 = new Form14();
            f14.ShowDialog();
        }
        public static string isScheduledReset = "";
        private void button2_Click(object sender, EventArgs e) //Reset
        {
            string incomeSavePath = @"income.txt";
            string incomeSave = string.Format("{0}\n{1}\n{2}", card, cash, cashout);
            try //간혹 5초마다 자동으로 저장되는 타이머랑 겹치면 오류가 발생하기 때문에 예외처리
            {
                System.IO.File.WriteAllText(incomeSavePath, incomeSave, Encoding.GetEncoding("ks_c_5601-1987"));
            }
            catch { }
            if (isScheduledReset == "true")
            {
                Reset();
                return;
            }
            if (isReset2 == "true")
            {
                Form14 f14 = new Form14();
                f14.ShowDialog();
                return;
            }
            DialogResult dialogResult = MessageBox.Show("This will perform resetting the income of the today. Are you sure to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Reset();
            }
            else
            {
                return;
            }
        }
        private void timer13_Tick(object sender, EventArgs e) //income을 txt파일에 저장
        {
            try
            {
                string savePath = @"income.txt";
                string textValue = string.Format("{0}\n{1}\n{2}", card, cash, cashout);
                System.IO.File.WriteAllText(savePath, textValue, Encoding.GetEncoding("ks_c_5601-1987"));
            }
            catch { }
        }


        DateTime dt;

        public static string userinfo;

        public static int price;

        public static decimal passbeverage;

        public static string isReset;
        public static string isReset2;

        public static decimal beveragetb1 = 0;
        public static decimal beveragetb2 = 0;
        public static decimal beveragetb3 = 0;
        public static decimal beveragetb4 = 0;
        public static decimal beveragetb5 = 0;
        public static decimal beveragetb6 = 0;
        public static decimal beveragetb7 = 0;
        public static decimal beveragetb8 = 0;
        public static decimal beveragetb9 = 0;
        public static decimal beveragetb10 = 0;
        public static decimal beveragetb11 = 0;
        public static decimal beveragetb12 = 0;

        public static string beveragepaytb1 = "";
        public static string beveragepaytb2 = "";
        public static string beveragepaytb3 = "";
        public static string beveragepaytb4 = "";
        public static string beveragepaytb5 = "";
        public static string beveragepaytb6 = "";
        public static string beveragepaytb7 = "";
        public static string beveragepaytb8 = "";
        public static string beveragepaytb9 = "";
        public static string beveragepaytb10 = "";
        public static string beveragepaytb11 = "";
        public static string beveragepaytb12 = "";

        public static string modifytime = "";

        public static string gametypetb1 = "";
        public static string gametypetb2 = "";
        public static string gametypetb3 = "";
        public static string gametypetb4 = "";
        public static string gametypetb5 = "";
        public static string gametypetb6 = "";
        public static string gametypetb7 = "";
        public static string gametypetb8 = "";
        public static string gametypetb9 = "";
        public static string gametypetb10 = "";
        public static string gametypetb11 = "";
        public static string gametypetb12 = "";

        public int duration1;
        public int duration2;
        public int duration3;
        public int duration4;
        public int duration5;
        public int duration6;
        public int duration7;
        public int duration8;
        public int duration9;
        public int duration10;
        public int duration11;
        public int duration12;

        public int duration13;

        public static string tablememo1 = "";
        public static string tablememo2 = "";
        public static string tablememo3 = "";
        public static string tablememo4 = "";
        public static string tablememo5 = "";
        public static string tablememo6 = "";
        public static string tablememo7 = "";
        public static string tablememo8 = "";
        public static string tablememo9 = "";
        public static string tablememo10 = "";
        public static string tablememo11 = "";
        public static string tablememo12 = "";

        public static string tablememoindicator = "";

        //Table 1 우클릭 시작
        private void 일시정지ToolStripMenuItem_Click(object sender, EventArgs e) //일시정지
        {
            if (gametypetb1 != "")
            {
                if (timer1.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer1.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }
        private void 음료추가ToolStripMenuItem_Click(object sender, EventArgs e) //음료추가
        {
            if (gametypetb1 == "paying after")
            {
                beveragepaytb1 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }
        private void 시간수동입력ToolStripMenuItem_Click(object sender, EventArgs e) //시간수동입력
        {
            if (gametypetb1 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb1 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "1";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }
        private void memoToolStripMenuItem_Click(object sender, EventArgs e) //메모
        {
            tablememoindicator = "1";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }
        private void StartFreeSessionToolStripMenuItem_Click(object sender, EventArgs e) //서비스세션 시작
        {
            if (gametypetb1 == "")
            {
                gametypetb1 = "service session";
                timer1.Enabled = true;
                duration1 = 0;
                timer1.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.SetToolTip(Table1, tablememo1);
        }
        //Table 1 우클릭 끝

        //Table 2 우클릭 시작
        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (gametypetb2 != "")
            {
                if (timer2.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer2.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer2.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (gametypetb2 == "paying after")
            {
                beveragepaytb2 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (gametypetb2 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb2 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "2";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            tablememoindicator = "2";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (gametypetb2 == "")
            {
                gametypetb2 = "service session";
                timer2.Enabled = true;
                duration2 = 0;
                timer2.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table2_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip2.SetToolTip(Table2, tablememo2);

        }
        // Table 2 우클릭 끝

        //Table 3 우클릭 시작
        private void ToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (gametypetb3 != "")
            {
                if (timer3.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer3.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer3.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (gametypetb3 == "paying after")
            {
                beveragepaytb3 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            if (gametypetb3 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb3 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "3";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            tablememoindicator = "3";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            if (gametypetb3 == "")
            {
                gametypetb3 = "service session";
                timer3.Enabled = true;
                duration3 = 0;
                timer3.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table3_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip3.SetToolTip(Table3, tablememo3);
        }
        //Table 3 우클릭 끝

        //Table 4 우클릭 시작
        private void ToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            if (gametypetb4 != "")
            {
                if (timer4.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer4.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer4.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            if (gametypetb4 == "paying after")
            {
                beveragepaytb4 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (gametypetb4 == "paying after")
            {
                beveragepaytb4 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem19_Click(object sender, EventArgs e)
        {
            tablememoindicator = "4";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem20_Click(object sender, EventArgs e)
        {
            if (gametypetb4 == "")
            {
                gametypetb4 = "service session";
                timer4.Enabled = true;
                duration4 = 0;
                timer4.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table4_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip4.SetToolTip(Table4, tablememo4);
        }
        //Table 4 우클릭 끝

        //Table 5 우클릭 시작
        private void ToolStripMenuItem21_Click(object sender, EventArgs e)
        {
            if (gametypetb5 != "")
            {
                if (timer5.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer5.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer5.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem22_Click(object sender, EventArgs e)
        {
            if (gametypetb5 == "paying after")
            {
                beveragepaytb5 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem23_Click(object sender, EventArgs e)
        {
            if (gametypetb5 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb5 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "5";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem24_Click(object sender, EventArgs e)
        {
            tablememoindicator = "5";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem25_Click(object sender, EventArgs e)
        {
            if (gametypetb5 == "")
            {
                gametypetb5 = "service session";
                timer5.Enabled = true;
                duration5 = 0;
                timer5.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table5_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip5.SetToolTip(Table5, tablememo5);
        }
        //Table 5 우클릭 끝

        //Table 6 우클릭 시작
        private void ToolStripMenuItem26_Click(object sender, EventArgs e)
        {
            if (gametypetb6 != "")
            {
                if (timer6.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer6.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer6.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem27_Click(object sender, EventArgs e)
        {
            if (gametypetb6 == "paying after")
            {
                beveragepaytb6 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem28_Click(object sender, EventArgs e)
        {
            if (gametypetb6 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb6 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "6";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem29_Click(object sender, EventArgs e)
        {
            tablememoindicator = "6";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem30_Click(object sender, EventArgs e)
        {
            if (gametypetb6 == "")
            {
                gametypetb6 = "service session";
                timer6.Enabled = true;
                duration6 = 0;
                timer6.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table6_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip6.SetToolTip(Table6, tablememo6);
        }
        //Table 6 우클릭 끝

        //Table 7 우클릭 시작
        private void ToolStripMenuItem31_Click(object sender, EventArgs e)
        {
            if (gametypetb7 != "")
            {
                if (timer7.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer7.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer7.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem32_Click(object sender, EventArgs e)
        {
            if (gametypetb7 == "paying after")
            {
                beveragepaytb7 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem33_Click(object sender, EventArgs e)
        {
            if (gametypetb7 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb7 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "7";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem34_Click(object sender, EventArgs e)
        {
            tablememoindicator = "7";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem35_Click(object sender, EventArgs e)
        {
            if (gametypetb7 == "")
            {
                gametypetb7 = "service session";
                timer7.Enabled = true;
                duration7 = 0;
                timer7.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table7_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip7.SetToolTip(Table7, tablememo7);
        }
        //Table 7 우클릭 끝

        //Table 8 우클릭 시작
        private void ToolStripMenuItem36_Click(object sender, EventArgs e)
        {
            if (gametypetb8 != "")
            {
                if (timer8.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer8.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer8.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem37_Click(object sender, EventArgs e)
        {
            if (gametypetb8 == "paying after")
            {
                beveragepaytb8 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem38_Click(object sender, EventArgs e)
        {
            if (gametypetb8 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb8 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "8";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem39_Click(object sender, EventArgs e)
        {
            tablememoindicator = "8";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem40_Click(object sender, EventArgs e)
        {
            if (gametypetb8 == "")
            {
                gametypetb8 = "service session";
                timer8.Enabled = true;
                duration8 = 0;
                timer8.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table8_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip8.SetToolTip(Table8, tablememo8);
        }
        //Table 8 우클릭 끝

        //Table 9 우클릭 시작
        private void ToolStripMenuItem41_Click(object sender, EventArgs e)
        {
            if (gametypetb9 != "")
            {
                if (timer9.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer9.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer9.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem42_Click(object sender, EventArgs e)
        {
            if (gametypetb9 == "paying after")
            {
                beveragepaytb9 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem43_Click(object sender, EventArgs e)
        {
            if (gametypetb9 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb9 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "9";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem44_Click(object sender, EventArgs e)
        {
            tablememoindicator = "9";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem45_Click(object sender, EventArgs e)
        {
            if (gametypetb9 == "")
            {
                gametypetb9 = "service session";
                timer9.Enabled = true;
                duration9 = 0;
                timer9.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table9_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip9.SetToolTip(Table9, tablememo9);
        }
        //Table 9 우클릭 끝

        //Table 10 우클릭 시작
        private void ToolStripMenuItem46_Click(object sender, EventArgs e)
        {
            if (gametypetb10 != "")
            {
                if (timer10.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer10.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer10.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem47_Click(object sender, EventArgs e)
        {
            if (gametypetb10 == "paying after")
            {
                beveragepaytb10 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem48_Click(object sender, EventArgs e)
        {
            if (gametypetb10 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb10 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "10";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem49_Click(object sender, EventArgs e)
        {
            tablememoindicator = "10";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem50_Click(object sender, EventArgs e)
        {
            if (gametypetb10 == "")
            {
                gametypetb10 = "service session";
                timer10.Enabled = true;
                duration10 = 0;
                timer10.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table10_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip10.SetToolTip(Table10, tablememo10);
        }
        //Table 10 우클릭 끝

        //Table 11 우클릭 시작
        private void ToolStripMenuItem51_Click(object sender, EventArgs e)
        {
            if (gametypetb11 != "")
            {
                if (timer11.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer11.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer11.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem52_Click(object sender, EventArgs e)
        {
            if (gametypetb11 == "paying after")
            {
                beveragepaytb11 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem53_Click(object sender, EventArgs e)
        {
            if (gametypetb11 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb11 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "11";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem54_Click(object sender, EventArgs e)
        {
            tablememoindicator = "11";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem55_Click(object sender, EventArgs e)
        {
            if (gametypetb11 == "")
            {
                gametypetb11 = "service session";
                timer11.Enabled = true;
                duration11 = 0;
                timer11.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table11_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip11.SetToolTip(Table11, tablememo11);
        }
        //Table 11 우클릭 끝

        //Table 12 우클릭 시작
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (gametypetb12 != "")
            {
                if (timer12.Enabled)
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 timer is paused.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer12.Stop();
                }
                else
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer12.Start();
                }
            }
            else
            {
                MessageBox.Show("The table is not currently active.");
            }
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (gametypetb12 == "paying after")
            {
                beveragepaytb12 = "yes";
                Form8 f8 = new Form8();
                f8.ShowDialog();
            }
            else
            {
                MessageBox.Show("The table is currently not active or the session is not paying after.");
                return;
            }
        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (gametypetb12 == "daytime")
            {
                MessageBox.Show("Timer cannot be changed while in DAYTIME session.");
                return;
            }
            else if (gametypetb12 == "")
            {
                MessageBox.Show("The table is not currently active.");
                return;
            }
            modifytime = "12";
            Form5 f5 = new Form5();
            f5.ShowDialog();
        }

        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            tablememoindicator = "12";
            Form7 frm7 = new Form7();
            frm7.ShowDialog();
        }

        private void ToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (gametypetb12 == "")
            {
                gametypetb12 = "service session";
                timer12.Enabled = true;
                duration12 = 0;
                timer12.Start();

                //로그 시작
                string savePath = @"log.txt";
                string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 started at no charge.\n", Form2.userinfo);
                System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));
            }
            else
            {
                MessageBox.Show("Unable to start the session. Table is already in use.");
                return;
            }
        }
        private void Table12_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip12.SetToolTip(Table12, tablememo12);
        }
        //Table 12 우클릭 끝


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        // Table 1 시작
        private void timer1_Tick(object sender, EventArgs e)  //타이머설정
        {
            if (gametypetb1 == "paying after") //후불 세션
            {
                duration1++;
                Table1.Text = dt.AddSeconds(duration1).ToString("H:mm:ss");
            }
            else if (gametypetb1 == "service session") //서비스 세션
            {
                duration1++;
                Table1.Text = "FREE\n" + dt.AddSeconds(duration1).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration1--;
                Table1.Text = "Upfront\n" + dt.AddSeconds(duration1).ToString("H:mm:ss");
                if (duration1 == 0) //세션종료
                {
                    timer1.Stop();
                    MessageBox.Show("The time for the Table 1 is up.\nPlease inform them that they have run out of their time.", "Table 1");
                    Table1.Text = "Table 1";
                    gametypetb1 = "";
                    tablememo1 = "";
                }

            }
        }
        private void Table1_Click(object sender, EventArgs e) //테이블버튼 클릭설정
        {
            passbeverage = 0;
            if (gametypetb1 != "")
            {
                if (gametypetb1 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer1.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration1 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "1";
                        if (beveragetb1 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb1;
                        }
                        frm3.Passvalue = (price + beveragetb1).ToString();
                        passbeverage = beveragetb1;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer1.Start();
                    }
                }
                else if (timer1.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration1 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "1";
                        timer1.Stop();
                        Table1.Text = "Table 1";
                        gametypetb1 = "";
                        tablememo1 = "";
                        duration1 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 1 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer1.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "1";
                frm3.ShowDialog();
            }
        }
        // Table 1 끝

        // Table 2 시작
        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (gametypetb2 == "paying after") //후불 세션
            {
                duration2++;
                Table2.Text = dt.AddSeconds(duration2).ToString("H:mm:ss");
            }
            else if (gametypetb2 == "service session") //서비스 세션
            {
                duration2++;
                Table2.Text = "FREE\n" + dt.AddSeconds(duration2).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration2--;
                Table2.Text = "Upfront\n" + dt.AddSeconds(duration2).ToString("H:mm:ss");
                if (duration2 == 0) //세션종료
                {
                    timer2.Stop();
                    MessageBox.Show("The time for the Table 2 is up.\nPlease inform them that they have run out of their time.", "Table 2");
                    Table2.Text = "Table 2";
                    gametypetb2 = "";
                    tablememo2 = "";
                }
            }
        }

        private void Table2_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb2 != "")
            {
                if (gametypetb2 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer2.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration2 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "2";
                        if (beveragetb2 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb2;
                        }
                        frm3.Passvalue = (price + beveragetb2).ToString();
                        passbeverage = beveragetb2;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer2.Start();
                    }
                }
                else if (timer2.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration2 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "2";
                        timer2.Stop();
                        Table2.Text = "Table 2";
                        gametypetb2 = "";
                        tablememo2 = "";
                        duration2 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }

                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 2 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer2.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "2";
                frm3.ShowDialog();
            }
        }
        // Table 2 끝

        // Table 3 시작
        private void Table3_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb3 != "")
            {
                if (gametypetb3 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer3.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration3 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "3";
                        if (beveragetb3 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb3;
                        }
                        frm3.Passvalue = (price + beveragetb3).ToString();
                        passbeverage = beveragetb3;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer3.Start();
                    }
                }
                else if (timer3.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration3 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "3";
                        timer3.Stop();
                        Table3.Text = "Table 3";
                        gametypetb3 = "";
                        tablememo3 = "";
                        duration3 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 3 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer3.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "3";
                frm3.ShowDialog();
            }
        }

        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (gametypetb3 == "paying after") //후불 세션
            {
                duration3++;
                Table3.Text = dt.AddSeconds(duration3).ToString("H:mm:ss");
            }
            else if (gametypetb3 == "service session") //서비스 세션
            {
                duration3++;
                Table3.Text = "FREE\n" + dt.AddSeconds(duration3).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration3--;
                Table3.Text = "Upfront\n" + dt.AddSeconds(duration3).ToString("H:mm:ss");
                if (duration3 == 0) //세션종료
                {
                    timer3.Stop();
                    MessageBox.Show("The time for the Table 3 is up.\nPlease inform them that they have run out of their time.", "Table 3");
                    Table3.Text = "Table 3";
                    gametypetb3 = "";
                    tablememo3 = "";
                }

            }

        }

        // Table 3 끝
        // Table 4 시작
        private void Table4_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb4 != "")
            {
                if (gametypetb4 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer4.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration4 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "4";
                        if (beveragetb4 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb4;
                        }
                        frm3.Passvalue = (price + beveragetb4).ToString();
                        passbeverage = beveragetb4;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer4.Start();
                    }
                }
                else if (timer4.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration4 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "4";
                        timer4.Stop();
                        Table4.Text = "Table 4";
                        gametypetb4 = "";
                        tablememo4 = "";
                        duration4 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 4 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer4.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "4";
                frm3.ShowDialog();
            }
        }

        private void Timer4_Tick(object sender, EventArgs e)
        {
            if (gametypetb4 == "paying after") //후불 세션
            {
                duration4++;
                Table4.Text = dt.AddSeconds(duration4).ToString("H:mm:ss");
            }
            else if (gametypetb4 == "service session") //서비스 세션
            {
                duration4++;
                Table4.Text = "FREE\n" + dt.AddSeconds(duration4).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration4--;
                Table4.Text = "Upfront\n" + dt.AddSeconds(duration4).ToString("H:mm:ss");
                if (duration4 == 0) //세션종료
                {
                    timer4.Stop();
                    MessageBox.Show("The time for the Table 4 is up.\nPlease inform them that they have run out of their time.", "Table 4");
                    Table4.Text = "Table 4";
                    gametypetb4 = "";
                    tablememo4 = "";
                }

            }
        }
        // Table 4 끝
        // Table 5 시작
        private void Table5_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb5 != "")
            {
                if (gametypetb5 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer5.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration5 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "5";
                        if (beveragetb5 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb5;
                        }
                        frm3.Passvalue = (price + beveragetb5).ToString();
                        passbeverage = beveragetb5;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer5.Start();
                    }
                }
                else if (timer5.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration5 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "5";
                        timer5.Stop();
                        Table5.Text = "Table 5";
                        gametypetb5 = "";
                        tablememo5 = "";
                        duration5 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 5 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer5.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "5";
                frm3.ShowDialog();
            }
        }

        private void Timer5_Tick(object sender, EventArgs e)
        {
            if (gametypetb5 == "paying after") //후불 세션
            {
                duration5++;
                Table5.Text = dt.AddSeconds(duration5).ToString("H:mm:ss");
            }
            else if (gametypetb5 == "service session") //서비스 세션
            {
                duration5++;
                Table5.Text = "FREE\n" + dt.AddSeconds(duration5).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration5--;
                Table5.Text = "Upfront\n" + dt.AddSeconds(duration5).ToString("H:mm:ss");
                if (duration5 == 0) //세션종료
                {
                    timer5.Stop();
                    MessageBox.Show("The time for the Table 5 is up.\nPlease inform them that they have run out of their time.", "Table 5");
                    Table5.Text = "Table 5";
                    gametypetb5 = "";
                    tablememo5 = "";
                }

            }
        }
        // Table 5 끝

        // Table 6 시작
        private void Table6_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb6 != "")
            {
                if (gametypetb6 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer6.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration6 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "6";
                        if (beveragetb6 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb6;
                        }
                        frm3.Passvalue = (price + beveragetb6).ToString();
                        passbeverage = beveragetb6;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer6.Start();
                    }
                }
                else if (timer6.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration6 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "6";
                        timer6.Stop();
                        Table6.Text = "Table 6";
                        gametypetb6 = "";
                        tablememo6 = "";
                        duration6 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 6 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer6.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "6";
                frm3.ShowDialog();
            }
        }

        private void Timer6_Tick(object sender, EventArgs e)
        {
            if (gametypetb6 == "paying after") //후불 세션
            {
                duration6++;
                Table6.Text = dt.AddSeconds(duration6).ToString("H:mm:ss");
            }
            else if (gametypetb6 == "service session") //서비스 세션
            {
                duration6++;
                Table6.Text = "FREE\n" + dt.AddSeconds(duration6).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration6--;
                Table6.Text = "Upfront\n" + dt.AddSeconds(duration6).ToString("H:mm:ss");
                if (duration6 == 0) //세션종료
                {
                    timer6.Stop();
                    MessageBox.Show("The time for the Table 6 is up.\nPlease inform them that they have run out of their time.", "Table 6");
                    Table6.Text = "Table 6";
                    gametypetb6 = "";
                    tablememo6 = "";
                }

            }
        }
        // Table 6 끝

        // Table 7 시작
        private void Table7_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb7 != "")
            {
                if (gametypetb7 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer7.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration7 * 0.0058);
                        if (price < 11) //미니멈적용
                        {
                            price = 11;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "7";
                        if (beveragetb7 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb7;
                        }
                        frm3.Passvalue = (price + beveragetb7).ToString();
                        passbeverage = beveragetb7;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer7.Start();
                    }
                }
                else if (timer7.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration7 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "7";
                        timer7.Stop();
                        Table7.Text = "Table 7";
                        gametypetb7 = "";
                        tablememo7 = "";
                        duration7 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 7 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer7.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "7";
                frm3.ShowDialog();
            }
        }

        private void Timer7_Tick(object sender, EventArgs e)
        {
            if (gametypetb7 == "paying after") //후불 세션
            {
                duration7++;
                Table7.Text = dt.AddSeconds(duration7).ToString("H:mm:ss");
            }
            else if (gametypetb7 == "service session") //서비스 세션
            {
                duration7++;
                Table7.Text = "FREE\n" + dt.AddSeconds(duration7).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration7--;
                Table7.Text = "Upfront\n" + dt.AddSeconds(duration7).ToString("H:mm:ss");
                if (duration7 == 0) //세션종료
                {
                    timer7.Stop();
                    MessageBox.Show("The time for the Table 7 is up.\nPlease inform them that they have run out of their time.", "Table 7");
                    Table7.Text = "Table 7";
                    gametypetb7 = "";
                    tablememo7 = "";
                }

            }
        }
        // Table 7 끝

        // Table 8 시작
        private void Table8_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb8 != "")
            {
                if (gametypetb8 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer8.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration8 * 0.005);
                        if (price < 9) //미니멈적용
                        {
                            price = 9;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "8";
                        if (beveragetb8 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb8;
                        }
                        frm3.Passvalue = (price + beveragetb8).ToString();
                        passbeverage = beveragetb8;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer8.Start();
                    }
                }
                else if (timer8.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration8 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "8";
                        timer8.Stop();
                        Table8.Text = "Table 8";
                        gametypetb8 = "";
                        tablememo8 = "";
                        duration8 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 8 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer8.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "8";
                frm3.ShowDialog();
            }
        }

        private void Timer8_Tick(object sender, EventArgs e)
        {
            if (gametypetb8 == "paying after") //후불 세션
            {
                duration8++;
                Table8.Text = dt.AddSeconds(duration8).ToString("H:mm:ss");
            }
            else if (gametypetb8 == "service session") //서비스 세션
            {
                duration8++;
                Table8.Text = "FREE\n" + dt.AddSeconds(duration8).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration8--;
                Table8.Text = "Upfront\n" + dt.AddSeconds(duration8).ToString("H:mm:ss");
                if (duration8 == 0) //세션종료
                {
                    timer8.Stop();
                    MessageBox.Show("The time for the Table 8 is up.\nPlease inform them that they have run out of their time.", "Table 8");
                    Table8.Text = "Table 8";
                    gametypetb8 = "";
                    tablememo8 = "";
                }

            }
        }
        // Table 8 끝

        // Table 9 시작
        private void Table9_Click(object sender, EventArgs e)
        {
            if (gametypetb9 != "")
            {
                if (gametypetb9 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer9.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration9 * 0.005);
                        if (price < 9) //미니멈적용
                        {
                            price = 9;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "9";
                        if (beveragetb9 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb9;
                        }
                        frm3.Passvalue = (price + beveragetb9).ToString();
                        passbeverage = beveragetb9;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer9.Start();
                    }
                }
                else if (timer9.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration9 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "9";
                        timer9.Stop();
                        Table9.Text = "Table 9";
                        gametypetb9 = "";
                        tablememo9 = "";
                        duration9 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 9 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer9.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "9";
                frm3.ShowDialog();
            }
        }

        private void Timer9_Tick(object sender, EventArgs e)
        {
            if (gametypetb9 == "paying after") //후불 세션
            {
                duration9++;
                Table9.Text = dt.AddSeconds(duration9).ToString("H:mm:ss");
            }
            else if (gametypetb9 == "service session") //서비스 세션
            {
                duration9++;
                Table9.Text = "FREE\n" + dt.AddSeconds(duration9).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration9--;
                Table9.Text = "Upfront\n" + dt.AddSeconds(duration9).ToString("H:mm:ss");
                if (duration9 == 0) //세션종료
                {
                    timer9.Stop();
                    MessageBox.Show("The time for the Table 9 is up.\nPlease inform them that they have run out of their time.", "Table 9");
                    Table9.Text = "Table 9";
                    gametypetb9 = "";
                    tablememo9 = "";
                }

            }
        }
        // Table 9 끝

        // Table 10 시작
        private void Table10_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb10 != "")
            {
                if (gametypetb10 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer10.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration10 * 0.005);
                        if (price < 9) //미니멈적용
                        {
                            price = 9;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "10";
                        if (beveragetb10 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb10;
                        }
                        frm3.Passvalue = (price + beveragetb10).ToString();
                        passbeverage = beveragetb10;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer10.Start();
                    }
                }
                else if (timer10.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration10 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "10";
                        timer10.Stop();
                        Table10.Text = "Table 10";
                        gametypetb10 = "";
                        tablememo10 = "";
                        duration10 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 10 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer10.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "10";
                frm3.ShowDialog();
            }
        }

        private void Timer10_Tick(object sender, EventArgs e)
        {
            if (gametypetb10 == "paying after") //후불 세션
            {
                duration10++;
                Table10.Text = dt.AddSeconds(duration10).ToString("H:mm:ss");
            }
            else if (gametypetb10 == "service session") //서비스 세션
            {
                duration10++;
                Table10.Text = "FREE\n" + dt.AddSeconds(duration10).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration10--;
                Table10.Text = "Upfront\n" + dt.AddSeconds(duration10).ToString("H:mm:ss");
                if (duration10 == 0) //세션종료
                {
                    timer10.Stop();
                    MessageBox.Show("The time for the Table 10 is up.\nPlease inform them that they have run out of their time.", "Table 10");
                    Table10.Text = "Table 10";
                    gametypetb10 = "";
                    tablememo10 = "";
                }

            }
        }
        // Table 10 끝

        // Table 11 시작
        private void Table11_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb11 != "")
            {
                if (gametypetb11 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer11.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration11 * 0.005);
                        if (price < 9) //미니멈적용
                        {
                            price = 9;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "11";
                        if (beveragetb11 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb11;
                        }
                        frm3.Passvalue = (price + beveragetb11).ToString();
                        passbeverage = beveragetb11;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer11.Start();
                    }
                }
                else if (timer11.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration11 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "11";
                        timer11.Stop();
                        Table11.Text = "Table 11";
                        gametypetb11 = "";
                        tablememo11 = "";
                        duration11 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 11 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer11.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "11";
                frm3.ShowDialog();
            }
        }

        private void Timer11_Tick(object sender, EventArgs e)
        {
            if (gametypetb11 == "paying after") //후불 세션
            {
                duration11++;
                Table11.Text = dt.AddSeconds(duration11).ToString("H:mm:ss");
            }
            else if (gametypetb11 == "service session") //서비스 세션
            {
                duration11++;
                Table11.Text = "FREE\n" + dt.AddSeconds(duration11).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration11--;
                Table11.Text = "Upfront\n" + dt.AddSeconds(duration11).ToString("H:mm:ss");
                if (duration11 == 0) //세션종료
                {
                    timer11.Stop();
                    MessageBox.Show("The time for the Table 11 is up.\nPlease inform them that they have run out of their time.", "Table 11");
                    Table11.Text = "Table 11";
                    gametypetb11 = "";
                    tablememo11 = "";
                }

            }
        }
        // Table 11 끝

        // Table 12 시작
        private void Table12_Click(object sender, EventArgs e)
        {
            passbeverage = 0;
            if (gametypetb12 != "")
            {
                if (gametypetb12 == "paying after") //참고) 후불 세션종료는 Form3에서 이루어집니다
                {
                    if (timer12.Enabled) //후불이면 폼3을 개조된상태로 열기
                    {
                        price = Convert.ToInt32(duration12 * 0.005);
                        if (price < 9) //미니멈적용
                        {
                            price = 9;
                        }
                        Form3 frm3 = new Form3();
                        frm3.checkBox1.Visible = false;
                        frm3.comboBox1.Visible = false;
                        frm3.label10.Visible = false;
                        frm3.textBox8.Visible = false;
                        frm3.radioButton1.Visible = false;
                        frm3.radioButton2.Visible = false;
                        frm3.radioButton3.Visible = false;
                        frm3.tablenumber = "12";
                        if (beveragetb12 > 1)
                        {
                            frm3.label12.Text = "Beverage is included.\nBilliards: $" + price + "\nBeverage: $" + beveragetb12;
                        }
                        frm3.Passvalue = (price + beveragetb12).ToString();
                        passbeverage = beveragetb12;
                        frm3.ShowDialog();
                    }
                    else //후불인데 정지가 되있는 상태라면 타이머 resume
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 timer is resumed.\n", Form2.userinfo);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        timer12.Start();
                    }
                }
                else if (timer12.Enabled) // 선불 시간이 남아있으나 강제로 세션종료
                {
                    DialogResult dialogResult = MessageBox.Show("Are you sure to end the session for the table?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        //로그 시작
                        string savePath = @"log.txt";
                        string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 is manually stopped. (Time Remaining: {1} minutes.)\n", Form2.userinfo, duration12 / 60);
                        System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                        Form3 frm3 = new Form3();
                        frm3.tablenumber = "12";
                        timer12.Stop();
                        Table12.Text = "Table 12";
                        gametypetb12 = "";
                        tablememo12 = "";
                        duration12 = 0;
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                        return;
                    }
                }
                else //선불인데 정지가 되있는 상태라면 타이머 resume
                {
                    //로그 시작
                    string savePath = @"log.txt";
                    string textValue = string.Format(DateTime.Now.ToString("[HH:mm]") + "[{0}] Table 12 timer is resumed.\n", Form2.userinfo);
                    System.IO.File.AppendAllText(savePath, textValue, Encoding.GetEncoding("UTF-8"));

                    timer12.Start();
                }
            }
            else // 처음 시작하는 경우
            {
                Form3 frm3 = new Form3();
                frm3.tablenumber = "12";
                frm3.ShowDialog();
            }
        }

        private void Timer12_Tick(object sender, EventArgs e)
        {
            if (gametypetb12 == "paying after") //후불 세션
            {
                duration12++;
                Table12.Text = dt.AddSeconds(duration12).ToString("H:mm:ss");
            }
            else if (gametypetb12 == "service session") //서비스 세션
            {
                duration12++;
                Table12.Text = "FREE\n" + dt.AddSeconds(duration12).ToString("H:mm:ss");
            }
            else //나머지 선불 세션
            {
                duration12--;
                Table12.Text = "Upfront\n" + dt.AddSeconds(duration12).ToString("H:mm:ss");
                if (duration12 == 0) //세션종료
                {
                    timer12.Stop();
                    MessageBox.Show("The time for the Table 12 is up.\nPlease inform them that they have run out of their time.", "Table 12");
                    Table12.Text = "Table 12";
                    gametypetb12 = "";
                    tablememo12 = "";
                }

            }
        }
        // Table 12 끝
        public static string formClosing;
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (formClosing != "true")
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }


        string liveTime;
        public static string sendAgain;
        private void Timer14_Tick(object sender, EventArgs e)
        {
            liveTime = DateTime.Now.ToString("HH:mm:ss");
            if (isReset2 == "true" || userinfo == "사장님")
            {
                return;
            }
            else if (liveTime == "14:00:00" || liveTime == "16:00:00" || liveTime == "18:00:00" || liveTime == "20:00:00" || liveTime == "22:00:00" || liveTime == "00:00:00" || sendAgain == "true")
            {
                string report;
                sendAgain = "";
                if (cashout == 0)
                {
                    report = string.Format("정기보고입니다.\n토탈: ${0}\n카드: ${1}\n현금: ${2}", total, card, cash, cashout);
                }
                else
                {
                    report = string.Format("정기보고입니다.\n토탈: ${0}\n카드: ${1}\n현금: ${2}\n캐시아웃: ${3}", total, card, cash, cashout);
                }
                SendKatalk(report);
                if (isRoomopen == "no") //카톡방이 안열려있으면
                {
                    MessageBox.Show("KakaoTalk chat room needs be launched to perform the task.\nLaunch the chat room and click yes.");
                    sendAgain = "true";
                    isRoomopen = "";
                    return;
                }
                string tbstatus1 = "";
                string tbstatus2 = "";
                string tbstatus3 = "";
                string tbstatus4 = "";
                string tbstatus5 = "";
                string tbstatus6 = "";
                string tbstatus7 = "";
                string tbstatus8 = "";
                string tbstatus9 = "";
                string tbstatus10 = "";
                string tbstatus11 = "";
                string tbstatus12 = "";

                int pool = 0;
                int ball4 = 0;
                int pool_payingafter = 0;
                int ball4_payingafter = 0;
                //후불 4구
                if (gametypetb1 == "paying after")
                    ball4_payingafter++;
                if (gametypetb2 == "paying after")
                    ball4_payingafter++;
                if (gametypetb3 == "paying after")
                    ball4_payingafter++;
                if (gametypetb4 == "paying after")
                    ball4_payingafter++;
                if (gametypetb5 == "paying after")
                    ball4_payingafter++;
                if (gametypetb6 == "paying after")
                    ball4_payingafter++;
                if (gametypetb7 == "paying after")
                    ball4_payingafter++;
                //선불 4구
                if (gametypetb1 != "" && gametypetb1 != "paying after")
                    ball4++;
                if (gametypetb2 != "" && gametypetb2 != "paying after")
                    ball4++;
                if (gametypetb3 != "" && gametypetb3 != "paying after")
                    ball4++;
                if (gametypetb4 != "" && gametypetb4 != "paying after")
                    ball4++;
                if (gametypetb5 != "" && gametypetb5 != "paying after")
                    ball4++;
                if (gametypetb6 != "" && gametypetb6 != "paying after")
                    ball4++;
                if (gametypetb7 != "" && gametypetb7 != "paying after")
                    ball4++;
                //후불 포켓
                if (gametypetb8 == "paying after")
                    pool_payingafter++;
                if (gametypetb9 == "paying after")
                    pool_payingafter++;
                if (gametypetb10 == "paying after")
                    pool_payingafter++;
                if (gametypetb11 == "paying after")
                    pool_payingafter++;
                if (gametypetb12 == "paying after")
                    pool_payingafter++;
                //선불 포켓
                if (gametypetb8 != "" && gametypetb8 != "paying after")
                    pool++;
                if (gametypetb9 != "" && gametypetb9 != "paying after")
                    pool++;
                if (gametypetb10 != "" && gametypetb10 != "paying after")
                    pool++;
                if (gametypetb11 != "" && gametypetb11 != "paying after")
                    pool++;
                if (gametypetb12 != "" && gametypetb12 != "paying after")
                    pool++;
                tbstatus1 = "Table 1: " + gametypetb1 + "\n";
                tbstatus2 = "Table 2: " + gametypetb2 + "\n";
                tbstatus3 = "Table 3: " + gametypetb3 + "\n";
                tbstatus4 = "Table 4: " + gametypetb4 + "\n";
                tbstatus5 = "Table 5: " + gametypetb5 + "\n";
                tbstatus6 = "Table 6: " + gametypetb6 + "\n";
                tbstatus7 = "Table 7: " + gametypetb7 + "\n";
                tbstatus8 = "Table 8: " + gametypetb8 + "\n";
                tbstatus9 = "Table 9: " + gametypetb9 + "\n";
                tbstatus10 = "Table 10: " + gametypetb10 + "\n";
                tbstatus11 = "Table 11: " + gametypetb11 + "\n";
                tbstatus12 = "Table 12: " + gametypetb12 + "\n";

                if (gametypetb1 == "")
                    tbstatus1 = "";
                if (gametypetb2 == "")
                    tbstatus2 = "";
                if (gametypetb3 == "")
                    tbstatus3 = "";
                if (gametypetb4 == "")
                    tbstatus4 = "";
                if (gametypetb5 == "")
                    tbstatus5 = "";
                if (gametypetb6 == "")
                    tbstatus6 = "";
                if (gametypetb7 == "")
                    tbstatus7 = "";
                if (gametypetb8 == "")
                    tbstatus8 = "";
                if (gametypetb9 == "")
                    tbstatus9 = "";
                if (gametypetb10 == "")
                    tbstatus10 = "";
                if (gametypetb11 == "")
                    tbstatus11 = "";
                if (gametypetb12 == "")
                    tbstatus12 = "";

                Thread.Sleep(50);

                if (tbstatus1 == "" && tbstatus1 == "" && tbstatus2 == "" && tbstatus3 == "" && tbstatus4 == "" && tbstatus5 == "" && tbstatus6 == "" && tbstatus7 == "" && tbstatus8 == "" && tbstatus9 == "" && tbstatus10 == "" && tbstatus11 == "" && tbstatus12 == "")
                {
                    SendKatalk("현재 손님 없습니다.");
                    return;
                }
                SendKatalk(string.Format("현재 테이블 상황입니다.\n{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}\n선불 4구: {12}팀, 선불 포켓볼: {13}팀\n후불 4구: {14}팀, 후불 포켓볼: {15}팀", tbstatus1,
                    tbstatus2, tbstatus3, tbstatus4, tbstatus5, tbstatus6, tbstatus7, tbstatus8, tbstatus9, tbstatus10, tbstatus11, tbstatus12, ball4, pool, ball4_payingafter, pool_payingafter));
            }
            if (liveTime == "18:05:00")
            {
                MessageBox.Show("Did you change the username?");
            }
        }

        private void Label1_DoubleClick(object sender, EventArgs e)
        {
            ifIdle = "true";
            button14_Click(null, null);
            ifIdle = "";
        }


        private void Timer15_Tick(object sender, EventArgs e)
        {
            if (Form13.isAdmin != "true")
            {
                try
                {
                    Process[] processList = Process.GetProcessesByName("taskmgr");

                    if (processList.Length > 0)
                    {
                        processList[0].Kill();
                    }
                }
                catch { }
            }
        }

        private void Timer16_Tick(object sender, EventArgs e)
        {
            duration13--;
            label17.Text = Form13.EndDate2 + "\n" + dt.AddSeconds(duration13).ToString("H:mm:ss");
            if (duration13 == 0) //세션종료
            {
                timer16.Stop();
                button2_Click(null, null);
            }
        }

        string Announcement;
        private void Label4_TextChanged(object sender, EventArgs e)
        {
            if (label4.Text.Length > 70)
            {
                Announcement = label4.Text;
                label4.Text = label4.Text.Substring(0, 70);
                toolTip13.SetToolTip(label4, Announcement);
            }
            else
            {
                toolTip13.SetToolTip(label4, "");
            }
        }
    }
}