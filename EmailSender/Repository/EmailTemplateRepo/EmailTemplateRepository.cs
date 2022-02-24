using EmailSender.Context;
using EmailSender.Model;
using System.Collections.Generic;

namespace EmailSender.Repository.EmailTemplateRepo
{
    public class EmailTemplateRepository : IEmailTemplateRepository
    {
        private readonly EmailSenderContext context;

        public EmailTemplateRepository(EmailSenderContext db)
        {
            context = db;
        }
        public List<EmailTemplates> Get()
        {
            var emailData = new List<EmailTemplates>();
            foreach (var e in context.EmailTemplates)
            {
                emailData.Add(e);
            }
            return emailData;
        }

        public EmailTemplates Insert(EmailTemplates data)
        {
            this.context.EmailTemplates.Add(data);
            this.context.SaveChanges();

            return data;
        }
    }
}
