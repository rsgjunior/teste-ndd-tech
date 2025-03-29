using RoboApi.Models;
using RoboApi.Models.Enums;

namespace RoboApi.Tests;

public class RoboTests
{
    [Fact]
    public void ShouldNotAllowWristRotationWhenElbowNotFullyContracted()
    {
        var robo = new Robo();

        Assert.Throws<InvalidOperationException>(() =>
            robo.RightArm.RotateWrist(WristPosition.Rotation45));
    }

    [Fact]
    public void ShouldAllowWristRotationWhenElbowFullyContracted()
    {
        var robo = new Robo();

        robo.LeftArm
            .RotateElbow(ElbowPosition.SlightlyContracted)
            .RotateElbow(ElbowPosition.Contracted)
            .RotateElbow(ElbowPosition.StronglyContracted);

        var exception = Record.Exception(() =>
            robo.LeftArm.RotateWrist(WristPosition.Rotation45));

        Assert.Null(exception);
    }

    [Fact]
    public void ShouldNotAllowHeadRotationWhenInclinationIsDown()
    {
        var robo = new Robo();

        Assert.Throws<InvalidOperationException>(() =>
            robo.Head
                .Incline(HeadInclination.Down)
                .Rotate(HeadRotation.Rotation45)
        );
    }

    [Fact]
    public void ShouldAllowHeadRotationWhenInclinationIsNotDown()
    {
        var robo = new Robo();

        var exception = Record.Exception(() =>
            robo.Head.Rotate(HeadRotation.Rotation45));

        Assert.Null(exception);
    }

}