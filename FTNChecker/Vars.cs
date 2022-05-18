using System;
using System.Collections.Generic;
using System.IO;

namespace FTNChecker
{
	// Token: 0x02000008 RID: 8
	internal class Vars
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000083D0 File Offset: 0x000065D0
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
							Vars.total++;
						}
					}
				}
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00008468 File Offset: 0x00006668
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
							Vars.proxytotal++;
						}
					}
				}
			}
		}

		// Token: 0x0400000D RID: 13
		public static int total;

		// Token: 0x0400000E RID: 14
		public static int bad = 0;

		// Token: 0x0400000F RID: 15
		public static int hits = 0;

		// Token: 0x04000010 RID: 16
		public static int err = 0;

		// Token: 0x04000011 RID: 17
		public static int check = 0;

		// Token: 0x04000012 RID: 18
		public static int accindex = 0;

		// Token: 0x04000013 RID: 19
		public static int stop = 0;

		// Token: 0x04000014 RID: 20
		public static List<string> proxies = new List<string>();

		// Token: 0x04000015 RID: 21
		public static string proxytype = "";

		// Token: 0x04000016 RID: 22
		public static int proxyindex = 0;

		// Token: 0x04000017 RID: 23
		public static int proxytotal = 0;

		// Token: 0x04000018 RID: 24
		public static List<string> RareSkins = new List<string>();

		// Token: 0x04000019 RID: 25
		public static List<string> accounts = new List<string>();

		// Token: 0x0400001A RID: 26
		public static bool isRunningProxyless;

		// Token: 0x0400001B RID: 27
		public static int CPS;

		// Token: 0x0400001C RID: 28
		public static int CPM;

		// Token: 0x0400001D RID: 29
		public static int Rares;

		// Token: 0x0400001E RID: 30
		public static int NoSkins;

		// Token: 0x0400001F RID: 31
		public static int Skinned;

		// Token: 0x04000020 RID: 32
		public static string ResultsFolder = string.Format("\\{0:MM-dd-yy}", DateTime.Now);

		// Token: 0x04000021 RID: 33
		public static int Galaxy;

		// Token: 0x04000022 RID: 34
		public static int IKONIK;

		// Token: 0x04000023 RID: 35
		public static int Eon;

		// Token: 0x04000024 RID: 36
		public static int Rb;

		// Token: 0x04000025 RID: 37
		public static int Reflex;

		// Token: 0x04000026 RID: 38
		public static int Honor;

		// Token: 0x04000027 RID: 39
		public static int Wonder;

		// Token: 0x04000028 RID: 40
		public static int BK;

		// Token: 0x04000029 RID: 41
		public static int SS;

		// Token: 0x0400002A RID: 42
		public static int CodeName;

		// Token: 0x0400002B RID: 43
		public static int Heidi;

		// Token: 0x0400002C RID: 44
		public static int Mako;

		// Token: 0x0400002D RID: 45
		public static int AA1;

		// Token: 0x0400002E RID: 46
		public static int Other;

		// Token: 0x0400002F RID: 47
		public static int RE;

		// Token: 0x04000030 RID: 48
		public static int GT;

		// Token: 0x04000031 RID: 49
		public static int OG_SKULL;

		// Token: 0x04000032 RID: 50
		public static int Aerial;

		// Token: 0x04000033 RID: 51
		public static int RR;

		// Token: 0x04000034 RID: 52
		public static int RR_AXE;

		// Token: 0x04000035 RID: 53
		public static int Max10;

		// Token: 0x04000036 RID: 54
		public static int Max25;

		// Token: 0x04000037 RID: 55
		public static int Max50;

		// Token: 0x04000038 RID: 56
		public static int Max100;
	}
}
