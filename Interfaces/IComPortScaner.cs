using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestApp.Interfaces
{
    public interface IComPortScaner
    {
        List<ComPortData> GetComPorts();
    }
}
