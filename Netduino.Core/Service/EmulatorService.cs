using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SPOT.Emulator;
using Microsoft.SPOT.Emulator.Gpio;
using Microsoft.SPOT.Hardware;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using Netduino.Core;

namespace Netduino.Core.Services
{
	[Export(typeof(IEmulatorService))]
	public class EmulatorService:Emulator2,IEmulatorService,IHandle<InputGpioEventArgs>,IHandle<OutputGpioEventArgs>
	{
		//private readonly Emulator _emulator;
		private GpioPort _onBoardLedPort;
		private GpioPort _onBoardSwitch1;
		private GpioPort _gpio_d0Port;
		private GpioPort _gpio_d1Port;
		private GpioPort _gpio_d2Port;
		private GpioPort _gpio_d3Port;
		private GpioPort _gpio_d4Port;
		private GpioPort _gpio_d5Port;
		private GpioPort _gpio_d6Port;
		private GpioPort _gpio_d7Port;
		private GpioPort _gpio_d8Port;
		private GpioPort _gpio_d9Port;
		private GpioPort _gpio_d10Port;
		private GpioPort _gpio_d11Port;
		private GpioPort _gpio_d12Port;
		private GpioPort _gpio_d13Port;
		//private GpioPort _gpio_a0Port;
		private readonly IEventAggregator _eventAggregator;
        private readonly ILog _log = LogManager.GetLog(typeof(EmulatorService));


		public EmulatorService(IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;
		}

        public override void Configure(System.Xml.XmlReader reader)
        {
            try
            {
                base.Configure(reader);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }
        }

        public override void Run()
        {
            base.Run();

        }
        
		/// <summary>Registers default components and settings for this emulator</summary>
		protected override void LoadDefaultComponents()
		{
			base.LoadDefaultComponents();

		}

		public override void SetupComponent()
		{
			base.SetupComponent();
		}

		public override void InitializeComponent()
		{
			base.InitializeComponent();
            _gpio_d0Port = RegisterOutput("GPIO_PIN_D0");
            _gpio_d1Port = RegisterOutput("GPIO_PIN_D1");
            _gpio_d2Port = RegisterOutput("GPIO_PIN_D2");
            _gpio_d3Port = RegisterOutput("GPIO_PIN_D3");
            _gpio_d4Port = RegisterOutput("GPIO_PIN_D4");
            _gpio_d5Port = RegisterOutput("GPIO_PIN_D5");
            _gpio_d6Port = RegisterOutput("GPIO_PIN_D6");
            _gpio_d7Port = RegisterOutput("GPIO_PIN_D7");
            _gpio_d8Port = RegisterOutput("GPIO_PIN_D8");
            _gpio_d9Port = RegisterOutput("GPIO_PIN_D9");
            _gpio_d10Port = RegisterOutput("GPIO_PIN_D10");
            _gpio_d11Port = RegisterOutput("GPIO_PIN_D11");
            _gpio_d12Port = RegisterOutput("GPIO_PIN_D12");
            _gpio_d13Port = RegisterOutput("GPIO_PIN_D13");
            _onBoardLedPort = RegisterOutput("ONBOARD_LED");
            _onBoardSwitch1 = this.FindComponentById("ONBOARD_SW1") as GpioPort;

            _eventAggregator.Subscribe(this);
		}

        private GpioPort RegisterOutput(string id)
        {
            GpioPort port = this.FindComponentById(id) as GpioPort;
            if (port != null)
            {
                if (port.ModesAllowed == GpioPortMode.InputOutputPort || port.ModesAllowed == GpioPortMode.OutputPort)
                {
                    _log.Info("Id={0} is an output pin so register it for events", id);
                    port.OnGpioActivity += new GpioActivity(Port_OnGpioActivity);
                }
                else
                {
                    _log.Warn("Id={0} must not be an output pin", id);
                }
            }
            else
            {
                _log.Warn("Output was not registered Id={0}", id);
            }
            return port;
        }

		public override void UninitializeComponent()
		{
			base.UninitializeComponent();
		}

		void Port_OnGpioActivity(GpioPort sender, bool edge)
		{
            _log.Info("Emulator Service GpioPort Fired {0} {1}",sender.Pin,edge);
            _eventAggregator.Publish<OutputGpioEventArgs>(new OutputGpioEventArgs((int)sender.Pin, edge));
		}

        public void Handle(InputGpioEventArgs message)
        {
            
            if (message != null)
            {
                _log.Info("Emulator Service: Handle Input {0} {1}",message.Pin,message.Edge);
                if (message.Pin == Pins.ONBOARD_SW1 && _onBoardSwitch1 != null)
                {
                    _onBoardSwitch1.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D1 && _gpio_d1Port != null)
                {
                    _gpio_d1Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D2 && _gpio_d2Port != null)
                {
                    _gpio_d2Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D3 && _gpio_d3Port != null)
                {
                    _gpio_d3Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D4 && _gpio_d4Port != null)
                {
                    _gpio_d4Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D5 && _gpio_d5Port != null)
                {
                    _gpio_d5Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D6 && _gpio_d6Port != null)
                {
                    _gpio_d6Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D7 && _gpio_d7Port != null)
                {
                    _gpio_d7Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D8 && _gpio_d8Port != null)
                {
                    _gpio_d8Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D9 && _gpio_d9Port != null)
                {
                    _gpio_d9Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D10 && _gpio_d10Port != null)
                {
                    _gpio_d10Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D11 && _gpio_d11Port != null)
                {
                    _gpio_d11Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D12 && _gpio_d12Port != null)
                {
                    _gpio_d12Port.Write(message.Edge);
                }
                if (message.Pin == Pins.GPIO_PIN_D13 && _gpio_d13Port != null)
                {
                    _gpio_d13Port.Write(message.Edge);
                }
            }
        }

        public void Handle(OutputGpioEventArgs message)
        {
            _log.Info("Emulator Service Handle Output ");

        }
    }
}
