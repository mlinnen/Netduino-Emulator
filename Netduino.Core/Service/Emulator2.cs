using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SPOT.Emulator;
using System.Threading;
using Microsoft.SPOT.Emulator.Gpio;
using Microsoft.SPOT.Hardware;

namespace Netduino.Core.Services
{
    /// <summary>Extension of standard Emulator to provide events for key state changes</summary>
    /// <remarks>The standard <see cref="emulator" /> and derived classes go through
    /// several stages of initialization. However, external classes, like a view model
    /// don't get to participate in the "notification" of state changes. Thus it is difficult
    /// for a view model to know when it is safe to attach to the emulators LCD changed event.
    /// <para>This class is designed to add events to the standard emulator for the Initialize
    /// and Uninitialize states and to simplify registration of GPIO pins and LCD display components</para>
    /// </remarks>
    public class Emulator2 : Emulator
    {
        /// <summary>Creates a new instance of an <see cref="Emulator2"/></summary>
        public Emulator2( )
        {
        }
 
        /// <summary>Creates a new instance of an <see cref="Emulator2"/></summary>
        /// <param name="Hal">Custom emulator HAL built from the .NET Micro Framework Porting kit</param>
        /// <remarks>
        /// This overload of the constructor allows setting the HAL/CLR implementation for the emulator.
        /// You can create custom HAL/CLR configurations using the .NET Micro Framework Porting kit.
        /// </remarks>
		//public Emulator2(IEmulator Hal)
		//    : base( Hal )
		//{
		//}
 
        #region Emulator Thread
        /// <summary>Start the emulator engine on a new thread</summary>
        /// <remarks>Uses Environment.GetCommandLineArguments()</remarks>
        public void StartEmulator( )
        {
            new Thread( Start ).Start( );
        }
 
        /// <summary>Start the emulator engine on a new thread</summary>
        /// <param name="Args">Command line arguments</param>
        public void StartEmulator( string[ ] Args )
        {
            new Thread( Start ).Start( Args );
        }
        #endregion
 
        #region Events
        /// <summary>Event raised when the emulator has reached the Initialize state</summary>
        /// <remarks>
        /// This event is raised after the emulator is initialized but before it begins executing
        /// code. All of the EmulatorComponentCollections are fully wired up at this point. This
        /// event is normally used to trigger application code to attach to events fired by the
        /// other emulator components, such as the LCD or a GPIO pin.
        /// </remarks>
        public event EventHandler Initialize = delegate { };
 
        /// <summary>Event raised when the emulator is shutting down</summary>
        /// <remarks>
        /// This event indicates the emulator is shutting down and receivers should
        /// detach events and handle any other cleanup work needed.
        /// </remarks>
        public event EventHandler Uninitialize = delegate { };
 
        /// <summary>Triggers the <see cref="Initialize"/> event</summary>
        public override void InitializeComponent( )
        {
            base.InitializeComponent( );
            Initialize(this, null);
        }
 
        /// <summary>Triggers the <see cref="Uninitialize"/> event</summary>
        public override void UninitializeComponent()
        {
            base.UninitializeComponent( );
            Uninitialize( this, null );
        }
        #endregion

		#region GPIO Pins
		/// <summary>Registers a GPIO pin in the system</summary>
		/// <param name="Pin">Pin number</param>
		/// <param name="Name">Name for the pin</param>
		/// <remarks>
		/// <para>Useful method for registering a pin</para>
		/// </para>NOTE: This should only be called from an overload of the
		/// <see cref="LoadDefaultComponents()"/> Method</para>
		/// </remarks>
		protected GpioPort RegisterPin(string Name, Cpu.Pin Pin)
		{
			return RegisterPin(Name, Pin, GpioPortMode.InputOutputPort, GpioPortMode.InputOutputPort);
		}

		/// <summary>Registers a GPIO pin in the system</summary>
		/// <param name="Pin">Pin number</param>
		/// <param name="Name">Name for the pin</param>
		/// <param name="Key">Virtual Key to attach to the pin</param>
		/// <remarks>
		/// <para>Useful method for registering a pin that acts as a button</para>
		/// </para>NOTE: This should only be called from an overload of the
		/// <see cref="LoadDefaultComponents()"/> Method</para>
		/// </remarks>
		protected GpioPort RegisterPin(string Name, Cpu.Pin Pin, VirtualKey Key)
		{
			GpioPort retVal = RegisterPin(Name, Pin);
			retVal.VirtualKey = Key;
			return retVal;
		}

		/// <summary>Registers a GPIO pin in the system</summary>
		/// <param name="Name">Name for the pin</param>
		/// <param name="Pin">Pin number</param>
		/// <param name="ExpectedMode">Expected mode for the pin</param>
		/// <param name="AllowedMode">Allowed modes for the pin</param>
		/// <remarks>
		/// <para>Useful method for registering a pin</para>
		/// </para>NOTE: This should only be called from an overload of the
		/// <see cref="LoadDefaultComponents()"/> Method</para>
		/// </remarks>
		protected GpioPort RegisterPin(string Name, Cpu.Pin Pin, GpioPortMode ExpectedMode, GpioPortMode AllowedMode)
		{
			if (this.State != EmulatorState.Configuration)
				throw new InvalidOperationException(
							 "Cannot register GPIO Pins unless emulator is in the Configuration state");

			GpioPort port = new GpioPort();
			port.Pin = Pin;
			port.ModesAllowed = AllowedMode;
			port.ModesExpected = ExpectedMode;
			port.ComponentId = Name;
			RegisterComponent(port);
			return port;
		}
		#endregion
    }
}
