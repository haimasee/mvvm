using MVVM_4.ModelView;
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

namespace MVVM_4
{
    /// <summary>
    /// Логика взаимодействия для new_type.xaml
    /// </summary>
    public partial class new_type : Window
    {
        public new_type()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
        private void Closed(object sender, RoutedEventArgs e)
        {
            Close();
            new MainWindow().Show();
        }
    }
}
