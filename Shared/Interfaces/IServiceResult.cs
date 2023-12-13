using Shared.Enums;

namespace Shared.Interfaces;

public interface IServiceResult
{
    object Result { get; set; }
    ServiceStatus Status { get; set; }
}
