using RoboApi.Models;
using RoboApi.Models.Enums;

namespace RoboApi.Services;

public static class RoboService
{
    static Robo Robo { get; set; }

    static RoboService()
    {
        Robo = new Robo();
    }

    public static void Reset()
    {
        Robo = new Robo();
    }

    public static Robo Get() => Robo;

    public static Arm GetArm(ArmSide side) => side switch
    {
        ArmSide.Left => Robo.LeftArm,
        ArmSide.Right => Robo.RightArm,
        _ => throw new ArgumentException("Invalid arm side")
    };
}