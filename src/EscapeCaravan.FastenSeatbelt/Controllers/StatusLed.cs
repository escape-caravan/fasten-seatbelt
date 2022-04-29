using System.Device.Gpio;
using EscapeCaravan.FastenSeatbelt.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscapeCaravan.FastenSeatbelt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusLed : ControllerBase
{

    private const int Pin = 17; // GPIO 17 (Physical pin 11)


    [HttpGet]
    public IActionResult GetPinStatus()
    {
        var controller = new GpioController();

        controller.OpenPin(Pin, PinMode.Input);
        var pinStatus = controller.Read(Pin);
        controller.ClosePin(Pin);

        return Ok(new PinStatus
        {
            Pin = Pin,
            Status = pinStatus.ToString()
        });
    }


    [HttpPost]
    public IActionResult SetPinStatus(PinStatus dto)
    {
        if (dto.Pin != Pin)
        {
            return BadRequest("This pin cannot be controlled");
        }

        var desiredPinValue = dto.Status.ToLower().EndsWith("high") ? 
            PinValue.High : PinValue.Low;

        var controller = new GpioController();

        controller.OpenPin(Pin, PinMode.Output);
        controller.Write(Pin, desiredPinValue);
        controller.ClosePin(Pin);

        return Ok(new PinStatus()
        {
            Pin = Pin,
            Status = desiredPinValue.ToString()
        });
    }


}
