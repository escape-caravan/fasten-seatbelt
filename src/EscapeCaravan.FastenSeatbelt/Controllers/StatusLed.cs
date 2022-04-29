using EscapeCaravan.FastenSeatbelt.Models;
using Iot.Device.CpuTemperature;
using Microsoft.AspNetCore.Mvc;

namespace EscapeCaravan.FastenSeatbelt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusLed : ControllerBase
{

    [HttpGet]
    public IActionResult Get()
    {
        using CpuTemperature cpuTemperature = new CpuTemperature();
        if (cpuTemperature.IsAvailable)
        {
            var temperature = cpuTemperature.ReadTemperatures();
            foreach (var entry in temperature)
            {
                if (!double.IsNaN(entry.Temperature.DegreesCelsius))
                {
                    return Ok(new TemperatureModel
                    {
                        Temperature = entry.Temperature.DegreesCelsius
                    });
                }

                return BadRequest("Unable to read Temperature.");
            }
        }
        else
        {
            return BadRequest("CPU temperature is not available");
        }
        return BadRequest("Unknown error");
    }

}
