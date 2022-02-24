using System;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Model
{
    public class EmailSenderData
    {
        [Key]
        public int Id
        {
            get; set;
        }
        public string Key
        {
            get; set;
        }
        public string Email
        {
            get; set;
        }
        public string Attributes
        {
            get; set;
        }
        public DateTime? AddedOn
        {
            get;
            set;
        }
    }
}
