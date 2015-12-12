using System;
using System.IO.Ports;
using System.Windows.Controls;

namespace ArduinoSerialTest
{
    /// <summary>
    /// Class that manages the cmmunication between the application and Arduino.
    /// </summary>
    class SerialComm
    {

        private SerialPort port;
        private TextBlock txt;

        public SerialComm(TextBlock txt)
        {
            this.txt = txt;
            port = new SerialPort();
            port.BaudRate = 9600;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.PortName = SerialPort.GetPortNames()[0];
            port.Open();

        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            Console.Write(inData);
        }

        public int Write(string text)
        {
            port.Write(text);
            return 0;
        }
    }
    
}