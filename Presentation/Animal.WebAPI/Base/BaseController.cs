﻿using Animal.WebAPI.Authentication.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Animal.WebAPI.Base
{
    [BasicAuthorization]
    public class BaseController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Entities.User CurrentUser { get; set; }

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var accessor = _httpContextAccessor.HttpContext;

            // Access the user information only if the user is authenticated
            if (accessor.User.Identity.IsAuthenticated)
            {
                CurrentUser = new Entities.User();
                
                //dynamic
                foreach (var attribute in CurrentUser.GetType().GetProperties())
                {
                    var attributeValue = accessor.User.FindFirstValue(attribute.Name);
                    if (attributeValue != null)
                    {
                        attribute.SetValue(CurrentUser, Convert.ChangeType(attributeValue,attribute.PropertyType));
                    }
                }

                CurrentUser.Name = accessor.User.Identity.Name;
                CurrentUser.Email = accessor.User.FindFirstValue(ClaimTypes.Email);
                CurrentUser.DateOfBirth = accessor.User.FindFirstValue(ClaimTypes.DateOfBirth);
                CurrentUser.role = accessor.User.FindFirstValue(ClaimTypes.Role);
            }
        }
    }
}
