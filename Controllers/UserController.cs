using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using password_manager.api.Constants;
using password_manager.api.Dtos.User;
using password_manager.api.Interfaces;
using password_manager.api.Models;

namespace password_manager.api.Controllers
{
    [Route($"api/{Constants.Constants.API_VERSION}/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _userRepository;
        public UserController(IMapper mapper, IUser userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadUserDto>> GetUsers()
        {
            var userList = _userRepository.GetAllUsers();

            return Ok(_mapper.Map<IEnumerable<ReadUserDto>>(userList));
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ReadUserDto> GetUserById(Guid id)
        {
            var user = _userRepository.GetUserById(id);

            if (user != null)
            {
                ReadUserDto userMap = _mapper.Map<ReadUserDto>(user);
                return Ok(userMap);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<ReadUserDto> CreateUser(CreateUserDto user)
        {
            var userModel = _mapper.Map<User>(user);
            userModel.Id = Guid.NewGuid();
            userModel.AccountLevel = "Master";
            userModel.Created = DateTimeOffset.Now;
            userModel.IsEnabled = true;

            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            var userRead = _mapper.Map<ReadUserDto>(userModel);

            return userRead;
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<ReadUserDto> UpdateUser(UpdateUserDto user, Guid id)
        {
            var userToUpdate = _userRepository.GetUserById(id);

            if (!String.IsNullOrEmpty(user.Firstname))
                userToUpdate.Firstname = user.Firstname;
            if (!String.IsNullOrEmpty(user.Lastname))
                userToUpdate.Lastname = user.Lastname;
            if (!String.IsNullOrEmpty(user.Email))
                userToUpdate.Email = user.Email;
            if (user.PhoneNumber > 0)
                userToUpdate.PhoneNumber = user.PhoneNumber;

            _userRepository.SaveChanges();

            var userRead = _mapper.Map<ReadUserDto>(userToUpdate);

            return userRead;
        }

        [HttpDelete]
        public ActionResult<bool> DeleteUser(Guid id, bool isSoftDelete)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            if (isSoftDelete)
            {
                user.IsEnabled = false;
            }
            else
            {
                _userRepository.DeleteUser(user);
            }

            _userRepository.SaveChanges();

            return true;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult<Guid> Login(LoginUserDto user)
        {
            var userModel = _mapper.Map<User>(user);
            var userToLogin = _userRepository.LoginUser(userModel);

            if (!String.IsNullOrEmpty(HttpContext.Session.GetString(UserSession.SessionKeyUserId)))
                return BadRequest();

            if (userToLogin != null)
            {
                HttpContext.Session.SetString(UserSession.SessionKeyUserId, userToLogin.Id.ToString());
                return Ok(Guid.Parse(HttpContext.Session.GetString(UserSession.SessionKeyUserId)));
            }

            return NotFound();

        }
    }
}