using System.Net;
using System.Security.Principal;
using Stikprove.Api.Models.Dto;

namespace Stikprove.Api.Models
{
    public partial class User : IIdentifiable, IPrincipal
    {
        public static User FromDto(UserDto dto)
        {
            var user = new User
            {
                Id = 1,
                CreationDate = dto.CreationDate,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                EnergyUserName = dto.EnergyUserName,
                Phone = dto.Phone,
            };

            if (dto.Role != null)
                user.UserRoleId = dto.Role.Id;

            if (dto.Company != null)
                user.CompanyId = dto.Company.Id;

            return user;
        }

        public static User FromDto(UserCreationDto dto)
        {
            var user = FromDto(dto as UserDto);
            user.Password = dto.Password;
            user.EnergyPassword = dto.EnergyPassword;
            return user;
        }

        public bool IsInRole(string role)
        {
            // TODO multiple roles
            return true;
        }

        public IIdentity Identity
        {
            get { return new HttpListenerBasicIdentity(this.Email, this.Password); }
        }
    }
}