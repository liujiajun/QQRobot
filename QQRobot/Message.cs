using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace QQRobot
{
    class Message
    {
        JObject message;
        string m_Text = "";
        public string Text 
        {get { return m_Text; }}

        string m_FromUin = "";
        public string FromUin { get { return m_FromUin; } }

        string m_FromGroupUin = "";
        public string FromGroupUin { get { return m_FromGroupUin; } }
        private string m_json;
        public string json
        {
            set { 
                m_json = value;  
                message = JObject.Parse(value);
                Console.WriteLine(value);
            }
        }

        public string initMessage() 
        {
                if (message["retcode"].ToString() == "0")
                {

                    string type = message["result"][0]["poll_type"].ToString();
                    switch (type)
                    {
                        case "message":
                            initFriendMessageEvent();
                            break;

                        case "group_message":
                            initGroupMessageEvent();
                            break;

                        case "kick_message":
                            m_Text = message["result"][0]["value"]["reason"].ToString();
                            break;

                        default:
                            break;
                    }
                    return m_Text;
                }
                else
                {
                    return message["retcode"].ToString();
                }

        }
        public string initFriendMessageEvent()
        { 
            m_Text = message["result"][0]["value"]["content"][1].ToString();
            m_FromUin = message["result"][0]["value"]["from_uin"].ToString();
            return m_Text;
        }

        public string initGroupMessageEvent()
        {
            m_Text = message["result"][0]["value"]["content"][1].ToString();
            m_FromUin = message["result"][0]["value"]["from_uin"].ToString();
            m_FromGroupUin = message["result"][0]["value"]["from_uin"].ToString();
            return m_Text;
        }

        public string initErrorMessageEvent()
        {
            m_Text = message["retcode"].ToString();
            return m_Text;
        }
    }

}
