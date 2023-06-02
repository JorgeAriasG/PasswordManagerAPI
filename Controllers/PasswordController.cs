using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        public ActionResult<ReadPasswordDto> CreatePassword(CreatePasswordDto password)
        {
            var passwordModel = _mapper.Map<Password>(password);
            _passwordRepository.CreatePassword(passwordModel);
            _passwordRepository.SaveChanges();

            ReadPasswordDto readPasswordDto = _mapper.Map<ReadPasswordDto>(passwordModel);

            return Ok(readPasswordDto);
        }
    }
}