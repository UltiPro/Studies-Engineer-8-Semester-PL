using SPA.ApplicationModes;
using SPA.ApplicationModes.Types;

namespace SPA
{
    public sealed class SPAApp
    {
        private static SPAApp _app;

        private bool _debugInTests;

        private SPAApp(bool debugInTests = false) => _debugInTests = debugInTests;
      
        public static SPAApp InitApp(bool debugInTests)
        {
            if(_app is null)
            {
                _app = new SPAApp(debugInTests);
                return _app;
            }
            return _app;
        }

        public void Run(ApplicationRunModes runMode = ApplicationRunModes.Default, string[] args = null)
        {
            //string[] args = Environment.GetCommandLineArgs();
            if(runMode == ApplicationRunModes.Test)
            {
                SPATestMode.Run(_debugInTests);
                return;
            }
            else if(runMode == ApplicationRunModes.Basic)
            {
                SPABasicMode.Run(args);
                return;
            }
            SPABasicMode.Run(args);
        }
    }
}
