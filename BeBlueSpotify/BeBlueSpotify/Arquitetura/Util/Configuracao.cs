using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BeBlueSpotify.Arquitetura.Util
{
    public class Configuracao
    {
        public static IConfiguration GetConfiguracao()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            return builder.Build();
        }        
    }
}
