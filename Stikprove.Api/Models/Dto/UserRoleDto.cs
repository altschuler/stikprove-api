using System;

namespace Stikprove.Api.Models.Dto
{
    public class UserRoleDto : AbstractDto
    {
        public string Name { get; set; }

        public static UserRoleDto Create(UserRole userRole)
        {
            return new UserRoleDto()
            {
                Id = userRole.Id,
                Name = userRole.Name
            };
        }
    }
}