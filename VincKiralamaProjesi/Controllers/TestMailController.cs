using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VincKiralamaProjesi.Services;

namespace VincKiralamaProjesi.Controllers
{
    public class TestMailController : Controller
    {
        private readonly EmailService _email;

        public TestMailController(EmailService email)
        {
            _email = email;
        }

        // /TestMail/Send
        public async Task<IActionResult> Send()
        {
            await _email.SendFirmKeyAsync("betulacikgoz74@gmail.com", "Betül Test", "TEST1234");
            return Content("Test mail gönderildi (kod buraya geldiyse).");
        }
    }
}
