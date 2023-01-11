namespace HttpBuildR.RunTime;

public static class ErrorExtensions
{
    public static HttpRunTimeError ToHttpRunTimeError(
        this int errorCode,
        string errorMessage,
        Exception? exception = null
    ) => HttpRunTimeError.New(errorCode, errorMessage, exception);
}
