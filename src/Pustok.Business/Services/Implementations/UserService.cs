    using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pustok.Business.DTOs.UserDtos;
using Pustok.Business.Exceptions.UserExceptions;
using Pustok.Business.Services.Interfaces;
using Pustok.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateUserAsnyc(UserPostDto userPostDto)
        {
            var user =  _mapper.Map<AppUser>(userPostDto);

            var identityResult = await _userManager.CreateAsync(user, userPostDto.Password);
            if (!identityResult.Succeeded)
                throw new CreateUserFailException(identityResult.Errors);
        }
    }
}
