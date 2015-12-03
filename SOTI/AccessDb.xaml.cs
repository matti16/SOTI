using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SOTI.Model;

namespace SOTI
{
    /// <summary>
    /// Logica di interazione per AccessDb.xaml
    /// </summary>
    public partial class AccessDb : Window
    {
        public AccessDb()
        {
            InitializeComponent();
            DataLayer data = new DataLayer();
            string prova = "";
            foreach (var item in data.ricette)
            {
                prova += item.ToString() + '\n';
            }
            textDB.Text = prova;
        }
    }
}
