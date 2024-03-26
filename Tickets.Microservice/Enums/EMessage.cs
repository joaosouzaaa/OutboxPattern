using System.ComponentModel;

namespace Tickets.Microservice.Enums;

public enum EMessage : ushort
{
    [Description("{0} has invalid length. It should be {1}.")]
    InvalidLength,

    [Description("{0} needs to be filled.")]
    Required,

    [Description("{0} was not found.")]
    NotFound

    [Description("{0} has to be greater than {1}.")]
    GreaterThan,
}
