using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class Elbow
{
    private static readonly ElbowPosition[] ValidRotations =
    [
        ElbowPosition.Rest,
        ElbowPosition.SlightlyContracted,
        ElbowPosition.Contracted,
        ElbowPosition.StronglyContracted
    ];

    public SequentialMovement<ElbowPosition> Rotation { get; }

    public Elbow(ElbowPosition startPosition = ElbowPosition.Rest)
    {
        Rotation = new SequentialMovement<ElbowPosition>(ValidRotations, startPosition);
    }
}