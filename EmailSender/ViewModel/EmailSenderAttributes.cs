using DataAnnotationsExtensions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EmailSender.Model
{
    public class EmailSenderInput
    {
        public string Key
        {
            get; set;
        }
        [Email]
        public string Email
        {
            get; set;
        }
        [JsonProperty("Attributes")]
        public List<string> Attributes
        {
            get; set;
        }
    }
}
