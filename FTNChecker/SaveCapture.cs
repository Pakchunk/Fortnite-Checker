using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FTNChecker
{
	// Token: 0x02000006 RID: 6
	internal class SaveCapture
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00007F10 File Offset: 0x00006110
		public static void WriteToDisk(string combo, string id, string created, string username, string isFA, int vbucks, List<string> Skins, List<string> Backblings, List<string> Pickaxes, List<string> Gliders, List<string> Rares)
		{
			string text = string.Join(Environment.NewLine, Skins.ToArray());
			string text2 = string.Join(Environment.NewLine, Backblings.ToArray());
			string text3 = string.Join(Environment.NewLine, Pickaxes.ToArray());
			string text4 = string.Join(Environment.NewLine, Gliders.ToArray());
			string text5 = string.Join(Environment.NewLine, Rares.ToArray());
			string str = string.Concat(new object[]
			{
				"|---------------| Account Information |---------------|",
				Environment.NewLine,
				"-> Login: " + combo,
				Environment.NewLine,
				"-> FA: " + isFA,
				Environment.NewLine,
				"-> ID: " + id,
				Environment.NewLine,
				"-> Created: " + created,
				Environment.NewLine,
				"-> Display Name: " + username,
				Environment.NewLine,
				string.Format("-> V-Bucks: {0}", vbucks),
				Environment.NewLine,
				string.Format("-> Rares: {0}", Rares.Count<string>()),
				Environment.NewLine,
				string.Format("-> Skins: {0}", Skins.Count<string>()),
				Environment.NewLine,
				string.Format("-> Backblings: {0}", Backblings.Count<string>()),
				Environment.NewLine,
				string.Format("-> Pickaxes: {0}", Pickaxes.Count<string>()),
				Environment.NewLine,
				string.Format("-> Gliders: {0}", Gliders.Count<string>()),
				Environment.NewLine,
				"|----------------------| Rares |----------------------|",
				Environment.NewLine,
				text5,
				Environment.NewLine,
				"|----------------------| Skins {0} |----------------------|",
				Skins.Count<string>(),
				Environment.NewLine,
				text,
				Environment.NewLine,
				"|--------------------| Backblings |-------------------|",
				Environment.NewLine,
				text2,
				Environment.NewLine,
				"|---------------------| Pickaxes |--------------------|",
				Environment.NewLine,
				text3,
				Environment.NewLine,
				"|----------------------| Gliders |--------------------|",
				Environment.NewLine,
				text4,
				Environment.NewLine,
				"|----------------------------------------------------|",
				Environment.NewLine
			});
			File.AppendAllText("Results/" + Vars.ResultsFolder + "/Capture.txt", str + Environment.NewLine);
			bool flag = Skins.Count != 0;
			bool flag2 = flag;
			bool flag12 = flag2;
			if (flag12)
			{
				bool flag3 = !Directory.Exists("Results/" + Vars.ResultsFolder + "/Skins");
				bool flag13 = flag3;
				if (flag13)
				{
					Directory.CreateDirectory("Results/" + Vars.ResultsFolder + "/Skins");
				}
				bool flag4 = Skins.Count >= 50;
				bool flag5 = flag4;
				bool flag14 = flag5;
				if (flag14)
				{
					File.AppendAllText("Results/" + Vars.ResultsFolder + "/Skins/50-100+.txt", string.Format("{0} - {1} Skins", combo, Skins.Count<string>()) + Environment.NewLine);
					Vars.Max100++;
				}
				else
				{
					bool flag6 = Skins.Count >= 25;
					bool flag7 = flag6;
					bool flag15 = flag7;
					if (flag15)
					{
						File.AppendAllText("Results/" + Vars.ResultsFolder + "/Skins/25-50.txt", string.Format("{0} - {1} Skins", combo, Skins.Count<string>()) + Environment.NewLine);
						Vars.Max50++;
					}
					else
					{
						bool flag8 = Skins.Count >= 10;
						bool flag9 = flag8;
						bool flag16 = flag9;
						if (flag16)
						{
							File.AppendAllText("Results/" + Vars.ResultsFolder + "/Skins/10-25.txt", string.Format("{0} - {1} Skins", combo, Skins.Count<string>()) + Environment.NewLine);
							Vars.Max25++;
						}
						else
						{
							bool flag10 = Skins.Count >= 1;
							bool flag11 = flag10;
							bool flag17 = flag11;
							if (flag17)
							{
								File.AppendAllText("Results/" + Vars.ResultsFolder + "/Skins/1-10.txt", string.Format("{0} - {1} Skins", combo, Skins.Count<string>()) + Environment.NewLine);
								Vars.Max10++;
							}
						}
					}
				}
			}
		}
	}
}
