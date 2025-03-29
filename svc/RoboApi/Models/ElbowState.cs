using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class ElbowState
{
    public ElbowPosition CurrentPosition { get; }

    public ElbowState(ElbowPosition position)
    {
        CurrentPosition = position;
    }
}