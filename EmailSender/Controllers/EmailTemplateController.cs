using EmailSender.Repository.EmailSenderRepo;
using EmailSender.Service.EmailSenderService;
using EmailSender.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace EmailSender.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailTemplateController : ControllerBase
    {
        private readonly ILogger<EmailTemplateController> _logger;
        private readonly IEmailSenderRepository _emailSender;
        private readonly IEmailSenderService _emailSenderService;

        public EmailTemplateController(ILogger<EmailTemplateController> logger, IEmailSenderRepository emailSender, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _emailSenderService = emailSenderService;

        }
        public DefaultResponse Get()
        {
            var response = new DefaultResponse();
            try
            {
                _emailSenderService.PrepareEmailsForUsers();
                response.StatusMessage = "Success";
                response.Success = true;
                response.StatusCode = 200;
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
                response.Success = false;
                response.StatusCode = 505;
                return response;
            }
            return response;
        }
    }
}
