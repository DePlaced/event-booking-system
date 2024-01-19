using System;
using RESTfulService.DTOs;
using Data.ModelLayer;

namespace RESTfulService.ModelConversion
{
    public static class UserRoleDTOConvert
    {
        public static UserRoleDTO ToUserRoleDTO(this UserRole userRole)
        {
            return new UserRoleDTO(
                userRole.Id,
                userRole.Role
            );
        }

        public static UserRole ToUserRole(this UserRoleDTO userRoleDTO)
        {
            return new UserRole(
                userRoleDTO.Id,
                userRoleDTO.Role
            );
        }
    }
}
