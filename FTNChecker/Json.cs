using System;
using Newtonsoft.Json;

namespace FTNChecker
{
	// Token: 0x02000004 RID: 4
	internal class Json
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000079CF File Offset: 0x00005BCF
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000079D7 File Offset: 0x00005BD7
		[JsonProperty("code")]
		public string exchangecode { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000079E0 File Offset: 0x00005BE0
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000079E8 File Offset: 0x00005BE8
		[JsonProperty("access_token")]
		public string access_token { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000079F1 File Offset: 0x00005BF1
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000079F9 File Offset: 0x00005BF9
		[JsonProperty("account_id")]
		public string account_id { get; set; }
	}
}
