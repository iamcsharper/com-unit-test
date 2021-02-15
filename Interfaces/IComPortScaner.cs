using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestApp.Interfaces
{
    interface IComPortScaner
    {
        List<ComPortData> GetComPorts();
    }
}
