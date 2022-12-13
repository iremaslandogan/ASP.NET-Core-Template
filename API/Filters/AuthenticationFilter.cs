using Core.DTOs;
using Core.DTOs.UserDtos;
using Core.Utilities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API.Filters
{
    public class AuthenticationFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IList<int> _roles;

        public AuthenticationFilterAttribute(params Roles[] Roles) : base()
        {
            _roles = Roles.Select(x => (int)x).ToList();
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            var userRoleId = context.HttpContext.User.FindAll(ClaimTypes.Role).FirstOrDefault()?.Value;
            if (userRoleId == null)
            {
                // user authorize edilmemiş. UnAuth dön
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(401, "UNAUTHORIZED"));
            }
            else
            {
                //Console.WriteLine(int.Parse(Convert.ToBoolean(userRoleId)));
                if (_roles.Contains(int.Parse(userRoleId)))
                {
                    await next.Invoke();
                    return;
                }
                else
                {
                    context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(403, "FORBIDDEN"));

                }

            }

        }

    }
}
