using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestApp.Interfaces
{
    interface IComPortSender
    {
        void SendBytes(ComPortData comPort, byte[] bytes);
    }
}
