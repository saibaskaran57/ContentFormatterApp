namespace ContentFormatterApp.Formatters
{
    using MessagePack;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class MessagePackOutputFormatter : TextOutputFormatter
    {
        private const string ContentType = "application/x-msgpack";
        private static readonly string[] SupportedContentTypes = new[] { ContentType };

        readonly IFormatterResolver resolver;

        public MessagePackOutputFormatter()
            : this(null)
        {
        }

        public MessagePackOutputFormatter(IFormatterResolver resolver)
        {
            this.SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentType));
            this.SupportedEncodings.Add(Encoding.UTF8);
            this.SupportedEncodings.Add(Encoding.Unicode);

            this.resolver = resolver ?? MessagePackSerializer.DefaultResolver;
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            context.HttpContext.Response.ContentType = ContentType;

            if (context.ObjectType == typeof(object))
            {
                if (context.Object == null)
                {
                    context.HttpContext.Response.Body.WriteByte(MessagePackCode.Nil);
                    return Task.CompletedTask;
                }
                else
                {
                    MessagePackSerializer.NonGeneric.Serialize(context.Object.GetType(), context.HttpContext.Response.Body, context.Object, resolver);
                    return Task.CompletedTask;
                }
            }
            else
            {
                MessagePackSerializer.NonGeneric.Serialize(context.ObjectType, context.HttpContext.Response.Body, context.Object, resolver);
                return Task.CompletedTask;
            }
        }
    }
}