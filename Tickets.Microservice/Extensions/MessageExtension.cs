using System.ComponentModel;
using Tickets.Microservice.Enums;

namespace Tickets.Microservice.Extensions;

public static class MessageExtension
{
    public static string Description(this EMessage message)
    {
        var memberInfo = typeof(EMessage).GetMember(message.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return ((DescriptionAttribute)attributes[0]).Description;
    }
}
