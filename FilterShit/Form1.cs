using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.IO;
using System.Web;

namespace FilterShit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int boofs = 0;
        static int boofg = 0;
        static int boo = 0;
        static string coo = "";
        static string id = "";
        static int followersd = 0;
        static int followingd = 0;
        static string csrftoken = "";
        static string url2 ="";
        static string url3 = "";
        Point point0 = new Point();
        bool boolean0;
        static int num1 = 0;
        static int num2 = 0;
        static int num3 = 0;
        static int num4 = 0;
        static int num5 = 0;
        static int num6 = 0;
        static string urlfesBAND = "";
        static string urlfigBAND = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Project Name : FilterShit - [Free]\nProgrammer : Karar(MrVirus)\nInstagram : BQBB\nTelegram : NNN7N\nTelegram Channel : Camera","CopyRight");
            CheckForIllegalCrossThreadCalls = false;
        }
        static bool login(string user, string pass, Form1 but)
        {
            bool check = true;
            WebClient tk = new WebClient();
            string tk2 = tk.DownloadString("https://www.instagram.com/");
            string token = Regex.Match(tk2, "\"csrf_token\":\"(.*?)\",").Groups[1].Value;
            WebClient web = new WebClient();
            web.Headers.Add("accept", "*/*");
            web.Headers.Add("content-type", "application/x-www-form-urlencoded");
            web.Headers.Add("user-agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");
            web.Headers.Add("x-csrftoken", token);
            web.Headers.Add("x-instagram-ajax", "1");
            web.Headers.Add("x-requested-with", "XMLHttpRequest");
            string response = web.UploadString("https://www.instagram.com/accounts/login/ajax/", "username=" + user + "&password=" + pass);
            if (response.Contains("\"authenticated\": true,"))
            {
                check = true;
                coo = cookie(web.ResponseHeaders.ToString());
                but.button1.Text = "GetAll";

            }
            else { check = false; MessageBox.Show("Fail To Login", "Error"); }
            return check;

        }
        static string cookie(string headers)
        {

            string cook = "";
            string mid = Regex.Match(headers, "mid=(.*?);").Groups[1].Value;
            string sessionid = Regex.Match(headers, "sessionid=(.*?);").Groups[1].Value;
            string userid = Regex.Match(headers, "ds_user_id=(.*?);").Groups[1].Value;
             csrftoken = Regex.Match(headers, "csrftoken=(.*?);").Groups[1].Value;
            string ig_did = Regex.Match(headers, "ig_did=(.*?);").Groups[1].Value;
            cook = "mid=" + mid + ";ds_user_id=" + userid + ";sessionid=" + sessionid + ";csrftoken=" + csrftoken + ";ig_did=" + ig_did + ";";
            return cook;
        }
        static string getid(string user)
        {
            
            WebClient web = new WebClient();
            string result = web.DownloadString("https://www.instagram.com/" + user);
            id = Regex.Match(result, "\"profilePage_(.*?)\",").Groups[1].Value;
            url2 = "https://www.instagram.com/graphql/query/?query_hash=d04b0a864b4b54837c0d870b0e77e076&variables={%22id%22:%22" + id + "%22,%22include_reel%22:true,%22fetch_mutual%22:false,%22first%22:24}";
            url3 = "https://www.instagram.com/graphql/query/?query_hash=c76146de99bb02f6415203be841dd25a&variables={%22id%22:%22" + id + "%22,%22include_reel%22:true,%22fetch_mutual%22:false,%22first%22:24}";
            return id;

        }
        static string followers(string url,Form1 for2)
        {

            try
            {
                WebClient web = new WebClient();
                web.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                web.Headers.Add("cookie", coo);
                web.Headers.Add("x-csrftoken", csrftoken);
                web.Headers.Add("x-requested-with", "XMLHttpRequest");
                web.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
                string result = web.DownloadString(url);
                followersd = Convert.ToInt32(Regex.Match(result, "\"count\":(.*?),").Groups[1].Value);
                if (num1 >= followersd) { return ""; }
                else if (num5 >= 250) { Thread.Sleep(Convert.ToInt32(for2.textBox12.Text)*1000); num5 = 0; followers(url, for2); return ""; }
                else
                {
                    if (result.Contains("\"has_next_page\":false,"))
                    {

                        MatchCollection fetch2 = Regex.Matches(result, "\"owner\":{(.*?)}");
                        foreach (Match user in fetch2)
                        {

                            string u = Regex.Match(user.Groups[1].Value, ",\"username\":\"(.*?)\"").Groups[1].Value;

                            for2.listBox1.Items.Add(u);
                            num1 += 1;
                            num5 += 1;
                            for2.textBox1.Text = num1.ToString();
                        }
                        boofs += 1;
                        boo += 1;
                        if (boo == 2)
                        {
                            filterr(for2);
                        }
                    }
                    else if (result.Contains("\"has_next_page\":true,"))
                    {
                        string cursor = Regex.Match(result, "\"end_cursor\":\"(.*?)\"").Groups[1].Value;

                        MatchCollection fetch2 = Regex.Matches(result, "\"owner\":{(.*?)}");
                        foreach (Match user in fetch2)
                        {

                            string u = Regex.Match(user.Groups[1].Value, ",\"username\":\"(.*?)\"").Groups[1].Value;

                            for2.listBox1.Items.Add(u);
                            num1 += 1;
                            num5 += 1;
                            for2.textBox1.Text = num1.ToString();
                        }
                        followers("https://www.instagram.com/graphql/query/?query_hash=c76146de99bb02f6415203be841dd25a&variables={%22id%22:%22" + id + "%22,%22include_reel%22:true,%22fetch_mutual%22:false,%22first%22:24,%22after%22:%22" + cursor + "%22}", for2);
                    }


                    return "";
                }
            }
            catch
            {
                urlfesBAND = url;
                filterr(for2);
            }
            return "";
        }
        static string following(string url,Form1 for2)
        {
            try
            {
                WebClient web = new WebClient();
                web.Headers.Add("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                web.Headers.Add("cookie", coo);
                web.Headers.Add("x-csrftoken", csrftoken);
                web.Headers.Add("x-requested-with", "XMLHttpRequest");
                web.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/79.0.3945.130 Safari/537.36");
                string result = web.DownloadString(url);
                followingd = Convert.ToInt32(Regex.Match(result, "\"count\":(.*?),").Groups[1].Value);
                if (num2 >= followingd) { return ""; }
                else if (num6 >= 250) { Thread.Sleep(Convert.ToInt32(for2.textBox12.Text) * 1000); num6 = 0; following(url, for2); return ""; }
                else
                {
                    if (result.Contains("\"has_next_page\":false,"))
                    {

                        MatchCollection fetch2 = Regex.Matches(result, "\"owner\":{(.*?)}");
                        foreach (Match user in fetch2)
                        {
                            string u = Regex.Match(user.Groups[1].Value, ",\"username\":\"(.*?)\"").Groups[1].Value;
                            for2.listBox2.Items.Add(u);
                            num2 += 1;
                            num6 += 1;
                            for2.textBox2.Text = num2.ToString();
                        }
                        boofg += 1;
                        boo += 1;
                        if (boo == 2)
                        {
                            filterr(for2);
                        }
                    }
                    else if (result.Contains("\"has_next_page\":true,"))
                    {
                        string cursor = Regex.Match(result, "\"end_cursor\":\"(.*?)\"").Groups[1].Value;

                        MatchCollection fetch2 = Regex.Matches(result, "\"owner\":{(.*?)}");
                        foreach (Match user in fetch2)
                        {
                            string u = Regex.Match(user.Groups[1].Value, ",\"username\":\"(.*?)\"").Groups[1].Value;
                            for2.listBox2.Items.Add(u);
                            num2 += 1;
                            num6 += 1;
                            for2.textBox2.Text = num2.ToString();
                        }
                        following("https://www.instagram.com/graphql/query/?query_hash=d04b0a864b4b54837c0d870b0e77e076&variables={%22id%22:%22" + id + "%22,%22include_reel%22:true,%22fetch_mutual%22:false,%22first%22:24,%22after%22:%22" + cursor + "%22}", for2);
                    }
                    return "";
                }
                
            }
            catch
            {
                urlfigBAND = url;
                filterr(for2);
            }
            return "";
        }
        static void filterr(Form1 wo)
        {
            int v = wo.listBox2.Items.Count - 1;
            for (int i=0;i<=v;i++)
            {
                if (wo.listBox1.Items.Contains(wo.listBox2.Items[i]) && !wo.listBox3.Items.Contains(wo.listBox2.Items[i]))
                {
                    wo.listBox3.Items.Add(wo.listBox2.Items[i]);
                    num3 += 1;
                    wo.textBox3.Text = num3.ToString();
                }
                else if(!wo.listBox1.Items.Contains(wo.listBox2.Items[i]) && !wo.listBox4.Items.Contains(wo.listBox2.Items[i]))
                {
                    wo.listBox4.Items.Add(wo.listBox2.Items[i]);
                    num4 += 1;
                    wo.textBox4.Text = num4.ToString();
                }
            }
            if (boo == 2 && boofg == 1 && boofs==1) { MessageBox.Show("Successful...", "BQBB"); }
            else if(boo==1 && boofg==1) { Thread.Sleep(Convert.ToInt32(wo.textBox13.Text) * 1000);followers(urlfesBAND, wo); }
            else if (boo==1 && boofs==1) { Thread.Sleep(Convert.ToInt32(wo.textBox13.Text) * 1000);following(urlfigBAND, wo); }
        }
   

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Login")
            {
                login(textBox6.Text, textBox5.Text,this);
            }
            else if (button1.Text == "GetAll") {
                getid(textBox7.Text);
               
                Thread th1 = new Thread(()=> followers(url3,this));
                Thread th2 = new Thread(()=> following(url2, this));
                th1.Start();
                th2.Start();
               
               

            }
           
        }



        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.point0 = new Point((0 - e.X), (0 - e.Y));
                this.boolean0 = true;
            }
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.boolean0)
            {
                Point mousePosition = Control.MousePosition;
                mousePosition.Offset(this.point0.X, this.point0.Y);
                this.Location = mousePosition;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.boolean0 = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
