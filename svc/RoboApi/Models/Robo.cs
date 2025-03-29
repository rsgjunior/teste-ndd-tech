namespace RoboApi.Models;

public class Robo
{
    public Arm RightArm { get; }
    public Arm LeftArm { get; }
    public Head Head { get; }

    public Robo()
    {
        RightArm = new Arm();
        LeftArm = new Arm();
        Head = new Head();
    }
}