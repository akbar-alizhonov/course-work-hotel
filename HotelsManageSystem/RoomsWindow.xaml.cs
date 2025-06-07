using HotelsManageSystem.Models;
using HotelsManageSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace HotelsManageSystem
{
    public partial class RoomsWindow : Window
    {
        public ObservableCollection<Room> filterRooms { get; set; }
        public Hotel SelectedHotel { get; set; }

        public RoomsWindow(Hotel hotel, ObservableCollection<Room> allRooms)
        {
            InitializeComponent();
            SelectedHotel = hotel;
            filterRooms = new ObservableCollection<Room>(
                allRooms.Where(r => r.HotelId == hotel.Id).ToList()
            );
            DataContext = this;
        }
    }
}
