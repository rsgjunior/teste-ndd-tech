using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class Wrist
{
    private static readonly WristPosition[] ValidRotations =
    [
        WristPosition.Rotation90Negative,
        WristPosition.Rotation45Negative,
        WristPosition.Rest,
        WristPosition.Rotation45,
        WristPosition.Rotation90,
        WristPosition.Rotation135,
        WristPosition.Rotation180
    ];

    public SequentialMovement<WristPosition> Rotation { get; }

    public Wrist(WristPosition startPosition = WristPosition.Rest)
    {
        Rotation = new SequentialMovement<WristPosition>(ValidRotations, startPosition);
    }
}