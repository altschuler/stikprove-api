using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stikprove.Api.Models;
using Stikprove.Api.Models.Dto;
using Stikprove.Api.Security;

namespace Stikprove.Api.Controllers
{
    [TokenAuth]
    public class CompanyController : AbstractApiController
    {
        public IEnumerable<CompanyDto> GetCompanies()
        {
            return this.RepositoryContext.CompanyRepository.GetAll().AsEnumerable().Select(CompanyDto.Create);
        }

        public CompanyDto GetCompany(int id)
        {
            var company = this.RepositoryContext.CompanyRepository.GetById(id);
            if (company == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return CompanyDto.Create(company);
        }

        public HttpResponseMessage PutCompany(int id, Company company)
        {
            if (ModelState.IsValid && id == company.Id)
            {
                this.RepositoryContext.CompanyRepository.Update(id, company);

                try
                {
                    this.RepositoryContext.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        [Authorize(Roles = "Administrator")]
        public HttpResponseMessage PostCompany(Company company)
        {
            company.CreationDate = DateTime.Now;

            if (ModelState.IsValid)
            {
                this.RepositoryContext.CompanyRepository.Add(company);
                this.RepositoryContext.Save();

                var response = Request.CreateResponse(HttpStatusCode.Created, CompanyDto.Create(company));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage DeleteCompany(int id)
        {
            var company = this.RepositoryContext.CompanyRepository.GetById(id);
            if (company == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            this.RepositoryContext.CompanyRepository.Delete(company);

            try
            {
                this.RepositoryContext.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, CompanyDto.Create(company));
        }

        protected override void Dispose(bool disposing)
        {
            this.RepositoryContext.Dispose();
            base.Dispose(disposing);
        }
    }
}