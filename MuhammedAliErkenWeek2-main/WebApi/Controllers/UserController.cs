using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UserOperations.CreateUser;
using WebApi.UserOperations.DeleteUser;
using WebApi.UserOperations.GetUserDetail;
using WebApi.UserOperations.GetUsers;
using WebApi.UserOperations.UpdateUser;

namespace WebApi.Controllers
{
    [Route("[controller]s")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // readonly uygulama içerisinden değiştirilemez. Yalnızca constructor ile set edilebilir.
        private readonly BookShopDB _context;
        private readonly IMapper _mapper;

        public UserController(BookShopDB context, IMapper mapper) // constructor ile inject etme.
        {
            _context = context;
            _mapper = mapper;
        }

        // Tüm kullanıcıları getir
        [HttpGet]
        public IActionResult GetUsers()
        {
            GetUsersQuery query = new GetUsersQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // id'ye göre kullanıcı getir. FromRoute
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            UserDetailViewModel result;
            try
            {
                GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
                query.UserId = id;
                result = query.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        // yeni kullanıcı ekleme
        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = newUser;
            CreateUserCommandValidator validator = new CreateUserCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return StatusCode(201);
        }

        // var olan kullanıcıyı güncelleme
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UpdateUserModel updatedUser)
        {
            UpdateUserCommand command = new UpdateUserCommand(_context);
            try
            {
                command.UserId = id;
                command.Model = updatedUser;
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // User silme
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                DeleteUserCommand command = new DeleteUserCommand(_context);
                command.UserId = id;
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        // tek bir alanı güncelleme için patch yöntemi.
        [HttpPatch("{id}")]
        public IActionResult UpdateUserEmail(int id, [FromBody] string email)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);
            if (user is null)
                return BadRequest();

            user.Email = email != default ? email : user.Email;
            _context.SaveChanges();

            return Ok();
        }

        // Örn: /api/products/list?name=abc
        [HttpGet("list")]
        public ActionResult<List<User>> GetByFilter([FromQuery] string filter)
        {
            var searchedUserList = _context.Users.Where(u => u.FirstName == filter).ToList();


            return Ok(searchedUserList);
        }
    }
}