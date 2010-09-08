using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SPOT.Emulator.Gpio;

namespace Netduino.Core
{
    /// <summary>
    /// A simple DTO that represents the state of an input pin on the netduino
    /// </summary>
	public class InputGpioEventArgs
	{
		private int _pin;
		private bool _edge;
        public InputGpioEventArgs(int pin, bool edge)
		{
            _pin = pin;
			_edge = edge;

		}

        /// <summary>
        /// What pin this event is for
        /// </summary>
		public int Pin
		{
            get { return _pin; }
            set { _pin = value; }
		}

        /// <summary>
        /// What state the pin transitioned to
        /// </summary>
		public bool Edge
		{
			get { return _edge; }
			set { _edge = value; }
		}
	}
}
