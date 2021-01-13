using ControlsXL;
using Integra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraXL.Windows
{
    /// <summary>
    /// Provides a base class for windows have INTEGRA-7 dependencies.
    /// </summary>
    public class IntegraWindow : CommonWindow 
    { 
        /// <summary>
        /// Gets the device context.
        /// </summary>
        public Device DeviceContext
        {
            get { return ApplicationContext.Integra; }
        }
    }
}
