using LanguageExt.Common;

namespace HttpBuildR.RunTime;

public class HttpRunTimeException : Exception
{
    public HttpRunTimeException(Error error) : base(error.Message) { }
}

public record HttpRunTimeError : Error
{
    private readonly HttpRunTimeException _exception;

    public override bool Is<E>() => _exception is E;

    public override ErrorException ToErrorException() => ErrorException.New(Code, Message, ErrorException.New(_exception));

    public override string Message => _exception.Message;
    public override bool IsExceptional => true;
    public override bool IsExpected => false;

    private HttpRunTimeError(Error error)
    {
        _exception = new HttpRunTimeException(error);
    }

    public static HttpRunTimeError New(
        int errorCode,
        string errorMessage,
        Exception? exception = null
    ) =>
        exception is null
            ? new HttpRunTimeError(Error.New(errorCode, errorMessage))
            : new HttpRunTimeError(Error.New(errorCode, errorMessage, exception!));
}
