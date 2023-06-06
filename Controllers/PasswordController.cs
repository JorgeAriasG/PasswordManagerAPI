using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using password_manager.api.Constants;
using password_manager.api.Dtos.Password;
using password_manager.api.Interfaces;
using password_manager.api.Models;

namespace password_manager.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPassword _passwordRepository;
        private readonly IMapper _mapper;
        public PasswordController(IPassword passwordRepository, IMapper mapper)
        {
            _passwordRepository = passwordRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReadPasswordDto>> GetAllPasswordByUser()
        {
            string userId = HttpContext.Session.GetString(UserSession.SessionKeyUserId);
            if (!String.IsNullOrEmpty(userId))
            {
                var passwords = _passwordRepository.GetAllPasswords(Guid.Parse(userId));

                if (passwords.Count() > 0)
                {
                    IEnumerable<ReadPasswordDto> passwordsMapped = _mapper.Map<IEnumerable<ReadPasswordDto>>(passwords);
                    return Ok(passwordsMapped);
                }
            }

            return NotFound();

        }

        //TODO: Maybe an endpoint to get a specific password

        [HttpPost]
        public ActionResult<ReadPasswordDto> CreatePassword(CreatePasswordDto password, Guid userId)
        {
            var passwordModel = _mapper.Map<Password>(password);
            passwordModel.UserId = userId;
            passwordModel.Creted = DateTimeOffset.Now;
            _passwordRepository.CreatePassword(passwordModel);
            _passwordRepository.SaveChanges();

            ReadPasswordDto readPasswordDto = _mapper.Map<ReadPasswordDto>(passwordModel);

            return Ok(readPasswordDto);
        }

        [HttpPatch]
        public ActionResult<ReadPasswordDto> UpdatePassword(UpdatePasswordDto password, int id, Guid userId)
        {
            var passwordToUpdate = _passwordRepository.GetPasswordById(userId, id);

            if (passwordToUpdate != null)
            {
                if (!String.IsNullOrEmpty(password.Site))
                    passwordToUpdate.Site = password.Site;

                if (!String.IsNullOrEmpty(password.Pword))
                    passwordToUpdate.Pword = password.Pword;

                if (!String.IsNullOrEmpty(password.Username))
                    passwordToUpdate.Username = password.Username;

                passwordToUpdate.Modified = DateTimeOffset.Now;
                passwordToUpdate.ModifiedBy = userId;

                _passwordRepository.SaveChanges();

                ReadPasswordDto passwordMapped = _mapper.Map<ReadPasswordDto>(passwordToUpdate);

                return Ok(passwordMapped);
            }

            return NotFound();
        }

        [HttpDelete]
        public ActionResult<bool> DeletePassword(Guid userId, int id)
        {
            var password = _passwordRepository.GetPasswordById(userId, id);

            if (password == null)
                return NotFound();

            _passwordRepository.DeletePassword(password);
            _passwordRepository.SaveChanges();

            return Ok(true);
        }
    }
}