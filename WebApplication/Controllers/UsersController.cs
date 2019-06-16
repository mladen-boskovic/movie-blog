using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.RoleCommands;
using Application.Commands.UserCommands;
using Application.DTO.InsertUpdateDTO;
using Application.Exceptions;
using Application.Searches;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGetUsersCommand getUsers;
        private readonly IGetUserCommand getUser;
        private readonly IAddUserCommand addUser;
        private readonly IEditUserCommand editUser;
        private readonly IDeleteUserCommand deleteUser;

        private readonly IGetAllRolesCommand getAllRoles;

        public UsersController(IGetUsersCommand getUsers, IGetUserCommand getUser, IAddUserCommand addUser, IEditUserCommand editUser, IDeleteUserCommand deleteUser, IGetAllRolesCommand getAllRoles)
        {
            this.getUsers = getUsers;
            this.getUser = getUser;
            this.addUser = addUser;
            this.editUser = editUser;
            this.deleteUser = deleteUser;
            this.getAllRoles = getAllRoles;
        }

        // GET: Users
        public ActionResult Index([FromQuery] UserSearch search)
        {
            try
            {
                var users = getUsers.Execute(search);
                return View(users.Data);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = getUser.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.Roles = getAllRoles.Execute();
                return View();
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InsertUpdateUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please enter a valid data.";
                return View(dto);
            }

            try
            {
                addUser.Execute(dto);
                TempData["success"] = "User successfully added.";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                ViewBag.Roles = getAllRoles.Execute();
                return View(dto);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var user = getUser.Execute(id);
                ViewBag.Roles = getAllRoles.Execute();

                var userInsUpd = new InsertUpdateUserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RoleId = user.RoleId,
                    Username = user.Username,
                    Password = user.Password
                };
                return View(userInsUpd);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InsertUpdateUserDto dto)
        {
            dto.Id = id;

            try
            {
                editUser.Execute(dto);
                TempData["success"] = "User successfully updated.";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
                ViewBag.Roles = getAllRoles.Execute();
                return View(dto);
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                deleteUser.Execute(id);
                TempData["success"] = "User successfully deleted.";
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyDeletedException)
            {
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "An error has occurred.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}