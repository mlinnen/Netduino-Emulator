using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.IO;

namespace Netduino.Core.ViewModels
{
    /// <summary>
    /// The implementation of the shell
    /// </summary>
    [Export(typeof(IShellViewModel))]
    public class ShellViewModel :Conductor<Screen>,IShellViewModel
    {
        private string _emulatorName;
        private string _keyBase = @"HKEY_CURRENT_USER\Software\Microsoft\.NETMicroFramework\v4.1\Emulators\{45D406A2-51DD-4662-ABDD-499BD9589AF1}";
        private IWindowManager _windowManager;
        private IEmulatorViewModel _emulatorViewModel;

        [ImportingConstructor]
        public ShellViewModel(IWindowManager windowManager,IEmulatorViewModel viewModel)
        {
            _windowManager = windowManager;
            _emulatorViewModel = viewModel;
            this.DisplayName = "Netduino Emulator";
            
        }

        public string EmulatorName
        {
            get { return _emulatorName; }
            set 
            {
                if (_emulatorName != value)
                { 
                    _emulatorName = value;
                    NotifyOfPropertyChange(() => EmulatorName);
                    NotifyOfPropertyChange(() => CanWriteRegistry);
                }
            }
        }

        public IEmulatorViewModel EmulatorViewModel
        {
            get { return _emulatorViewModel; }
            set
            {
                if (_emulatorViewModel != value)
                {
                    _emulatorViewModel = value;
                }
            }
        }

        public bool CanWriteRegistry
        {
            get
            {
                return !string.IsNullOrEmpty(EmulatorName);
            }
            set { }
        }

        public void WriteRegistry()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Netduino.Shell.exe"); ;

            Microsoft.Win32.Registry.SetValue(_keyBase, "Name", EmulatorName);
            Microsoft.Win32.Registry.SetValue(_keyBase, "Path", path);

        }

        protected override void OnActivate()
        {
            base.OnActivate();
            Screen view = (Screen)IoC.Get<IEmulatorViewModel>();
            ChangeActiveItem(view, true);
        }
    }
}
