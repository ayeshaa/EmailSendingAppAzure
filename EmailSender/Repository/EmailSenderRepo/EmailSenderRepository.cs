using EmailSender.Context;
using EmailSender.Model;
using System.Collections.Generic;

namespace EmailSender.Repository.EmailSenderRepo
{
    public class EmailSenderRepository : IEmailSenderRepository
    {
        private readonly EmailSenderContext context;

        public EmailSenderRepository(EmailSenderContext db)
        {
            context = db;
        }
        public List<EmailSenderData> Get()
        {
            var emailData = new List<EmailSenderData>();
            foreach (var e in context.EmailSenderData)
            {
                emailData.Add(e);
            }
            return emailData;
        }

        public EmailSenderData Insert(EmailSenderData data)
        {
            this.context.EmailSenderData.Add(data);
            this.context.SaveChanges();

            return data;
        }
    }
}
