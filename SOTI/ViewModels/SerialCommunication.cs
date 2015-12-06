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


        public SerialCommunication()
        {
            port = new SerialPort();
            port.BaudRate = 9600;
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

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
                    break;
                case RED:
                    break;
                case BLUE:
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
            string leds = "";

            if (green){
                leds += GREEN; }
            if (red){
                leds += RED; }
            if (blue){
                leds += BLUE; }

            port.Write(leds);

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
