﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Stikprove.Api.Models;
using Stikprove.Api.Models.Dto;

namespace Stikprove.Api.Controllers
{
    [Authorize]
    public class UserController : AbstractApiController
    {
        public IEnumerable<UserDto> GetUsers()
        {
            return this.RepositoryContext.UserRepository.GetAll().AsEnumerable().Select(UserDto.Create);
        }

        public UserDto GetUser(int id)
        {
            var user = this.RepositoryContext.UserRepository.GetById(id);
            if (user == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return UserDto.Create(user);
        }

        public HttpResponseMessage PutUser(int id, User user)
        {
            if (ModelState.IsValid && id == user.Id)
            {
                this.RepositoryContext.UserRepository.Update(id, user);

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

        public HttpResponseMessage PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if user name is taken - return 409 CONFLICT if it is
                if (this.RepositoryContext.UserRepository.GetAll().Any(u => u.Email == user.Email))
                    return Request.CreateResponse(HttpStatusCode.Conflict);

                user.CreationDate = DateTime.Now;
                user.Salt = Guid.NewGuid().ToString();

                this.RepositoryContext.UserRepository.Add(user);

                this.RepositoryContext.Save();

                return Request.CreateResponse(HttpStatusCode.Created, UserDto.Create(user));
            }
            
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage DeleteUser(int id)
        {
            var user = this.RepositoryContext.UserRepository.GetById(id);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var dto = UserDto.Create(user);
            this.RepositoryContext.UserRepository.Delete(user);

            try
            {
                this.RepositoryContext.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, dto);
        }

        protected override void Dispose(bool disposing)
        {
            this.RepositoryContext.Dispose();
            base.Dispose(disposing);
        }
    }
}