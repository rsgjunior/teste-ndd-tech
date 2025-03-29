using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class HeadInclinationState
{
    public HeadInclination CurrentPosition { get; }

    public HeadInclinationState(HeadInclination position)
    {
        CurrentPosition = position;
    }
}