using System;
using BigDSignRestfulService.DTOs;
using SignData.ModelLayer;

namespace BigDSignRestfulService.ModelConversion
{
    public static class UserDTOConvert
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO(
                user.Id,
                user.UserRole?.ToUserRoleDTO(),
                user.FirstName,
                user.LastName,
                user.Email,
                user.LoginCredentials?.Username,
                null
            );
        }
    }
}
