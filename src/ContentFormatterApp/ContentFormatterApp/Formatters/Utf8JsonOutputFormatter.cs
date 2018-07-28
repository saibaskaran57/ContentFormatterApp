namespace ContentFormatterApp.Formatters
{
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Utf8Json;

    public class Utf8JsonOutputFormatter : TextOutputFormatter
    {
        private const string ContentType = "application/json";
        private static readonly string[] SupportedContentTypes = new[] { ContentType };

        private readonly IJsonFormatterResolver resolver;

        public Utf8JsonOutputFormatter()
            : this(null)
        {
        }

        public Utf8JsonOutputFormatter(IJsonFormatterResolver resolver)
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

            this.resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            context.HttpContext.Response.ContentType = ContentType;

            if (context.ObjectType == typeof(object))
            {
                return JsonSerializer.NonGeneric.SerializeAsync(context.HttpContext.Response.Body, context.Object, resolver);
            }
            else
            {
                return JsonSerializer.NonGeneric.SerializeAsync(context.ObjectType, context.HttpContext.Response.Body, context.Object, resolver);
            }
        }
    }
}