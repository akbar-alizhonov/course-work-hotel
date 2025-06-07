using HotelsManageSystem.Models;
using HotelsManageSystem.ViewModels;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelsManageSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand ViewHotelCommand { get; }
        public User User { get; set; }
        public MainWindow(User user)
        {
            InitializeComponent();
            var viewModel = new MainViewModel(user);
            DataContext = viewModel;
        }
    }
}