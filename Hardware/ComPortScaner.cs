using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UnitTestApp.Interfaces;

namespace UnitTestApp.Hardware
{
    public class ComPortScaner : IComPortScaner
    {
        private Dictionary<string, string> gatherDescriptions()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            var result = new Dictionary<string, string>();

            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();

                    // TODO: regexp with our board names
                    if (desc.Contains("Arduino"))
                    {
                        result.Add(deviceId, desc);
                    }
                }
            }
            catch (ManagementException e)
            {
                /* Do Nothing */
            }

            return result;
        }

        public List<ComPortData> GetComPorts()
        {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            var namesToDesc = gatherDescriptions();


            var portDatas = new List<ComPortData>();

            // Display each port name to the console.
            foreach (string port in ports)
            {
                var portObj = new SerialPort(port);
                string desc = null;

                if (namesToDesc.TryGetValue(port, out string val))
                {
                    desc = val;
                }

                portDatas.Add(new ComPortData
                {
                    port = portObj,
                    description = desc
                });
            }

            return portDatas;
        }
    }
}
