using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Helper
{
    public class AppConfigManager
    {
		public static string DbConnectionString;
		
		public AppConfigManager()
		{
			var configurationBuilder = new ConfigurationBuilder();
			var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
			configurationBuilder.AddJsonFile(path, false);
			var root = configurationBuilder.Build();

			DbConnectionString = root.GetSection("ConnectionStrings").GetSection("DBConnectionString").Value;
			

		}
		public string GetConnectionString
		{
			get => DbConnectionString;
		}
		
	}
}
