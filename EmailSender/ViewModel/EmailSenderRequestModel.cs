using EmailSender.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.ViewModel
{
    public class EmailSenderRequestModel
    {
        [Required]
        [JsonProperty("EmailSenderInputs")]
        public List<EmailSenderInput> EmailSenderInputs
        {
            get;
            set;
        }
    }
}
