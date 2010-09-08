using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Microsoft.SPOT.Emulator.Gpio;

namespace Netduino.Core.ViewModels
{
    [Export(typeof(IEmulatorViewModel))]
    public class GenericViewModel : Screen,IEmulatorViewModel, IHandle<NetduinoGpioEvent>
	{
		private readonly IEventAggregator _eventAggregator;
		[ImportingConstructor]
        public GenericViewModel(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
			_eventAggregator.Subscribe(this);
		}
        private string _switch1ButtonText = "Switch LED On";
        public string Switch1ButtonText
        {
            get { return _switch1ButtonText; }
            set
            {
                if (_switch1ButtonText != value)
                {
                    _switch1ButtonText = value;
                    NotifyOfPropertyChange(() => Switch1ButtonText);
                }
            }

        }
        private bool _switchState;
		private bool _dio0;
		public bool DIO0
		{
			get { return _dio0; }
			set
			{
				if (_dio0 != value)
				{
					_dio0 = value;
					NotifyOfPropertyChange(() => DIO0);
				}
			}
		}
		private bool _dio1;
		public bool DIO1
		{
			get { return _dio1; }
			set
			{
				if (_dio1 != value)
				{
					_dio1 = value;
					NotifyOfPropertyChange(() => DIO1);
				}
			}
		}
		private bool _dio2;
		public bool DIO2
		{
			get { return _dio2; }
			set
			{
				if (_dio2 != value)
				{
					_dio2 = value;
					NotifyOfPropertyChange(() => DIO2);
				}
			}
		}
		private bool _dio3;
		public bool DIO3
		{
			get { return _dio3; }
			set
			{
				if (_dio3 != value)
				{
					_dio3 = value;
					NotifyOfPropertyChange(() => DIO3);
				}
			}
		}
		private bool _dio4;
		public bool DIO4
		{
			get { return _dio4; }
			set
			{
				if (_dio4 != value)
				{
					_dio4 = value;
					NotifyOfPropertyChange(() => DIO4);
				}
			}
		}
		private bool _dio5;
		public bool DIO5
		{
			get { return _dio5; }
			set
			{
				if (_dio5 != value)
				{
					_dio5 = value;
					NotifyOfPropertyChange(() => DIO5);
				}
			}
		}
		private bool _dio6;
		public bool DIO6
		{
			get { return _dio6; }
			set
			{
				if (_dio6 != value)
				{
					_dio6 = value;
					NotifyOfPropertyChange(() => DIO6);
				}
			}
		}
		private bool _dio7;
		public bool DIO7
		{
			get { return _dio7; }
			set
			{
				if (_dio7 != value)
				{
					_dio7 = value;
					NotifyOfPropertyChange(() => DIO7);
				}
			}
		}
		private bool _dio8;
		public bool DIO8
		{
			get { return _dio8; }
			set
			{
				if (_dio8 != value)
				{
					_dio8 = value;
					NotifyOfPropertyChange(() => DIO8);
				}
			}
		}
		private bool _dio9;
		public bool DIO9
		{
			get { return _dio9; }
			set
			{
				if (_dio9 != value)
				{
					_dio9 = value;
					NotifyOfPropertyChange(() => DIO9);
				}
			}
		}
		private bool _dio10;
		public bool DIO10
		{
			get { return _dio10; }
			set
			{
				if (_dio10 != value)
				{
					_dio10 = value;
					NotifyOfPropertyChange(() => DIO10);
				}
			}
		}
		private bool _dio11;
		public bool DIO11
		{
			get { return _dio11; }
			set
			{
				if (_dio11 != value)
				{
					_dio11 = value;
					NotifyOfPropertyChange(() => DIO11);
				}
			}
		}
		private bool _dio12;
		public bool DIO12
		{
			get { return _dio12; }
			set
			{
				if (_dio12 != value)
				{
					_dio12 = value;
					NotifyOfPropertyChange(() => DIO12);
				}
			}
		}
		private bool _dio13;
		public bool DIO13
		{
			get { return _dio13; }
			set
			{
				if (_dio13 != value)
				{
					_dio13 = value;
					NotifyOfPropertyChange(() => DIO13);
				}
			}
		}
        private bool _onBoardLed;
        public bool OnBoardLed
        {
            get { return _onBoardLed; }
            set
            {
                if (_onBoardLed != value)
                {
                    _onBoardLed = value;
                    NotifyOfPropertyChange(() => OnBoardLed);
                }
            }
        }
        public void OnBoardSwitch()
        {
            _switchState = !_switchState;
            if (_switchState)
                Switch1ButtonText = "Switch LED Off";
            else
                Switch1ButtonText = "Switch LED On";
            _eventAggregator.Publish<EmulatorGpioEvent>(new EmulatorGpioEvent(Pins.ONBOARD_SW1,_switchState));
        }

		#region IHandle<GpioEvent> Members

		public void Handle(NetduinoGpioEvent message)
		{
			if (message != null)
			{
				Console.WriteLine("Pin {0}", message.Pin);
				if (message.Pin==Pins.GPIO_PIN_D0)
					DIO0 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D1)
					DIO1 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D2)
					DIO2 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D3)
					DIO3 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D4)
					DIO4 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D5)
					DIO5 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D6)
					DIO6 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D7)
					DIO7 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D8)
					DIO8 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D9)
					DIO9 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D10)
					DIO10 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D11)
					DIO11 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D12)
					DIO12 = message.Edge;
                if (message.Pin == Pins.GPIO_PIN_D13)
					DIO13 = message.Edge;
                if (message.Pin == Pins.ONBOARD_LED)
                    OnBoardLed = message.Edge;
            }
		}

		#endregion

	}
}
