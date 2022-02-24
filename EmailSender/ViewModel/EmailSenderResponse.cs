using EmailSender.Model;
using System.Collections.Generic;

namespace EmailSender.ViewModel
{
    public class EmailSenderResponse
    {
        public DefaultResponse defaultResponse
        {
            get;
            set;
        }
        public List<EmailSenderData> GetList
        {
            get;
            set;
        }
    }
}
