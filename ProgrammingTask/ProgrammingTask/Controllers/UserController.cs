using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProgrammingTask.Entity;
using ProgrammingTask.Models;
using ProgrammingTask.ViewModels;

namespace ProgrammingTask.Controllers
{
    public class UserController : Controller
    {
        private readonly ProgrammingTaskEntities _context;

        public UserController()
        {
            _context = new ProgrammingTaskEntities();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<AspNetUser> users = _context.AspNetUsers
                .Include("AspNetRoles")
                .ToList();

            return View(users);
        }

        public ActionResult EditUser(string userId)
        {
            AspNetUser user = _context.AspNetUsers.SingleOrDefault(x => x.Id == userId);
            UserEditViewModel userViewModel = new UserEditViewModel();
            userViewModel.UserId = user.Id;
            
            userViewModel.Email = user.Email;
            userViewModel.UserName = user.UserName;
            userViewModel.EmailConfirmed = user.EmailConfirmed;
            if (user.UserDetail != null)
            {
                userViewModel.Experience = user.UserDetail.Experience;
                userViewModel.Technology = user.UserDetail.Technology;
            }
            //else
            //{
            //    userViewModel.Experience = 0;
            //    userViewModel.Technology = ".net";
            //}

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult EditUser(UserEditViewModel userprofile)
        {
            if (ModelState.IsValid)
            {
                string username = userprofile.UserId;
                AspNetUser user = _context.AspNetUsers
                    .Include("UserDetail")
                    .SingleOrDefault(u => u.Id.Equals(username));

                user.Email = userprofile.Email;
                user.EmailConfirmed = userprofile.EmailConfirmed;

                if (user.UserDetail == null)
                {
                    user.UserDetail = new UserDetail();
                }
                user.UserDetail.Technology = userprofile.Technology;
                user.UserDetail.Experience = userprofile.Experience;

                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("EditUser", new{ userId = user.Id});
            }
            return RedirectToAction("Index");
        }

        public ActionResult DeleteUser(string userId)
        {
            var userToRemove = _context.AspNetUsers.SingleOrDefault(x => x.Id == userId);
            if (userToRemove != null)
            {
                _context.AspNetUsers.Remove(userToRemove);
                _context.SaveChanges();
            }
        
        return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult AddUserDetails()
        {
            var viewModel = new UserDetailsViewModel
            {
                Technologies = _context.Technologies.ToList()
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserDetails(UserDetailsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Technologies = _context.Technologies.ToList();
                return RedirectToAction("AddUserDetails", viewModel);
            }

            var userId = User.Identity.GetUserId();
            UserDetail userDetails = new UserDetail
            {
                Experience = viewModel.Experience,
                Technology = viewModel.Technology,
            };
            AspNetUser user = _context.AspNetUsers.SingleOrDefault(x => x.Id == userId);
            if (user != null)
            {
                user.UserDetail = userDetails;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}