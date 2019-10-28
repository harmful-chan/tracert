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
            if(args.Length >= 1)
            {
                Ping ping = new Ping();
                PingOptions pingOptions = new PingOptions();
                PingReply pingReply = ping.Send(args[0], 120, Encoding.ASCII.GetBytes("test"), pingOptions);
                List<string> replyAddresses = new List<string>();
                if (pingReply != null)
                {
                    int maxTtl = Near(pingReply.Options.Ttl, 64, 128, 256) - pingReply.Options.Ttl;
                    for (int i = 1; i <= maxTtl; i++)
                    {
                        pingOptions.Ttl = i;
                        PingReply subReply = ping.Send(args[0], 120, Encoding.ASCII.GetBytes("test"), pingOptions);
                        if (i != maxTtl && subReply.Address.Equals(pingReply.Address))
                        {
                            replyAddresses.Add("*");
                        }
                        else
                        {
                            replyAddresses.Add(subReply.Address.ToString());
                        }
                        if(args.Length == 2)
                        {
                            if(int.Parse(args[1]) == i)
                                Console.WriteLine(i + " " + replyAddresses[i - 1]);
                        }
                        else
                        {
                            Console.WriteLine(i + " " + replyAddresses[i - 1]);
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
