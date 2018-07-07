namespace ContentFormatterApp.Models
{
    using System.Runtime.Serialization;

    [DataContract(Namespace = "")]
    public sealed class Request
    {
        [DataMember]
        public string Id { get; set; }
    }
}
