using EmailSender.Model;
using System.Collections.Generic;

namespace EmailSender.Repository.EmailTemplateRepo
{
    public interface IEmailTemplateRepository
    {
        List<EmailTemplates> Get();
        EmailTemplates Insert(EmailTemplates data);
    }
}
