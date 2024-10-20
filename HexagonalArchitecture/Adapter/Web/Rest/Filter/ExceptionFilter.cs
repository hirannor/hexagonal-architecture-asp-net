using HexagonalArchitecture.Application.Error;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HexagonalArchitecture.Adapter.Web.Rest.Filter;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred while processing your request.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = context.Exception.Message,
            Instance = context.HttpContext.Request.Path
        };

        switch (context.Exception)
        {
            case UserWithEmailAddressAlreadyExist emailAddressAlreadyExist:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = emailAddressAlreadyExist.Message;
                break;
            case UserRegistrationFailed userRegistrationFailed:
                problemDetails.Status = StatusCodes.Status409Conflict;
                problemDetails.Title = userRegistrationFailed.Message;
                problemDetails.Extensions.Add("errors", userRegistrationFailed.Errors);
                break;
            case UserAuthenticationFailed userAuthenticationFailed:
                problemDetails.Status = StatusCodes.Status401Unauthorized;
                problemDetails.Title = userAuthenticationFailed.Message;
                problemDetails.Extensions.Add("errors", userAuthenticationFailed.Errors);
                break;
            default:
                problemDetails.Title = "An unexpected error occurred.";
                break;
        }

        context.Result = new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };

        context.ExceptionHandled = true;
    }
    
}