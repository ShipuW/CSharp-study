using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace MyNet.Tgs.CollectTest
{
    public partial class FrmMain : Form
    {
        InvokeService invokeService = new InvokeService();
        bool flag = false;
        System.Xml.XmlDocument Imagedoc = new System.Xml.XmlDocument();
        System.Xml.XmlNodeList Imagenlist;
        int CurrentIndex = -1;
        int count = 0;
        System.Net.WebClient MyWebClient = new System.Net.WebClient();
        string hphmHead = Properties.Settings.Default.HphmHead;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(invokeService.TestConnect());
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            //string kssj = "2013-06-03 14:30:30";
            //string jssj = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //TimeSpan dt = DateTime.Parse(jssj) - DateTime.Parse(kssj);
            //double totalhours = dt.TotalHours;
            txtzjwj1.Text = "http://127.0.0.1/web/pic/20090812142610_00000127_0.jpg";
            txtzjwj2.Text = "http://127.0.0.1/web/pic/20090812142610_00000127_1.jpg";
            txtzjwj3.Text = "http://127.0.0.1/web/pic/20090812142610_00000127_2.jpg";
            txtzjwj4.Text = "http://127.0.0.1/web/pic/20090812142610_00000127_1.jpg";
            //txtkkid.Text = "511123010001";
            txtclsd.Text = "";
            txthphm.Text = "";
            cmbcdbh.Text = "1";
            cmbfxmc.SelectedIndex = 1;
            cmbhpys.SelectedIndex = 2;
            txtclsd_Click(sender, e);
            txthphm_Click(sender, e);
            radioNormal.Checked = true;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "imagelist.xml";
            Imagedoc.Load(filePath);
            Imagenlist = Imagedoc.SelectNodes("imagelist/image");
           
        }

        private void butTest_Click(object sender, EventArgs e)
        {

            //string ipaddress = GetLocalIP();
            //DataTable dt = invokeService.GetDeviceList("service_ip='" + ipaddress + "'");
            string returnValue = invokeService.InPassCarInfo(GetXMl(txthphm.Text, txtclsd.Text));

            ShowXml(returnValue);
        }
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        private void ShowXml(string XmlBody)
        {
            
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(XmlBody);
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "temp.xml";
            doc.Save(filePath);
            this.webBrowser1.Url = new Uri(filePath);
        }
        private string GetImageUrl(int idx)
        {
            if (CurrentIndex>(Imagenlist.Count-1))
            {
                CurrentIndex = 0;
            }
         
            switch (idx)
            {

                case 1:
                    return Properties.Settings.Default.HttpHead + Imagenlist[CurrentIndex].InnerText + "" + Properties.Settings.Default.StartIndex.ToString() + ".jpg";
                case 2:
                    return Properties.Settings.Default.HttpHead + Imagenlist[CurrentIndex].InnerText + "" + (Properties.Settings.Default.StartIndex + 1).ToString() + ".jpg";
                case 3:
                    return Properties.Settings.Default.HttpHead + Imagenlist[CurrentIndex].InnerText + "" + (Properties.Settings.Default.StartIndex + 2).ToString() + ".jpg";
            }
            return "";
        }

        private string GetXMl(string hphm, string clsd)
        {
            CurrentIndex++;
            Random rd = new Random();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xml = xml + "<message System=\"ATMS\" Ver=\"1.0\">";
            xml = xml + "<systemtype>TGS</systemtype>";
            xml = xml + "<messagetype>NOTICE</messagetype>";
            xml = xml + "<sourceIP>3.0</sourceIP>";
            xml = xml + "<targetIP />";
            xml = xml + "<user>PASSCAR</user>";
            xml = xml + "<password>PASSCAR</password>";
            xml = xml + "<passcarinfo>";
            xml = xml + "<kkid>" + txtkkid.Text + "</kkid>";
            xml = xml + "<hphm>" + hphm + "</hphm>";
            xml = xml + "<hpys>" + cmbhpys.Text + "</hpys>";
            xml = xml + "<cllx>K31</cllx>";
            xml = xml + "<gwsj>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</gwsj>";
            xml = xml + "<fxbh>" + cmbfxmc.Text.ToString() + "</fxbh>";
            xml = xml + "<cdbh>" + cmbcdbh.Text + "</cdbh>";
            xml = xml + "<clsd>" + clsd + "</clsd>";
            xml = xml + "<zjwj1>http://127.0.0.1/web/pic/20090812142610_00000127_1.jpg</zjwj1>";
            xml = xml + "<zjwj2>http://127.0.0.1/web/pic/20090812142610_00000127_2.jpg</zjwj2>";
            xml = xml + "<zjwj3>http://127.0.0.1/web/pic/20090812142610_00000127_3.jpg</zjwj3>";
            xml = xml + "<zjwj4></zjwj4>";
            if (radioNormal.Checked)
            {
                xml = xml + "<jllx>0</jllx>";
            }
            else
            {
                xml = xml + "<jllx>14</jllx>";
            }
            xml = xml + "<cshm>" + rd.Next(20, 999).ToString() + "</cshm>";
            xml = xml + "<bz></bz>";
            //xml = xml + "<sbbh>10000000000</sbbh>";
            xml = xml + "</passcarinfo>";
            xml = xml + "</message>";

            return xml;
        }

        private void txthphm_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString("HHmmss");
            int sec = int.Parse(time.Substring(5, 1));
            this.txthphm.Text = hphmHead + GenerateRandom(1) + GenerateRandomNum(5);
        }

        private static char[] constantNum=  { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static char[] constant =  { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(25);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(25)]);
            }
            return newRandom.ToString();
        }
        public static string GenerateRandomNum(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(10);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constantNum[rd.Next(10)]);
            }
            return newRandom.ToString();
        }

        private void txtclsd_Click(object sender, EventArgs e)
        {
            Random rd = new Random();
            txtclsd.Text = rd.Next(20, 100).ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            count = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //radioAlarm.Checked = radioNormal.Checked;
            ////radioNormal.Checked = !radioNormal.Checked;
            ////radioAlarm.Checked = true;
            //txtclsd_Click(sender, e);
            //txthphm_Click(sender, e);
            //string returnValue = invokeService.InPassCarInfo(GetXMl(txthphm.Text, txtclsd.Text));
            //count++;
            //ShowXml(returnValue);
            //this.lblCount.Text = count.ToString();
            //TimeSpan  sec = System.DateTime.Now - DateTime.Parse(this.lblStart.Text);
            //if (sec.TotalSeconds > 0)
            //{
            //    labMiao.Text = (count / sec.TotalSeconds).ToString();
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.lblEnd.Text = System.DateTime.Now.ToString();
           
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "imagelist.xml";
            string[] filenames = Directory.GetFiles(Properties.Settings.Default.ImagePath, "*.jpg", SearchOption.AllDirectories);
            System.Collections.Hashtable hs = new System.Collections.Hashtable();
            string xmlnode = "";
            for (int i = 0; i < filenames.Length; i++)
            {
                string fileNanem = Path.GetFileName(filenames[i]).Substring(0, Properties.Settings.Default.FileFlag);
                if (!hs.ContainsKey(fileNanem))
                {
                    hs.Add(fileNanem, fileNanem);
                }

            }
            foreach (DictionaryEntry myDE in hs)
            {
                xmlnode = xmlnode + "<image>" + myDE.Value + "</image>";
            }
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xml = xml + "<imagelist>";
            xml = xml + xmlnode;
            xml = xml + "</imagelist>";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xml);
            doc.Save(filePath);
            hs.Clear();
            filenames = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string ipaddress = GetLocalIP();
            //DataTable dt = invokeService.GetDeviceList("service_ip='" + ipaddress + "'");
            //string returnValue = webService.InputVehicleInfo(GetXMl(txthphm.Text));

            //ShowXml(returnValue);
        }
        //调用---------------   


        private string GetXMl(string hphm)
        {
            CurrentIndex++;
            Random rd = new Random();
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
            xml = xml + "<message System=\"ATMS\" Ver=\"1.0\">";
            xml = xml + "<systemtype>TGS</systemtype>";
            xml = xml + "<messagetype>NOTICE</messagetype>";
            xml = xml + "<sourceIP>3.0</sourceIP>";
            xml = xml + "<targetIP />";
            xml = xml + "<user>PASSCAR</user>";
            xml = xml + "<password>PASSCAR</password>";
            xml = xml + "<passcarinfo>";
            xml = xml + "<dwbh>" + txtkkid.Text + "</dwbh>";
            xml = xml + "<hphm>" + hphm + "</hphm>";
            xml = xml + "<hpys>" + cmbhpys.Text + "</hpys>";
            xml = xml + "<cllx>K31</cllx>";
            xml = xml + "<gwsj>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</gwsj>";

            xml = xml + "<zjwj1>" + ConventToBase64(GetPic(GetImageUrl(2).Replace("127.0.0.1", this.txtIpaddress.Text))) + "</zjwj1>";
            xml = xml + "<zjwj2>" + ConventToBase64(GetPic(GetImageUrl(2).Replace("127.0.0.1", this.txtIpaddress.Text)))+ "</zjwj2>";
            xml = xml + "<zjwj3>" + ConventToBase64(GetPic(GetImageUrl(2).Replace("127.0.0.1", this.txtIpaddress.Text))) + "</zjwj3>";
            xml = xml + "<zjwj4></zjwj4>";
            if (radioNormal.Checked)
            {
                xml = xml + "<jllx>0</jllx>";
            }
            else
            {
                xml = xml + "<jllx>1</jllx>";
            }
            xml = xml + "<clpp>劳斯莱斯</clpp>";
            xml = xml + "<bz></bz>";
            xml = xml + "</passcarinfo>";
            xml = xml + "</message>";

            return xml;
        }



        public byte[] GetPic(string strUrl)
        {
            try
            {
                return MyWebClient.DownloadData(strUrl);
            }
            catch
            {
                return null;
            }

        }
        // 将图片的二进制数组转换成base64编码
        private string ConventToBase64(byte[] pic)
        {
            try
            {
                if (pic != null && pic.Length > 0)
                {
                    //if (pic.Length > picLength * 1024)
                    //{
                    //    logs.WriteLog("图片过大，进行压缩操作！");
                    MemoryStream ms = new MemoryStream(pic);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    byte[] tmp = ImageToByteArray(img);
                    return System.Convert.ToBase64String(tmp);

                    //    //return HttpUtility.UrlEncode(System.Convert.ToBase64String(tmp), Encoding.UTF8);
                    //}
                    //else
                    //{
                    //    return System.Convert.ToBase64String(pic);
                    //    //return HttpUtility.UrlEncode(System.Convert.ToBase64String(pic), Encoding.UTF8);
                    //}
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
             
                return "";
            }
        }
        //image转换成byte数组
        private byte[] ImageToByteArray(System.Drawing.Image img)
        {
            try
            {
                Bitmap image = new Bitmap(img, (1600 / 5) * 4, (1200 / 5) * 4);
                //image.Save("C:\\1.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                MemoryStream ms = new MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }

    }
}