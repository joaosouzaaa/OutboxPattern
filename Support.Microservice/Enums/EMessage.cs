using System.ComponentModel;

namespace Support.Microservice.Enums;

public enum EMessage : ushort
{
    [Description("{0} has invalid length. It should be {1}.")]
    InvalidLength,

    [Description("{0} is in invalid format.")]
    InvalidFormat,

    [Description("{0} was not found.")]
    NotFound
}
