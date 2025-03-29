using RoboApi.Models.Enums;

namespace RoboApi.Models;

public class Arm
{
    private Elbow elbow { get; }

    private Wrist wrist { get; }

    public WristState Wrist => new WristState(wrist.Rotation.CurrentPosition);
    public ElbowState Elbow => new ElbowState(elbow.Rotation.CurrentPosition);

    public Arm()
    {
        elbow = new Elbow();
        wrist = new Wrist();
    }

    public Arm RotateWrist(WristPosition position)
    {
        if (elbow.Rotation.CurrentPosition != ElbowPosition.StronglyContracted)
            throw new InvalidOperationException("Cannot rotate wrist unless elbow is strongly contracted");

        wrist.Rotation.MoveTo(position);

        return this;
    }

    public Arm RotateElbow(ElbowPosition position)
    {
        elbow.Rotation.MoveTo(position);

        return this;
    }
}