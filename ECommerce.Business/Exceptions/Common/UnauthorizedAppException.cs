namespace Ecommerce.Business.Exceptions.Common;
public sealed class UnauthorizedAppException : AppException
{
    public UnauthorizedAppException(string message, string errorCode = "unauthorized")
        : base(message, 401, errorCode)
    {
    }
}
