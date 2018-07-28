namespace ContentFormatterApp.Formatters
{
    using MessagePack;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class MessagePackInputFormatter : TextInputFormatter
    {
        private const string ContentType = "application/x-msgpack";
        private static readonly string[] SupportedContentTypes = new[] { ContentType };

        private readonly IFormatterResolver resolver;

        public MessagePackInputFormatter()
            : this(null)
        {
        }

        public MessagePackInputFormatter(IFormatterResolver resolver)
        {
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);

            this.resolver = resolver ?? MessagePackSerializer.DefaultResolver;
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            var result = MessagePackSerializer.NonGeneric.Deserialize(context.ModelType, request.Body, resolver);

            return InputFormatterResult.SuccessAsync(result);
        }
    }
}
