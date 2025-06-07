using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelsManageSystem
{
    public partial class AddRoomDialog : Window
    {
        public int RoomNumber { get; set; }
        public ICommand SaveCommand { get; }

        public AddRoomDialog()
        {
            InitializeComponent();
            SaveCommand = new RelayCommand(Save);
            DataContext = this;
        }

        private void Save(object parameter)
        {
            if (RoomNumber > 0)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Введите положительный номер комнаты");
            }
        }
    }
}
