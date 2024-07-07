using EcommercePro.DTO;
using EcommercePro.Models;
using EcommercePro.Repositiories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcommercePro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        RepoContact RepoContact;

        IContact _contact1;
        IEmailService _emailService;
        public ContactController(IContact contact1, IEmailService emailService)
        {
            _contact1 = contact1;
            _emailService = emailService;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult GetAll()
        {
            List<Contact> ConList = _contact1.GetAll();
            return Ok(ConList);

        }

        [HttpPost]
        public IActionResult Add([FromBody] Contact newCont)
        {
            if (newCont == null)
            {
                return BadRequest("Contact data is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contact1.Insert(new Contact()
                    {
                        Name = newCont.Name,
                        Message = newCont.Message,
                        Email = newCont.Email
                    });
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost("sendMassage")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SendMessage(SendEmailCommend sendEmail)
        {
            var response = await _emailService.SendEmail(sendEmail.Email, sendEmail.Meassage);
            if (response == "Success")
            {
                return Ok();
            }
            return BadRequest(response);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int ContactId)
        {
            bool isDeleted = this._contact1.Delete(ContactId);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest("Faild to Delete");
        }

    }
}
