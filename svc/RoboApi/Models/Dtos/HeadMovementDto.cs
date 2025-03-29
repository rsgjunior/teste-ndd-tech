using RoboApi.Models.Enums;

namespace RoboApi.Models.Dtos;

public class HeadMovementDto
{
    public HeadRotation? Rotation { get; set; }
    public HeadInclination? Inclination { get; set; }
}