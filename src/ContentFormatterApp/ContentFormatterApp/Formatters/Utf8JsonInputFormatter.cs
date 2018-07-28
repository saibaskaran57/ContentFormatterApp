namespace ContentFormatterApp.Formatters
{
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Utf8Json;

    public sealed class Utf8JsonInputFormatter : TextInputFormatter
    {
        private const string ContentType = "application/json";
        private static readonly string[] SupportedContentTypes = new[] { ContentType };

        private readonly IJsonFormatterResolver resolver;

        public Utf8JsonInputFormatter()
            : this(null)
        {
        }

        public Utf8JsonInputFormatter(IJsonFormatterResolver resolver)
        {
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);

            this.resolver = resolver ?? JsonSerializer.DefaultResolver;
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            var result = JsonSerializer.NonGeneric.Deserialize(context.ModelType, request.Body, resolver);

            return InputFormatterResult.SuccessAsync(result);
        }
    }
}
