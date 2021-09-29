using Common.SendMessage;
using DataLayer.Contracts;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using RegApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly EmailSender _emailSender;
        public AccountController(IAccountRepository accountRepository, EmailSender emailSender)
        {
            _accountRepository = accountRepository;
            _emailSender = emailSender;
        }

        public IActionResult Profile(int id)
        {
            if (id == 0)
                return NotFound();

            var user = _accountRepository.GetUserById(id);

            if (user is null)
                return NotFound();

            var registerVM = new ProfileViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address
            };

            return View(registerVM);
        }
        [HttpPost]
        public IActionResult EditProfile(ProfileViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserId = vm.UserId,
                    Username = vm.Username,
                    Email = vm.Email,
                    Password = vm.Password,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    PhoneNumber = vm.PhoneNumber,
                    Address = vm.Address
                };

                _accountRepository.EditUser(user);

                return RedirectToAction("Index" , "Home", new { message = "Update Done ..."});
            }

            return View(nameof(Profile), vm);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Username = vm.Username,
                    Email = vm.Email,
                    Password = vm.Password
                };
                _accountRepository.CreateUser(user);

                _emailSender.SendEmail(user.Email, "Welcome Message", $"<h3>Welcome {user.Username}</h3>");

                return RedirectToAction(nameof(Profile), new { id = user.UserId});
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _accountRepository.GetUserByUsername(vm.Username);

                if (user is null)
                    return NotFound();

                if (user.Password != vm.Password)
                {
                    ModelState.AddModelError("", "Username or Password is incorrect");
                    return View(vm);
                }

                return RedirectToAction(nameof(Profile), new { id = user.UserId });
            }
            return View(vm);
        }
    }
}
