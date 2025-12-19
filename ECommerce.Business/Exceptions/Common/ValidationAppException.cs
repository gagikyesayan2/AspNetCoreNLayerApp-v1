
namespace Ecommerce.Business.Exceptions;

public sealed class ValidationAppException : AppException
{
    public ValidationAppException(string message, string errorCode = "validation_error")
        : base(message, 400, errorCode)
    {
    }
}
