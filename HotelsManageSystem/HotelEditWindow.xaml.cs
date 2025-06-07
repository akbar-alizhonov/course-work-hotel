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
    public partial class HotelEditWindow : Window
    {
        public Hotel Hotel { get; set; }
        public List<User> Users { get; set; }

        public ICommand SaveCommand { get; }

        internal HotelEditWindow(Hotel hotel, List<User> users)
        {
            InitializeComponent();

            Hotel = new Hotel
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Stars = hotel.Stars,
                PhoneNumber = hotel.PhoneNumber,
                Address = hotel.Address,
                UserId = hotel.UserId
            };

            Users = users;

            SaveCommand = new RelayCommand(_ =>
            {
                DialogResult = true;
                Close();
            });

            DataContext = this;
        }
    }
}
