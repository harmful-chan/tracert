using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace tracert
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length >= 1 && args.Length <= 2)
            {
                List<string> replyAddresses = new List<string>();
                
                //ping 远程主机获取ttl
                Ping ping = new Ping();
                PingOptions pingOptions = new PingOptions();
                byte[] sendTest = Encoding.ASCII.GetBytes("test");
                PingReply pingReply = ping.Send(args[0], 120, sendTest, pingOptions);
                int jumpCount = pingReply.Options.Ttl;
                if (jumpCount > 0)
                {
                    //获取路由跳数
                    int maxTtl = Near(pingReply.Options.Ttl, 64, 128, 256) - jumpCount;
                    //按照跳数 由 1 开始加1 发送icmp报文
                    for (int i = 1; i <= maxTtl; i++)
                    {
                        pingOptions.Ttl = i;
                        PingReply subReply = ping.Send(args[0], 120, sendTest, pingOptions);
                        if (i != maxTtl && subReply.Address.Equals(pingReply.Address))    //实测，中途路由不可达会返回远程主机I P
                        {
                            replyAddresses.Add("*");
                        }
                        else
                        {
                            replyAddresses.Add(subReply.Address.ToString());
                        }
                        //输出
                        if(args.Length == 2)
                        {
                            //获取特定跳数IP
                            if(int.Parse(args[1]) == i)
                                Console.WriteLine(replyAddresses[i - 1]);
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0,2}", i)+ " " + replyAddresses[i - 1]);
                        }
                    }
                }
            } 
        }

        private static int Near(int param, params int[] nears)
        {
            if (nears.Length > 1)
            {
                for (int i = 0; i < nears.Length - 1; i++)
                {
                    int avg = (nears[i] + nears[i + 1]) >> 1;    // 除2
                    if (param <= avg) return nears[i];
                    else return nears[i + 1];
                }
            }
            else if (nears.Length == 1) return nears[0];

            return param;
        }
    }
}
