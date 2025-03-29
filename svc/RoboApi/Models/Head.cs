using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class Head
{
    private static readonly HeadRotation[] ValidRotations =
    [
        HeadRotation.Rotation90Negative,
        HeadRotation.Rotation45Negative,
        HeadRotation.Rest,
        HeadRotation.Rotation45,
        HeadRotation.Rotation90
    ];

    private static readonly HeadInclination[] ValidInclinations =
    [
        HeadInclination.Up,
        HeadInclination.Rest,
        HeadInclination.Down
    ];

    private SequencialMovement<HeadRotation> rotation { get; }

    private SequencialMovement<HeadInclination> inclination { get; }

    public HeadRotationState Rotation => new HeadRotationState(rotation.CurrentPosition);
    public HeadInclinationState Inclination => new HeadInclinationState(inclination.CurrentPosition);

    public Head()
    {
        rotation = new SequencialMovement<HeadRotation>(ValidRotations, HeadRotation.Rest);
        inclination = new SequencialMovement<HeadInclination>(ValidInclinations, HeadInclination.Rest);
    }

    public Head Rotate(HeadRotation position)
    {
        if (inclination.CurrentPosition == HeadInclination.Down)
            throw new InvalidOperationException("Cannot rotate head while inclination is down");

        rotation.MoveTo(position);

        return this;
    }

    public Head Incline(HeadInclination position)
    {
        inclination.MoveTo(position);

        return this;
    }
}