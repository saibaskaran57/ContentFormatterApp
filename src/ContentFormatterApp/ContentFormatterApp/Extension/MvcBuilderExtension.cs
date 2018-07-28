namespace ContentFormatterApp.Extension
{
    using Microsoft.Extensions.DependencyInjection;
    using Formatters;

    public static class MvcBuilderExtension
    {
        public static IMvcBuilder AddUtf8JsonFormatters(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                options.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.JsonInputFormatter>();
                options.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.JsonOutputFormatter>();

                options.InputFormatters.Add(new Utf8JsonInputFormatter());
                options.OutputFormatters.Add(new Utf8JsonOutputFormatter());
            });
          
            return builder;
        }

        public static IMvcBuilder AddMessagePackFormatters(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options =>
            {
                options.InputFormatters.Add(new MessagePackInputFormatter());
                options.OutputFormatters.Add(new MessagePackOutputFormatter());
            });

            return builder;
        }
    }
}
