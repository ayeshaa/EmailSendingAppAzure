using EmailSender.Model;
using System.Collections.Generic;

namespace EmailSender.Repository.EmailSenderRepo
{
    public interface IEmailSenderRepository
    {
        List<EmailSenderData> Get();
        EmailSenderData Insert(EmailSenderData data);
    }
}
