using AutoMapper;
using FasgionGrid.UserService.Data;
using FasgionGrid.UserService.Models;
using FasgionGrid.UserService.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System;

namespace FasgionGrid.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJWTTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext db, IJWTTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,IMapper mapper)
        {
            _db = db;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Email);
            bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (isvalid == false || user == null)
            {//if user was not found
                return new LoginResponseDto() { User = null, Token = "" };
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            UserDto userDto = _mapper.Map<UserDto>(user);


            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            //Registering a new user to the system
            ApplicationUser user = _mapper.Map<ApplicationUser>(registrationRequestDto);
            user.Surname = "";

            try
            {
                //Creating the customer user
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                   await AssignRole(user.Email, "CUSTOMER");
                   
                    var registeredUser = await _userManager.FindByEmailAsync(user.Email);

                    UserDto userDto = _mapper.Map<UserDto>(registeredUser);


                    return "";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }
            return "Error Encountered";
        }

        public async Task<string> RegisterDealer(RegistrationRequestDto registrationRequestDto)
        {
            //Registering a new user to the system
            ApplicationUser user = _mapper.Map<ApplicationUser>(registrationRequestDto);
            user.Surname = "";

            try
            {
                //Creating the dealer user
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    await AssignRole(user.Email, "DEALER");
                   
                    var registeredUser = await _userManager.FindByEmailAsync(user.Email);

                    UserDto userDto = _mapper.Map<UserDto>(registeredUser);


                    return "";
                }
            }
            catch (Exception x)
            {
                return x.Message;
            }
            return "Error Encountered";
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            { 
                return false;
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                // Create the role only if it doesn't exist
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }

            // Add the user to the role
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }
    }
}

