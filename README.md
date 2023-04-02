# CycleStream

CycleStream is an IoT solution designed to collect and analyze data gathered from bicycle riding sessions. Planned features include:

* Environmental monitoring of temperature, humidity, pressure, and air quality ([Bosch BME688](https://www.bosch-sensortec.com/products/environmental-sensors/gas-sensors/bme688/)).
* Acceleration monitoring ([Bosch BMI270](https://www.bosch-sensortec.com/products/motion-sensors/imus/bmi270))
* Stability monitoring ([Bosch BMI270](https://www.bosch-sensortec.com/products/motion-sensors/imus/bmi270)).
* Light Tracking ([Rohm BH1750](https://www.biomaker.org/block-catalogue/2021/12/17/lux-light-sensor-bh1750))
* GPS Tracking (Unit TBD).
* Web interface to visualize data using [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor). 

CycleStream is built on the Meadow Platform from [Wilderness Labs](https://www.wildernesslabs.co/) and uses a Meadow F7V2 microcontroller to interface with the various sensors and transmit data to the Cyclestream server. Meadow is a complete, IoT platform with defense-grade security that runs full .NET Standard applications on embeddable microcontrollers.

The project is currently in an early alpha stage and under active development.