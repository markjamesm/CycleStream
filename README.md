# CycleStream

CycleStream is an IoT solution designed to collect and analyze data gathered from bicycle riding sessions. 

## Planned features

* Environmental monitoring of temperature, humidity, pressure, and air quality ([Bosch BME688](https://www.bosch-sensortec.com/products/environmental-sensors/gas-sensors/bme688/)).
* Acceleration tracking ([Bosch BMI270](https://www.bosch-sensortec.com/products/motion-sensors/imus/bmi270)).
* Light Tracking ([Rohm BH1750](https://www.biomaker.org/block-catalogue/2021/12/17/lux-light-sensor-bh1750)) - Only supports readings of up to 65k lux, but it may be possible to boost this to 100k.
* GPS Tracking (Unit TBD).
* Web interface to visualize data using [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor).

Note that the project is currently in a very early alpha stage and the featureset is subject to change.

## Project Architecture

CycleStream is built on the [Meadow Platform](https://www.wildernesslabs.co/), a complete, IoT platform with defense-grade security that runs full .NET Standard applications on embeddable microcontrollers. 

The project uses a [Meadow F7V2](https://store.wildernesslabs.co/products/meadow-f7) attached to to a [Project Lab](https://store.wildernesslabs.co/collections/frontpage/products/project-lab-board) I/O expander board to interface with the various sensors and transmit data to the Cyclestream server.
