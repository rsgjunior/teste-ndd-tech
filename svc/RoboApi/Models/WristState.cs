using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class WristState
{
    public WristPosition CurrentPosition { get; }

    public WristState(WristPosition position)
    {
        CurrentPosition = position;
    }
}