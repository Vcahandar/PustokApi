using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pustok.Business.DTOs.CommonDtos;
using Pustok.Business.DTOs.UserDtos;
using Pustok.Business.Services.Interfaces;
using System.Net;

namespace Pustok.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserPostDto userPostDto)
        {
            await _userService.CreateUserAsnyc(userPostDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(HttpStatusCode.Created,
                                                          "User Successfully created"));
        }
    }
}
