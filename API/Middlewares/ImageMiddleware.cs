using Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Service.Exceptions;
using System.Threading.Tasks;

namespace API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            List<Image> filePaths = new List<Image>();

            try
            {
                if (httpContext.Request.Form.Files.Count() > 0)
                {
                    try
                    {
                        //httpContext.Request.Form.Files -> IFormFile türünde
                        foreach (var formFile in httpContext.Request.Form.Files)
                        {
                            var path = "";
                            var location = "";
                            var extension = Path.GetExtension(formFile.FileName);
                            //var folderLocation = "";
                            //var fileName = "";
                            //var name = "";

                            extension = Path.GetExtension(formFile.FileName);
                            var imageName = Guid.NewGuid() + extension;
                            path = Path.Combine("/Images/", imageName);
                            location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", imageName);

                            using (var fileStream = new FileStream(location, FileMode.Create))
                            {
                                await formFile.CopyToAsync(fileStream);
                            }

                            filePaths.Add(new Image
                            {
                                Name=formFile.Name,
                                Path=path,
                            });

                        }

                        httpContext.Items["FilePaths"] = filePaths;
                        await _next(httpContext);
                    }
                    catch (Exception e)
                    {
                        throw new ClientSideException();
                    }
                }
                else
                {
                    httpContext.Items["FilePaths"] = filePaths;
                    await _next(httpContext);
                }
            }
            catch (Exception e)
            {
                throw new ClientSideException();
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ImageMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageMiddleware>();
        }
    }
}
