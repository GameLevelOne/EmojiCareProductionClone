using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YouAPPiSDK.Android
{
   
    public interface IUIExecutor
    {
        void runOnUI(Action func);
    }
}
