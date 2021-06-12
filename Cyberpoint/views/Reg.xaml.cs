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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cyberpoint.Views
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : UserControl
    {
        public Button b1;

        public bool LogIsClicked = false;
    
        public Reg()
        {
            InitializeComponent();
        }

        public bool GetB()
        {
            return LogIsClicked;
        }

        private void B1_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Window.GetWindow((DependencyObject)sender);

            mainWindow.U1();

            LogIsClicked = true;
            Values.b1 = true;
            Console.WriteLine("Changed");
        }
    }
}
