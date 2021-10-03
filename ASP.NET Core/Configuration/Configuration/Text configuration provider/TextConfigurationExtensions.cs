using System;

using Microsoft.Extensions.Configuration;

namespace Configuration.Text_configuration_provider
{
    public static class TextConfigurationExtensions : Object
    {
        public static IConfigurationBuilder AddTextFile(this IConfigurationBuilder builder, String fileName)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("fileName is null or empty.");
            }

            var source = new TextConfigurationSource(fileName);

            builder.Add(source);

            return builder;
        }
    }
}