using RoboApi.Models;

namespace RoboApi.Tests;

public class SequencialMovementTests
{
    private static readonly int[] ValidMoveSet = { 1, 2, 3, 4, 5 };

    [Fact]
    public void ShouldAllowSequentialMovementsInOrder()
    {
        var movement = new SequentialMovement<int>(ValidMoveSet, 3);

        var exception = Record.Exception(() =>
        {
            movement
                .MoveTo(4)
                .MoveTo(5)
                .MoveTo(4)
                .MoveTo(3)
                .MoveTo(2)
                .MoveTo(1)
                .MoveTo(2)
                .MoveTo(3);
        });

        Assert.Null(exception);
    }

    [Fact]
    public void ShouldNotAllowSkippingPositions()
    {
        var movement = new SequentialMovement<int>(ValidMoveSet, 1);

        Assert.Throws<InvalidOperationException>(() =>
            movement.MoveTo(3));
    }

    [Fact]
    public void ShouldThrowWhenStartPositionNotInMoveSet()
    {
        Assert.Throws<ArgumentException>(() =>
            new SequentialMovement<int>(ValidMoveSet, 10));
    }

    [Fact]
    public void ShouldNotAllowMoveToInvalidPosition()
    {
        var movement = new SequentialMovement<int>(ValidMoveSet, 1);

        Assert.Throws<InvalidOperationException>(() =>
            movement.MoveTo(10));
    }
}