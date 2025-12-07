using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers{
public static class UserMapper
{
    public static UserDto ToUserDto(this User userModel)
    {
        return new UserDto
        {
            id = userModel.id,
            Name = userModel.Name,
            phoneNumber = userModel.phoneNumber,
            email = userModel.email,          // include email if needed
            IsSuspended = userModel.IsSuspended
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

    // Map from getUserDto → User
    public static User ToUserFromGetUserDto(this getUserDto dto)
    {
        return new User
        {
            Name = dto.Name,
            phoneNumber = dto.phoneNumber,
            email = dto.email,
            type = "User",
            IsSuspended = dto.IsSuspended
        };
    }

    // Optional: Map from User → getUserDto
    public static getUserDto ToGetUserDto(this User user)
    {
        return new getUserDto
        {
            Name = user.Name,
            phoneNumber = user.phoneNumber,
            email = user.email,
            IsSuspended = user.IsSuspended
        };
    }
}
}