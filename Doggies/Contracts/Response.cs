using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Doggies
{
    [DataContract]
    public class Response
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "message")]
        public IEnumerable<string> Message { get; set; }
    }
}
