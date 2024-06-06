namespace Business.Constants;

public class CustomError : Exception
{
    public ErrorEnum ErrorEnum;

    public CustomError(ErrorEnum errorEnum)
    {
        ErrorEnum = errorEnum;
    }
}