using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace QQRobot
{
    class Http
    {
        
        private string strAccept = "*/*";
        private string strContentType = "application/x-www-form-urlencoded;charset=utf-8";
        private string strUserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.04506.30; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322; .NET4.0C; .NET4.0E)";
        private CookieContainer cookies=new CookieContainer();
        public CookieContainer cookie
        {
            get { return cookies; }
            set { cookies = value; }
        }

        public string HttpSendData(string URL, string Method = "GET", string Data = "", string Encode = "UTF-8",string Referer="")
        {
            HttpWebResponse response;
            StreamReader reader;
            this.strContentType = "application/x-www-form-urlencoded; charset=" + Encode.ToLower();
            Uri requestUri = new Uri(URL);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUri);
            request.UserAgent = this.strUserAgent;
            request.Accept = this.strAccept;
            request.ContentType = this.strContentType;
            request.Method = Method;
            request.Referer = Referer;
            request.CookieContainer = this.cookies;
            if (Method.ToUpper() == "POST")
            {
                byte[] bytes = Encoding.Default.GetBytes(Data);
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();
            }
            try
            {
              //  Console.WriteLine("HTTP:正在获取响应流");
                response = (HttpWebResponse)request.GetResponse();
           //     Console.WriteLine("HTTP:已经取得响应流");
            }
            catch (WebException exception)
            {
                response = (HttpWebResponse)exception.Response;
                return ("HTTP:无法取得响应流"+exception.Message);
            }
            this.cookies.Add(response.Cookies);

            Stream responseStream = response.GetResponseStream();
            if (Encode.ToLower() == "utf-8")
            {
                reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
            }
            else
            {
                reader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
            }
            string str = reader.ReadToEnd();
           // Console.WriteLine("HTTP:已经读取响应流");
            reader.Close();
            responseStream.Close();
            request.Abort();
            response.Close();

            return str;
        }

       

    }
}
