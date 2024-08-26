using FluentValidation;
using Microsoft.AspNetCore.Http;
namespace WebApp.Core.Application.Helpers.Behaviours
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var firstError = ex.Errors
                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                    .FirstOrDefault() ?? "Validation error occurred";

                var response = new
                {
                    error = firstError
                };

                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
               
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = new
                {
                    message = "An unexpected error occurred. Please try again later",
                    details = ex.Message 
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

}
