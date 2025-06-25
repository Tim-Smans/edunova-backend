using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduNova.Api.Middleware
{
    public class ExceptionMiddleware
    {
        // EF Core Dependency: RequestDelegate is the "next" step in the HTTP-pipeline.
        private readonly RequestDelegate _next;
        // DI to log errors
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // This is the core method that will be invoked on every request
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Try to make further the request in the pipeline
                await _next(httpContext); 
            } catch (Exception ex)
            {
                // if an exception rises, log it with full exception detail
                _logger.LogError(ex, $"Something went wrong: {ex.Message}");

                // Handle the exception and return a clean API response
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        // This method wil build a error response and returns a ProblemDetails object.
        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //Put the response header to JSON in the ProblemDetails format
            httpContext.Response.ContentType = "application/problem+json";

            //Get and put the HTTP-status code
            int statusCode = ex switch
            {
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;


            //Build the ProblemDetails object, it gives info on the error
            ProblemDetails problem = new ProblemDetails
            {
                Status = statusCode, 
                Title = "An unexpected error occurred!",
                Detail = ex.Message,
                Instance = httpContext.Request.Path
            };

            // Send the ProblemDetails object as JSON in a response
            return httpContext.Response.WriteAsJsonAsync(problem);
        }
    }
}
