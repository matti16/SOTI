using Caliburn.Micro;
using SOTI.Message;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOTI.ViewModels
{
    /// <summary>
    /// Class that manages the cmmunication between the application and Arduino.
    /// </summary>
    class SerialCommunication
    {   
        
        const string GREEN = "1";
        const string RED = "2";
        const string BLUE = "3";
        const string OFF = "0";

        private SerialPort port;

        private readonly IEventAggregator eventAggregator;

        public SerialCommunication(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            port = new SerialPort();
            port.BaudRate = 9600;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.PortName = SerialPort.GetPortNames()[0]; //First port with a device

            port.Open();

        }

        /// <summary>
        /// This method is used to handle the incoming data.
        /// It has to recognize the pressed button and send out the information.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            switch (inData)
            {
                case GREEN:
                    this.eventAggregator.PublishOnUIThread(new GreenButtonMessage());
                    break;

                case RED:
                    this.eventAggregator.PublishOnUIThread(new RedButtonMessage());
                    break;

                case BLUE:
                    this.eventAggregator.PublishOnUIThread(new BlueButtonMessage());
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// Turn on the requested leds.
        /// </summary>
        /// <param name="green"></param>
        /// <param name="red"></param>
        /// <param name="blue"></param>
        /// <returns>Return 0</returns>
        public int LedsOn(bool green, bool red, bool blue)
        {
            if (green){
                port.Write(GREEN); }
            if (red){
                port.Write(RED); }
            if (blue){
                port.Write(BLUE); }
            
            return 0;
        }

        /// <summary>
        /// Turn off all the leds.
        /// </summary>
        /// <returns>Return 0</returns>
        public int LeadsOff()
        {
            port.Write(OFF);
            return 0;
        }
    }
}
