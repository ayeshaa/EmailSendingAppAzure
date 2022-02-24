using System;
using System.ComponentModel.DataAnnotations;

namespace EmailSender.Model
{
    public class EmailTemplates
    {
        [Key]
        public int Id
        {
            get;
            set;
        }
        public string SentBy
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
        public DateTime? SentOn
        {
            get;
            set;
        }
    }
}
