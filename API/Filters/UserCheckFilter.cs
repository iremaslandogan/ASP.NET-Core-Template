using Core.DTOs;
using Core.Utilities.Enum;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace API.Filters
{
    public class UserCheckFilterAttribute : Attribute, IAsyncActionFilter
    {

        public UserCheckFilterAttribute()
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.HttpContext.User.Claims.First(x => x.Type == "id").Value;
            var userRoleId = context.HttpContext.User.FindAll(ClaimTypes.Role).FirstOrDefault()?.Value;
            var userId = context.ActionArguments.ContainsKey("userId") ? context.ActionArguments["userId"] : null;
            if(userRoleId != Role.Admin && userId == null)
            {
                context.Result = new BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400, "BADREQUEST"));
            }
            else if (userRoleId != Role.Admin && int.Parse(idValue) != (int)userId)
            {
                context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(403, "FORBIDDEN"));
            }
            else
            {
                await next.Invoke();
                return;
            }
        }
    }
}
