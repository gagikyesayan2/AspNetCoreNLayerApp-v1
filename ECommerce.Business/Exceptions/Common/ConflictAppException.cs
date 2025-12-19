namespace Ecommerce.Business.Exceptions.Common;
public sealed class ConflictAppException : AppException
{
    public ConflictAppException(string message, string errorCode = "conflict")
        : base(message, 409, errorCode)
    {
    }
}

