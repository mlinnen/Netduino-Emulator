using System;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace Netduino.Sample1
{
	public class Program
	{
		public static void Main()
		{
			#region BlinkingLed
            InputPort sw1 = new InputPort(Pins.ONBOARD_SW1, false,Port.ResistorMode.Disabled);
            OutputPort led = new OutputPort(Pins.ONBOARD_LED, false);
            OutputPort[] ports = new OutputPort[14];
            ports[0] = new OutputPort(Pins.GPIO_PIN_D0, false);
            ports[1] = new OutputPort(Pins.GPIO_PIN_D1, false);
            ports[2] = new OutputPort(Pins.GPIO_PIN_D2, false);
            ports[3] = new OutputPort(Pins.GPIO_PIN_D3, false);
            ports[4] = new OutputPort(Pins.GPIO_PIN_D4, false);
            ports[5] = new OutputPort(Pins.GPIO_PIN_D5, false);
            ports[6] = new OutputPort(Pins.GPIO_PIN_D6, false);
            ports[7] = new OutputPort(Pins.GPIO_PIN_D7, false);
            ports[8] = new OutputPort(Pins.GPIO_PIN_D8, false);
            ports[9] = new OutputPort(Pins.GPIO_PIN_D9, false);
            ports[10] = new OutputPort(Pins.GPIO_PIN_D10, false);
            ports[11] = new OutputPort(Pins.GPIO_PIN_D11, false);
            ports[12] = new OutputPort(Pins.GPIO_PIN_D12, false);
            ports[13] = new OutputPort(Pins.GPIO_PIN_D13, false);
            int index = 0;
            bool newValue=false;
            DateTime expectedTime = DateTime.Now.AddSeconds(1);
            while (true)
			{
                bool state = sw1.Read();
                led.Write(state);
                if (DateTime.Now < expectedTime)
                {
                    continue;
                }
                expectedTime = DateTime.Now.AddSeconds(1);
                newValue = !newValue;
                ports[index].Write(newValue);
                if (newValue)
                    continue;
                else
                {
                    index++;
                    if (index > 13)
                        index = 0;
                }
            }
			#endregion


		}

	}
}
