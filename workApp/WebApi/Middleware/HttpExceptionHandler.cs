using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Results.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Business.Constants;
using Core.Utilities.Localization;
using ValidationException = FluentValidation.ValidationException;

namespace WebApi.Middleware
{
    public class HttpExceptionHandler
    {
        private HttpResponse response;
        private IHttpContextAccessor _httpContextAccessor;

        public HttpExceptionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Task HandleExceptionAsync(Exception exception) => exception switch
        {
            ValidationException => HandleValidationException(exception),
            CustomError => HandleCustomException(exception),
            _ => HandleException(exception)
        };

        public HttpResponse Response
        {
            get => response ?? throw new ArgumentNullException(nameof(response));
            set => response = value;
        }

        protected Task HandleCustomException(Exception exception)
        {
            CustomError ex = (CustomError)exception;
            Response.StatusCode = ex.ErrorEnum.StatusCode;
            string acceptLang = _httpContextAccessor.HttpContext.Request.Headers["Accept-Language"];
            string culture = acceptLang != null ? acceptLang : "en";
            string details = LocalizationManager.GetLocalizedMessages(ex.ErrorEnum.MessageTemplate, culture);
            return Response.WriteAsJsonAsync(new ErrorResult(details));
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