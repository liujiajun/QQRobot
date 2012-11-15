using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQRobot
{
    class Info
    {
        private static string m_ptwebqq = "";
        public static string ptwebqq
        {
            get { return m_ptwebqq; }
            set { m_ptwebqq = value; }
        }

        private static string m_psessionid = "";
        public static string psessionid
        {
            get { return m_psessionid; }
            set { m_psessionid = value; }
        }

        private static string m_vfwwebqq = "";
        public static string vfwwebqq
        {
            get { return m_vfwwebqq; }
            set { m_vfwwebqq = value; }
        }

        private static string m_json = "";
        public static string json
        {
            get { return m_json; }
            set { m_json = value; }
        }
    }
}
