using Common.SendMessage;
using DataLayer.Contracts;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly EmailSender _emailSender;
        public AccountController(IAccountRepository accountRepository, EmailSender emailSender)
        {
            _accountRepository = accountRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new User();// _accountRepository.GetUsers();

            return new JsonResult(users);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            if (id == 0)
                return NotFound();

            var user = _accountRepository.GetUserById(id);

            if (user is null)
                return NotFound();

            return new JsonResult(user);
        }

        [HttpGet("{username}/{password}")]
        public int GetUser(string username, string password)
        {
            var u = _accountRepository.GetUserByUsername(username);

            if (u is null)
                return 0;

            if (u.Password != password)
                return 0;

            return u.UserId;
        }

        // POST api/<AccountController>
        [HttpPost]
        public int PostUser(User user)
        {
            if (user is null)
                return 0;

            _accountRepository.CreateUser(user);
            _emailSender.SendEmail(user.Email, "Welcome Message", $"<h3>Welcome {user.Username}</h3>");

            return user.UserId;
        }

        // PUT api/<AccountController>
        [HttpPut]
        public int PutUser(User user)
        {
            if (user is null)
                return 0;

            _accountRepository.EditUser(user);

            return user.UserId;
        }
    }
}
