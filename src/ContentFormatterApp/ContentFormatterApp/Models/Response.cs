namespace ContentFormatterApp.Models
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "")]
    public sealed class Response
    {
        [DataMember]
        public string Message { get; set; }
    }
}
