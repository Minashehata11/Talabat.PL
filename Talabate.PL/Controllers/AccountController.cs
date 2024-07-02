using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services;
using Talabate.PL.Dtos;
using Talabate.PL.ErrorsHandle;
using Talabate.PL.Extention;

namespace Talabate.PL.Controllers
{

    public class AccountController : BaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _token;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,ITokenService token,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
            _mapper = mapper;
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(UserDto),200)]
        [ProducesResponseType(typeof(ErrorApiResponse),400)]
        public async Task<ActionResult<UserDto>> Register([FromForm]ReqgisterOrLoginDto dto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(new ErrorApiResponse(400));
            AppUser user = new AppUser
            {
                Email = dto.Email,
                FullName = dto.Email.Split('@')[0],
                UserName = dto.Email.Split('@')[0],
            };
            var result= await _userManager.CreateAsync(user,dto.Password);
            if (!result.Succeeded)
                return BadRequest(new ErrorApiResponse(400));
            UserDto returnedData = new UserDto
            {
                Email = dto.Email,
                Name = user.UserName,
                Token = await _token.GenerateToken(user, _userManager)

            };
            return Ok(returnedData);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(ReqgisterOrLoginDto dto)
        {
           var user =await _userManager.FindByEmailAsync(dto.Email);
            if (user is  null)
                return Unauthorized(new ErrorApiResponse(401,"Not Found Email Create One"));
            var result =  await _signInManager.CheckPasswordSignInAsync(user, dto.Password,false);
            if(!result.Succeeded)
                return Unauthorized(new ErrorApiResponse(401));

            return Ok(new UserDto
            {
                Email = dto.Email,
                Name = user.UserName,
                Token = await _token.GenerateToken(user, _userManager)
            });


        }
       

        [Authorize]
        [HttpGet("GetAddress")]
        public async Task<ActionResult<AddressDto>> GetCurrentAddress()
        {
            var user = await  _userManager.GetUserWithAddress(User);
            var mappedAddress=_mapper.Map<AddressDto>(user.Address);    
            return Ok(mappedAddress);
            
        }
    }
}
