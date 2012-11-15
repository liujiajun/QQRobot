using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQRobot
{
    class EncryptPass
    {
        public static string EncyptMD5_3_16(string s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            byte[] bytes2 = md5.ComputeHash(bytes1);
            byte[] bytes3 = md5.ComputeHash(bytes2);

            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var item in bytes3)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }

        /// <summary>
        /// 进行MD5加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string EncyptMD5(string s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);

            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var item in bytes1)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }
        public static byte[] EncyptMD5Bytes(string s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            return md5.ComputeHash(bytes);


        }
        public static string EncyptMD5(byte[] s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();

            byte[] bytes1 = md5.ComputeHash(s);
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var item in bytes1)
            {
                sb.Append(item.ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();

        }
        public static string EncryptQQWebMd5(string s)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5CryptoServiceProvider.Create();
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(s);
            byte[] bytes1 = md5.ComputeHash(bytes);
            System.Text.StringBuilder sb = new StringBuilder();
            foreach (var item in bytes1)
            {
                sb.Append(@"\x");
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
        /// <summary>
        /// 将验证码和密码进行组合加密，然后重新赋给password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public static string EncryptOld(string password, string verifyCode)
        {

            return EncyptMD5(EncyptMD5_3_16(password) + verifyCode.ToUpper());
        }

        public static string Encrypt(string qq, string password, string verifyCode)
        {
            return Encrypt(Convert.ToInt32(qq), password, verifyCode);
        }
        public static string Encrypt(long qq, string password, string verifyCode)
        {

            ByteBuffer buf = new ByteBuffer();
            buf.Put(EncyptMD5Bytes(password));
            buf.PutInt(0);
            buf.PutInt((uint)qq);
            string i = EncryptQQWebMd5(password);
            byte[] v = buf.ToByteArray();
            string h = EncyptMD5(v);
            string g = EncyptMD5(h + verifyCode.ToUpper());
            return g;
        }

    }
}

