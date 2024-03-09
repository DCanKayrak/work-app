using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results.Concrete;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;

namespace WebApi.Middleware
{
    public class HttpExceptionHandler
    {
        private HttpResponse response;

        public Task HandleExceptionAsync(Exception exception) => exception switch
        {
            ValidationException => HandleValidationException(exception),
            _ => HandleException(exception)
        };

        public HttpResponse Response
        {
            get => response ?? throw new ArgumentNullException(nameof(response));
            set => response = value;
        }
        protected Task HandleValidationException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            ValidationException ex = (ValidationException)exception;
            string details = ex.Errors.First().ToString();
            return Response.WriteAsJsonAsync(new ErrorResult(details));
        }
        protected Task HandleException(Exception exception)
        {
            Response.StatusCode = StatusCodes.Status400BadRequest;
            string details = exception.Message;
            return Response.WriteAsJsonAsync(new ErrorResult(details));
        }
    }
}