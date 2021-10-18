using System.ComponentModel;
namespace NgTemplate.API.DTOs.Enums
{
    public enum ExceptionType
    {
        [Description("Not found exception")]
        NotFoundException = 1,
        [Description("Operation failed exception")]
        OperationFailedException,
        [Description("Internal error exception")]
        InternalErrorException
    }
}