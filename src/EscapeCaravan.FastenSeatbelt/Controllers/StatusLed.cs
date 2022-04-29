using System.Device.Gpio;
using EscapeCaravan.FastenSeatbelt.Models;
using Microsoft.AspNetCore.Mvc;

namespace EscapeCaravan.FastenSeatbelt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusLed : ControllerBase
{
    private readonly ILogger _logger;

    private const int Pin = 17; // GPIO 17 (Physical pin 11)


    [HttpGet]
    public IActionResult GetPinStatus()
    {
        _logger.LogInformation("Request status of pin {pin}", Pin);

        var controller = new GpioController();
        controller.OpenPin(Pin, PinMode.Input);
        var pinStatus = controller.Read(Pin);
        controller.ClosePin(Pin);

        _logger.LogInformation("Pin status found for pin {pin}, was {status}", Pin, pinStatus);
        return Ok(new PinStatus
        {
            Pin = Pin,
            Status = pinStatus.ToString()
        });
    }


    [HttpPost]
    public IActionResult SetPinStatus(PinStatus dto)
    {
        _logger.LogInformation("Request to set pin {pin}, to status {status}", Pin, dto.Status);

        if (dto.Pin != Pin)
        {
            _logger.LogInformation("Uncontrollable pin addressed. Returning bad request");
            return BadRequest("This pin cannot be controlled");
        }

        var desiredPinValue = dto.Status.ToLower().EndsWith("high") ? 
            PinValue.High : PinValue.Low;
        _logger.LogInformation("Desired pin status is now set to {desiredStatus}", desiredPinValue);

        var controller = new GpioController();
        controller.OpenPin(Pin, PinMode.Output);
        controller.Write(Pin, desiredPinValue);
        controller.ClosePin(Pin);
        _logger.LogInformation("Setting new pin status succeeded");

        return Ok(new PinStatus()
        {
            Pin = Pin,
            Status = desiredPinValue.ToString()
        });
    }


    public StatusLed(ILogger logger)
    {
        _logger = logger;
    }

}
