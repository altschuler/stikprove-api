using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Stikprove.Api.Models;
using Stikprove.Api.Models.Dto;

namespace Stikprove.Tests.Models
{
    [TestClass]
    public class TestModels
    {
        [TestMethod]
        public void TestUserModel()
        {
            var now = DateTime.Now;
            var user = new User
            {
                Id = 1,
                CreationDate = now,
                FirstName = "firstName",
                LastName = "lastName",
                Email = "a@b.c",
                Password = "pass",
                Salt = "salt",
                EnergyPassword = "epass",
                EnergyUserName = "euser",
                Phone = 12345678,
                //Roles = new List<UserRole>() { new UserRole() { Id = 1, Name = "role" } },
                Company = new Company(),
                //AccessToken = "foobar",
            };

            var dto = UserDto.Create(user);

            Assert.AreEqual(user.Id, dto.Id);
            Assert.AreEqual(user.CreationDate, dto.CreationDate);
            Assert.AreEqual(user.FirstName, dto.FirstName);
            Assert.AreEqual(user.LastName, dto.LastName);
            Assert.AreEqual(user.Email, dto.Email);
            Assert.AreEqual(user.EnergyUserName, dto.EnergyUserName);
            Assert.AreEqual(user.Phone, dto.Phone);
            //Assert.AreEqual(user.UserRole.Name, dto.Role.Name);
            //Assert.AreEqual(user.AccessToken, dto.AccessToken);

            // Create user with only required fields
            var simpleUser = new User
            {
                Id = 1,
                CreationDate = now,
                FirstName = "firstName",
                LastName = "lastName",
                Email = "a@b.c",
                Password = "pass",
                Salt = "salt",
                //UserRole = new UserRole() { Id = 1, Name = "role" },
            };

            var simpleDto = UserDto.Create(simpleUser);

            Assert.IsNull(simpleDto.Company);
            Assert.IsNull(simpleDto.Phone);
            Assert.IsNull(simpleDto.EnergyUserName);
        }

        [TestMethod]
        public void TestCompanyModel()
        {
            var now = DateTime.Now;
            var company = new Company
            {
                Id = 1,
                CreationDate = now,
                Name = "firstName",
                Cvr = 100,
                Address = "address",
                Zip = 200,
                City = "city",
                Phone = 12345678,
                Email = "a@b.c",
                ResponseTime = 300
            };

            var dto = CompanyDto.Create(company);

            Assert.AreEqual(company.Id, dto.Id);
            Assert.AreEqual(company.CreationDate, dto.CreationDate);
            Assert.AreEqual(company.Name, dto.Name);
            Assert.AreEqual(company.Cvr, dto.Cvr);
            Assert.AreEqual(company.Address, dto.Address);
            Assert.AreEqual(company.Zip, dto.Zip);
            Assert.AreEqual(company.City, dto.City);
            Assert.AreEqual(company.Phone, dto.Phone);
            Assert.AreEqual(company.Email, dto.Email);
            Assert.AreEqual(company.ResponseTime, dto.ResponseTime);
        }

        [TestMethod]
        public void TestUserRoleModel()
        {
            var role = new UserRole
            {
                Id = 1,
                Name = "firstName",
            };

            var dto = UserRoleDto.Create(role);

            Assert.AreEqual(role.Id, dto.Id);
            Assert.AreEqual(role.Name, dto.Name);
        }

        [TestMethod]
        public void TestTranslationModel()
        {
            var translation = new Translation
            {
                Id = 1,
                Label = "label",
            };

            var dto = TranslationDto.Create(translation);

            Assert.AreEqual(translation.Id, dto.Id);
            Assert.AreEqual(translation.Label, dto.Label);
        }
    }
}
