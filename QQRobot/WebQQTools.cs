using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace QQRobot
{
    class WebQQTools
    {
        public string id { get; set;}
        public string password { get; set; }

        private string clientID = GenerateClientID();
        public static string GenerateClientID()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(0, 99) + "" + GetTime(DateTime.Now) / 1000000;
        }
        public static long GetTime(DateTime dateTime)
        {
            DateTime startDate = new DateTime(1970, 1, 1);
            DateTime endDate = dateTime.ToUniversalTime();
            TimeSpan span = endDate - startDate;
            return (long)(span.TotalMilliseconds + 0.5);
        }

        public string ErrorMessage { get; set; }

        Http http = new Http();

        public string checkVerifycode()
        {       
            string str=http.HttpSendData(string.Format("http://check.ptlogin2.qq.com/check?uin={0}&appid=1003903&r=0.9982102437527717",id),"GET", "", "UTF-8");
            if (str.IndexOf("('0'") > 0)
            {
                str = str.Substring(0x12, 4);  
                return str;
            }
                return "需要验证码！";
        }

        public bool Login1(string verifyCode)
        {
            string EncryptedPsw = EncryptPass.Encrypt(id, password, verifyCode);
            Console.WriteLine("Program: 计算得到密码字符串【" + EncryptedPsw + "】");
            string URL=string.Format("http://ptlogin2.qq.com/login?u={0}&p={1}&verifycode={2}&webqq_type=10&remember_uin=1&login2qq=1&aid=1003903&u1=http%3A%2F%2Fwebqq.qq.com%2Floginproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&h=1&ptredirect=0&ptlang=2052&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=4-26-31813&mibao_css=m_webqq&t=1&g=1",id,EncryptedPsw ,verifyCode);
            string res=http.HttpSendData(URL, "GET", "", "UTF-8", "http://ui.ptlogin2.qq.com/cgi-bin/login?target=self&style=5&mibao_css=m_webqq&appid=1003903&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fwebqq.qq.com%2Floginproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20120920001");
           
            if (res.IndexOf("成功") == -1) { ErrorMessage = res; return false; }
            Info.ptwebqq=http.cookie.GetCookieHeader(new Uri("http://qq.com")).Substring(90,64);
            return true;
        }

        public bool Login2(string ptwebqq)
        {
            string URL = "http://d.web2.qq.com/channel/login2";
            string postData = string.Format("r=%7B%22status%22%3A%22online%22%2C%22ptwebqq%22%3A%22{0}%22%2C%22passwd_sig%22%3A%22%22%2C%22clientid%22%3A%22{1}%22%2C%22psessionid%22%3Anull%7D&clientid={2}&psessionid=null",Info.ptwebqq,clientID,clientID);
            string res = http.HttpSendData(URL, "POST", postData, "UTF-8", "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3");
            if (res.IndexOf("retcode\":0") == -1) { ErrorMessage = res;  return false; } 
            JObject json = JObject.Parse(res);
            Info.vfwwebqq=json["result"]["vfwebqq"].ToString();
            Info.psessionid = json["result"]["psessionid"].ToString();
            return true;
        }


        public string sendPoll(string psessionid)
        {
            string res=this.http.HttpSendData("http://d.web2.qq.com/channel/poll2", "POST", "r=%7B%22clientid%22%3A%22"+clientID+"%22%2C%22psessionid%22%3A%22"+psessionid+"%22%2C%22key%22%3A0%2C%22ids%22%3A%5B%5D%7D&clientid="+clientID+"&psessionid="+psessionid, "UTF-8", "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=3");
            return res;
        }
             

    }
}
