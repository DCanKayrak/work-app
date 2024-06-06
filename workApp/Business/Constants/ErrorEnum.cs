using System.Net;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;

namespace Business.Constants;

public class ErrorEnum
{
    /* POMODORO */
    public static readonly ErrorEnum GET_ALL_POMODOROS_WITH_USER_AND_DATE = new ErrorEnum(
        1001, 
        "get.all.pomodoros.with.user.and.date", 
        400
    );
    
    
    
    public readonly int Code;
    public readonly string MessageTemplate;
    public readonly int StatusCode;

    public ErrorEnum(int code, string messageTemplate, int statusCode)
    {
        Code = code;
        MessageTemplate = messageTemplate;
        StatusCode = statusCode;
    }
}