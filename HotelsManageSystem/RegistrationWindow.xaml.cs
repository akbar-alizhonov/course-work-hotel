using HotelsManageSystem.Models;
using HotelsManageSystem.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelsManageSystem
{
    public partial class RegistrationWindow : Window
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
        public ICommand SaveCommand { get; }
        public RegistrationWindow(User user, List<User> users)
        {
            InitializeComponent();

            User = new User
            {
                Username = user.Username,
                Password = user.Password
            };

            Users = users;

            SaveCommand = new RelayCommand(Save);

            DataContext = this;
        }
        private void Save(object parameter)
        {
            var users = Users.Where(u => u.Username == User.Username).ToList();
            if (users.Count() == 0)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Username уже занят");
            }
        }
    }
}
