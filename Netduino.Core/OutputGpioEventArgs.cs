using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SPOT.Emulator.Gpio;

namespace Netduino.Core
{
	public class OutputGpioEventArgs
	{
		private int _pin;
		private bool _edge;

        /// <summary>
        /// A simple DTO that represents the state of an output pin on the netduino
        /// </summary>
        /// <param name="pin"></param>
        /// <param name="edge"></param>
		public OutputGpioEventArgs(int pin, bool edge)
		{
			_pin = pin;
			_edge = edge;

		}

        /// <summary>
        /// The pin this event belongs to
        /// </summary>
		public int Pin
		{
			get { return _pin; }
            set { _pin = value; }
		}

        /// <summary>
        /// The state the pin should transition to
        /// </summary>
		public bool Edge
		{
			get { return _edge; }
			set { _edge = value; }
		}
	}
}
