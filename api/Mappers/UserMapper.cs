using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel)
        {
            return new UserDto
            {
                id = userModel.id,
                Name = userModel.Name
            };
        }
        public static User ToUserFromCreateDto(this CreateUserDto createUserDto)
        {
            return new User
            {
                Name = createUserDto.Name,
                phoneNumber = createUserDto.phoneNumber,
                password = createUserDto.password,
                type = "User",
                email = createUserDto.email
            };
        }
    }
}