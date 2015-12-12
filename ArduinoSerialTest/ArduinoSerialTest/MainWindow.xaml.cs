using System.Windows;
using System.Windows.Input;

namespace ArduinoSerialTest
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private SerialComm port;

        public MainWindow()
        {
            InitializeComponent();
            input.Text = "Hello I'm the input";
            output.Text = "And I'm the output\n";
            input.KeyDown += Input_KeyDown;

            port = new SerialComm(output);

        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.Key == Key.Return)
            {
                string testo = input.Text;
                input.Text = "";
                port.Write(testo);
            }
        }
    }
}
