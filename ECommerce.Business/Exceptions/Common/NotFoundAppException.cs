namespace Ecommerce.Business.Exceptions;

public sealed class NotFoundAppException : AppException
{
    public NotFoundAppException(string message, string errorCode = "not_found")
        : base(message, 404, errorCode)
    {
    }
}
