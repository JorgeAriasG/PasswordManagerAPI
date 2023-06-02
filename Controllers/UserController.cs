using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using password_manager.api.Dtos;
using password_manager.api.Interfaces;
using password_manager.api.Models;

namespace password_manager.api.Controllers
{
    [Route("api/v1/[controller]")]
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
            _userRepository.CreateUser(userModel);
            _userRepository.SaveChanges();

            var userRead = _mapper.Map<ReadUserDto>(userModel);

            return userRead;
        }

        [HttpPatch]
        [Route("{id}")]
        public ActionResult<ReadUserDto> UpdateUser([FromBody] UpdateUserDto user, Guid id)
        {
            var userModel = _mapper.Map<User>(user);
            _userRepository.UpdateUser(userModel, id);
            _userRepository.SaveChanges();

            var userRead = _mapper.Map<ReadUserDto>(userModel);

            return userRead;
        }
    }
}