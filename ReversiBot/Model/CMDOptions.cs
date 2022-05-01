using System;
using CommandLine;

namespace ReversiBot
{
	public class CMDOptions
	{
		[Option("bot",
			HelpText = "How many robot players.",
			Required = false,
			Default = 1
		)]
		public int Bot {get;set;}
		public void Validate()
		{
			if(Bot < 0 || Bot > 2)
				throw new ArgumentException("Bot has to be between 0 and 2");
		}
	}
}