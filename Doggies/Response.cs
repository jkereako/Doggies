using System.Runtime.Serialization;

namespace Doggies
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public string[] Message { get; set; }
    }
}
