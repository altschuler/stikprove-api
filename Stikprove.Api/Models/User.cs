using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var role in dto.Roles)
            {
                user.UserUserRoleRelation.Add(new UserUserRoleRelation
                {
                    UserId = user.Id,
                    UserRoleId = role.Id
                });
            }

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
            return this.Roles.Any(r => r.Name.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<UserRole> Roles
        {
            get { return this.UserUserRoleRelation.Select(r => r.UserRole); }
        }

        public IIdentity Identity
        {
            get { return new HttpListenerBasicIdentity(this.Email, this.Password); }
        }
    }
}