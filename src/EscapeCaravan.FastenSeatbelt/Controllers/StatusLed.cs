using System.Device.Gpio;
using System.Device.Gpio.Drivers;
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
        Console.WriteLine($"Request status of pin {Pin}");

        var controller = new GpioController(PinNumberingScheme.Logical, new RaspberryPi3Driver() );
        
        controller.OpenPin(Pin, PinMode.Input);
        var pinStatus = controller.Read(Pin);
        controller.ClosePin(Pin);

        Console.WriteLine($"Pin status found for pin {Pin}, was {pinStatus}");
        return Ok(new PinStatus
        {
            Pin = Pin,
            Status = pinStatus.ToString()
        });
    }


    [HttpPost]
    public IActionResult SetPinStatus(PinStatus dto)
    {
        Console.WriteLine($"Request to set pin {Pin}, to status {dto.Status}");

        if (dto.Pin != Pin)
        {
            Console.WriteLine($"Uncontrollable pin addressed. Returning bad request");
            return BadRequest($"This pin cannot be controlled");
        }

        var desiredPinValue = dto.Status.ToLower().EndsWith("high") ? 
            PinValue.High : PinValue.Low;
        Console.WriteLine($"Desired pin status is now set to {desiredPinValue}");

        var controller = new GpioController();
        controller.OpenPin(Pin, PinMode.Output);
        controller.Write(Pin, desiredPinValue);
        controller.ClosePin(Pin);
        Console.WriteLine($"Setting new pin status succeeded");

        return Ok(new PinStatus()
        {
            Pin = Pin,
            Status = desiredPinValue.ToString()
        });
    }


}
