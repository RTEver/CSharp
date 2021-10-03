using System;
using System.IO;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

namespace Configuration.Text_configuration_provider
{
    public sealed class TextConfigurationProvider : ConfigurationProvider
    {
        private String pathToFile;

        public TextConfigurationProvider(String pathToFile)
            : base()
        {
            if (String.IsNullOrEmpty(pathToFile))
            {
                throw new ArgumentException("pathToFile is null or empty.");
            }

            this.pathToFile = pathToFile;
        }

        public override void Load()
        {
            var data = new Dictionary<String, String>();

            using (var fileStream = File.OpenRead(pathToFile))
            using (var streamReader = new StreamReader(fileStream))
            {
                var line = String.Empty;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var values = line.Trim().Split('=');

                    var key = values[0].Trim();
                    var value = values[1].Trim();

                    data.Add(key, value);
                }
            }

            Data = data;
        }
    }
}