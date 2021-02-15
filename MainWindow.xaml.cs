using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnitTestApp.Hardware;
using UnitTestApp.Interfaces;

namespace UnitTestApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<ComPortData> ports;

        private ComPortsSelectionDataContext ComPortsSelection;

        private string[] generatePortsNames()
        {
            string[] result = new string[ports.Count];

            int i = 0;
            foreach (var port in ports)
            {
                result[i] = port.port.PortName;
                if (port.description != null)
                {
                    result[i] += " - " + port.description;
                }

                i++;
            }

            return result;
        }

        public MainWindow()
        {
            InitializeComponent();

            IComPortScaner scaner = new ComPortScaner();
            ports = scaner.GetComPorts();

            ComPortsSelection = new ComPortsSelectionDataContext
            {
                ListOfAvaliablePorts
                = new ObservableCollection<string>(generatePortsNames()),
                SelectedPortIndex = -1
            };


            this.DataContext = ComPortsSelection;
        }

        private void port_select_btn_Click(object sender, RoutedEventArgs e)
        {
            var index = ComPortsSelection.SelectedPortIndex;

            if (index < 0)
            {
                MessageBox.Show("Select a COM port first!");
                return;
            }

            var t = new Thread(checkArduino);
            t.Start(ports[index]);
        }

        private static void checkArduino(object portObj)
        {
            var comPort = (ComPortData)portObj;
            var port = comPort.port;

            port.DataBits = 8;
            port.StopBits = StopBits.One;
            port.Handshake = Handshake.None;
            port.Parity = Parity.None;
            port.BaudRate = 9600;

            MessageBox.Show("Opening port...");

            port.Open();
            Thread.Sleep(500);

            string returnMessage = port.ReadLine();

            port.Close();

            MessageBox.Show("Result: " + returnMessage);

            //int c;

            //int iter = 0;
            //while ((c = port.ReadByte()) > 0)
            //{
            //    if (iter > 10)
            //        break;

            //    Console.WriteLine("Symbol read: " + (char)c);
            //    iter++;
            //}

        }
    }
}
