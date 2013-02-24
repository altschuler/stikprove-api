using System;

namespace Stikprove.Api.Models.Dto
{
    public class CompanyDto : AbstractDto
    {
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int Cvr { get; set; }
        public string Address { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public int ResponseTime { get; set; }

        public static CompanyDto Create(Company company)
        {
            return new CompanyDto()
            {
                Id = company.Id,
                CreationDate = company.CreationDate,
                Name = company.Name,
                Cvr = company.Cvr,
                Address = company.Address,
                Zip = company.Zip,
                City = company.City,
                Phone = company.Phone,
                Email = company.Email,
                ResponseTime = company.ResponseTime
            };
        }
    }
}