using HotelsManageSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelsManageSystem
{
    public partial class LoginWindow : Window
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
        public ICommand SaveCommand { get; }
        public LoginWindow(User user, List<User> users)
        {
            InitializeComponent();

            User = user; 
            Users = users;

            SaveCommand = new RelayCommand(Save);
            DataContext = this;
        }
        private void Save(object parameter)
        {
            var userLogin = Users
        .FirstOrDefault(u => u.Username == User.Username && u.Password == User.Password);
            if (userLogin != null)
            {
                User = userLogin;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль!");
            }
        }
    }
}
