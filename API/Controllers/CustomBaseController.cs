using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if(response.message == "OK")
            {
                return new ObjectResult(response)
                {
                    StatusCode = response.statusCode
                };
            }
            if (response.statusCode == 204 || response.statusCode == 201)
                return new ObjectResult(null)
                {
                    StatusCode = response.statusCode
                };
            if (response.data == null)
            {
                response.message = "NOTFOUND";
                return new ObjectResult(response)
                {
                    StatusCode = response.statusCode
                };
            }

            response.message = "OK";
            return new ObjectResult(response)
            {
                StatusCode = response.statusCode
            };


        }
    }
}
