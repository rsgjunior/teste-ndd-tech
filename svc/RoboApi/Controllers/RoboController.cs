using Microsoft.AspNetCore.Mvc;
using RoboApi.Models;
using RoboApi.Models.Dtos;
using RoboApi.Models.Enums;
using RoboApi.Services;

namespace RoboApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoboController : ControllerBase
{
    [HttpGet]
    public ActionResult<Robo> Get() => RoboService.Get();

    [HttpPut("head/rotate")]
    public ActionResult<Robo> RotateHead([FromBody] HeadRotationDto rotation)
    {
        try
        {
            var robo = RoboService.Get();
            robo.Head.Rotate(rotation.Position);
            return Ok(robo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("head/incline")]
    public ActionResult<Robo> InclineHead([FromBody] HeadInclinationDto inclination)
    {
        try
        {
            var robo = RoboService.Get();
            robo.Head.Incline(inclination.Position);
            return Ok(robo);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("arm/{side}/elbow")]
    public ActionResult<Robo> MoveElbow(ArmSide side, [FromBody] ElbowPositionDto position)
    {
        try
        {
            var robo = RoboService.Get();
            var arm = RoboService.GetArm(side);
            arm.RotateElbow(position.Position);
            return Ok(robo);
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("arm/{side}/wrist")]
    public ActionResult<Robo> MoveWrist(ArmSide side, [FromBody] WristPositionDto position)
    {
        try
        {
            var robo = RoboService.Get();
            var arm = RoboService.GetArm(side);
            arm.RotateWrist(position.Position);
            return Ok(robo);
        }
        catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
        {
            return BadRequest(ex.Message);
        }
    }
}