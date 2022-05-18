using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Colorful;

namespace FTNChecker
{
	// Token: 0x02000005 RID: 5
	internal class Program
	{
		// Token: 0x06000014 RID: 20 RVA: 0x00007A04 File Offset: 0x00005C04
		[STAThread]
		private static void Main(string[] args)
		{
			foreach (Process process in Process.GetProcessesByName("DnSpy-x86"))
			{
				Environment.Exit(0);
			}
			Colorful.Console.Title = (Colorful.Console.Title = "FORNITE KEKER V1.0 | By Sexify");
			Colorful.Console.Clear();
			Colorful.Console.WriteLine();
			Colorful.Console.Write("                            █████▒▒█████   ██▀███  ▄▄▄█████▓ ███▄    █  ██▓▄▄▄█████▓▓█████  \n", Color.Red);
			Colorful.Console.Write("                          ▓██   ▒▒██▒  ██▒▓██ ▒ ██▒▓  ██▒ ▓▒ ██ ▀█   █ ▓██▒▓  ██▒ ▓▒▓█   ▀  \n", Color.Red);
			Colorful.Console.Write("                          ▒████ ░▒██░  ██▒▓██ ░▄█ ▒▒ ▓██░ ▒░▓██  ▀█ ██▒▒██▒▒ ▓██░ ▒░▒███    \n", Color.Red);
			Colorful.Console.Write("                          ░▓█▒  ░▒██   ██░▒██▀▀█▄  ░ ▓██▓ ░ ▓██▒  ▐▌██▒░██░░ ▓██▓ ░ ▒▓█  ▄  \n", Color.Red);
			Colorful.Console.Write("                          ░▒█░   ░ ████▓▒░░██▓ ▒██▒  ▒██▒ ░ ▒██░   ▓██░░██░  ▒██▒ ░ ░▒████▒ \n", Color.Red);
			Colorful.Console.Write("                           ▒ ░   ░ ▒░▒░▒░ ░ ▒▓ ░▒▓░  ▒ ░░   ░ ▒░   ▒ ▒ ░▓    ▒ ░░   ░░ ▒░ ░ \n", Color.Red);
			Colorful.Console.Write("                           ░       ░ ▒ ▒░   ░▒ ░ ▒░    ░    ░ ░░   ░ ▒░ ▒ ░    ░     ░ ░  ░ \n", Color.Red);
			Colorful.Console.Write("                           ░ ░   ░ ░ ░ ▒    ░░   ░   ░         ░   ░ ░  ▒ ░  ░         ░    \n", Color.Red);
			Colorful.Console.Write("                                     ░ ░     ░                       ░  ░              ░  ░ \n", Color.Red);
			Colorful.Console.WriteLine();
			Thread.Sleep(250);
			int threads;
			for (;;)
			{
				Colorful.Console.Write("> How many ", Color.White);
				Colorful.Console.Write("THREADS", Color.White);
				Colorful.Console.Write(" You Want To Use!", Color.White);
				Colorful.Console.Write(" (max 200)", Color.White);
				Colorful.Console.Write(": ", Color.Red);
				string s = Colorful.Console.ReadLine();
				bool flag = int.Parse(s) > 200;
				bool flag8 = flag;
				if (flag8)
				{
					Colorful.Console.WriteLine();
					Colorful.Console.Write("> Please select a number of threads between 1 and 200.\n\n", Color.Red);
					Thread.Sleep(2000);
				}
				else
				{
					try
					{
						threads = int.Parse(s);
						break;
					}
					catch
					{
						threads = 100;
						break;
					}
				}
			}
			string text;
			for (;;)
			{
				Colorful.Console.Write(DateTime.Now.ToString("[hh:mm:ss]"), Color.Red);
				Colorful.Console.Write("> What type of ", Color.White);
				Colorful.Console.Write("PROXIES ", Color.White);
				Colorful.Console.Write("[HTTP, SOCKS4, SOCKS5, LESS]", Color.Red);
				Colorful.Console.Write(": ", Color.Red);
				text = Colorful.Console.ReadLine();
				text = text.ToUpper();
				bool flag2 = text.Contains("HTTP");
				bool flag9 = flag2;
				if (flag9)
				{
					break;
				}
				bool flag3 = text.Contains("SOCKS4");
				bool flag10 = flag3;
				if (flag10)
				{
					goto Block_5;
				}
				bool flag4 = text.Contains("SOCKS5");
				bool flag11 = flag4;
				if (flag11)
				{
					goto Block_6;
				}
				bool flag5 = text.Contains("LESS");
				bool flag12 = flag5;
				if (flag12)
				{
					goto Block_7;
				}
				Colorful.Console.Write("> Please select a valid proxy format.\n\n", Color.Red);
				Thread.Sleep(2000);
			}
			Vars.proxytype = "HTTP";
			goto IL_2F2;
			Block_5:
			Vars.proxytype = "SOCKS4";
			goto IL_2F2;
			Block_6:
			Vars.proxytype = "SOCKS5";
			goto IL_2F2;
			Block_7:
			Vars.isRunningProxyless = true;
			IL_2F2:
			Colorful.Console.WriteLine();
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string fileName;
			do
			{
				Colorful.Console.WriteLine("Select your Combos", Color.Red);
				Thread.Sleep(500);
				openFileDialog.Title = "Select Combo List";
				openFileDialog.DefaultExt = "txt";
				openFileDialog.Filter = "Text files|*.txt";
				openFileDialog.RestoreDirectory = true;
				openFileDialog.ShowDialog();
				fileName = openFileDialog.FileName;
			}
			while (!File.Exists(fileName));
			Vars.accounts = new List<string>(File.ReadAllLines(fileName));
			Vars.LoadCombos(fileName);
			Colorful.Console.Write("> ");
			Colorful.Console.Write(Vars.accounts.Count, Color.Red);
			Colorful.Console.WriteLine(" Combos added\n");
			Task.Factory.StartNew(delegate()
			{
				Check.UpdateTitle();
			});
			bool flag6 = text != "LESS";
			bool flag13 = flag6;
			if (flag13)
			{
				do
				{
					Colorful.Console.WriteLine("Select your Proxies", Color.Red);
					Thread.Sleep(500);
					openFileDialog.Title = "Select Proxy List";
					openFileDialog.DefaultExt = "txt";
					openFileDialog.Filter = "Text files|*.txt";
					openFileDialog.RestoreDirectory = true;
					openFileDialog.ShowDialog();
					fileName = openFileDialog.FileName;
				}
				while (!File.Exists(fileName));
				Vars.proxies = new List<string>(File.ReadAllLines(fileName));
				Vars.LoadProxies(fileName);
				Colorful.Console.Write("> ");
				Colorful.Console.Write(Vars.proxytotal, Color.Red);
				Colorful.Console.WriteLine(" Proxies added\n");
			}
			bool flag7 = !Directory.Exists("Results/" + Vars.ResultsFolder);
			bool flag14 = flag7;
			if (flag14)
			{
				Directory.CreateDirectory("Results/" + Vars.ResultsFolder);
			}
			Task.Factory.StartNew(delegate()
			{
				for (int j = 0; j < threads * 10; j++)
				{
					new Thread(new ThreadStart(Check.CallWorker)).Start();
				}
			}).Wait();
			System.Console.ReadLine();
			Environment.Exit(0);
		}
	}
}
