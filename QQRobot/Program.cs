using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("运行中...");
            WebQQTools tool = new WebQQTools();
            tool.id = "";
            tool.password = "";

            Console.WriteLine("Program：正在获取验证码");
            string verfiryCode = tool.checkVerifycode();

            if (verfiryCode.Length != 4) {
                Console.WriteLine("Program：错误！需要验证码");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Program：已经取得验证码：【"+verfiryCode+"】");
            Console.WriteLine(" Program:正在执行Login1");
            if (!tool.Login1(verfiryCode))
            {
                Console.WriteLine("Program：登录失败【"+tool.ErrorMessage.Replace("/r/n","") + "】");
                Console.ReadLine();
                Environment.Exit(0);
            }

            Console.WriteLine("Program：Login1执行完毕");
            Console.WriteLine("Program:已经取得ptwebqq:【"+Info.ptwebqq+"】");

            Console.WriteLine("Program:正在执行Login2");
            if (!tool.Login2(Info.ptwebqq))
            {
                Console.WriteLine("Program：登录失败【" + tool.ErrorMessage + "】");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Console.WriteLine("Program:Login2执行完毕");
            Console.WriteLine("Program:已经取得psessionid【"+Info.psessionid+"】");
            while (true)
            {
                tool.sendPoll(Info.psessionid);
            }
            Console.ReadLine();
        }
    }
}
