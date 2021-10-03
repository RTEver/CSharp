using System;

using Microsoft.Extensions.Configuration;

namespace Configuration.Text_configuration_provider
{
    public sealed class TextConfigurationSource : Object, IConfigurationSource
    {
        private String fileName;

        public TextConfigurationSource(String fileName)
            : base()
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("fileName is null or empty.");
            }

            this.fileName = fileName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            var filePath = builder.GetFileProvider().GetFileInfo(fileName).PhysicalPath;

            return new TextConfigurationProvider(filePath);
        }
    }
}