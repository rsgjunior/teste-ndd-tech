using RoboApi.Models.Enums;

namespace RoboApi.Models.Dtos;

public class ArmMovementDto
{
    public ElbowPosition? ElbowPosition { get; set; }
    public WristPosition? WristPosition { get; set; }
}