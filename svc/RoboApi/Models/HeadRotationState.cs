using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class HeadRotationState
{
    public HeadRotation CurrentPosition { get; }

    public HeadRotationState(HeadRotation position)
    {
        CurrentPosition = position;
    }
}