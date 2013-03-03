using System;

namespace Stikprove.Api.Models.Dto
{
    public class UserDto : AbstractDto
    {
        public DateTime CreationDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Phone { get; set; }
        public string Email { get; set; }
        public CompanyDto Company { get; set; }
        public UserRoleDto Role { get; set; }
        public string EnergyUserName { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessExpiry { get; set; }

        static public UserDto Create(User user)
        {
            var dto = new UserDto()
            {
                Id = user.Id,
                CreationDate = user.CreationDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Role = UserRoleDto.Create(user.UserRole),
                EnergyUserName = user.EnergyUserName,
                AccessToken = user.AccessToken,
                AccessExpiry = user.AccessTokenExpiry
            };

            if (user.Company != null)
                dto.Company = CompanyDto.Create(user.Company);

            return dto;
        }
    }
}
