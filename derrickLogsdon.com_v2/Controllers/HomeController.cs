using derrickLogsdon.com_v2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace derrickLogsdon.com_v2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender, IConfiguration iconfig)
        {
            _logger = logger;
            _emailSender = emailSender;
            _config = iconfig;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View(new ContactMeViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendEmail(ContactMeViewModel model)
        {
            string messageFormat = @"derricklogsdon.com Contact Request from
                               Name is : {0}
                               Phone Number is : {1}
                               Email Address is : {2}
                               Message :
                               {3}";

            string message = string.Format(messageFormat, model.Name, model.PhoneNumber, model.EmailAddress, model.Message);

        try {
                string emailAddress = _config.GetValue<string>("EmailAddress");
            Task.WaitAll(_emailSender.SendEmailAsync(emailAddress, "derricklogsdon.com Contact Form", message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
           
            return StatusCode(StatusCodes.Status200OK);
        }
        public IActionResult BlogRoll()
        {
            return View();
         
        }
        public IActionResult Post()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
