using HotelsManageSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HotelsManageSystem.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private User _currentUser;
        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
                LoadUserHotels();
            }
        }
        public ObservableCollection<Room> _rooms;
        public ObservableCollection<Room> Rooms
        {
            get => _rooms;
            set
            {
                _rooms = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Hotel> _hotels;
        public ObservableCollection<Hotel> Hotels
        {
            get => _hotels;
            set
            {
                _hotels = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddHotelCommand { get; }
        public ICommand ViewHotelRoomsCommand { get; }
        public ICommand AddRoomCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand LoginCommand { get; }
        public MainViewModel(User user = null)
        {
            CurrentUser = user;
            LoadData();
            if (CurrentUser != null)
            {
                LoadUserHotels();
            }
            AddHotelCommand = new RelayCommand(AddHotel);
            ViewHotelRoomsCommand = new RelayCommand(ViewHotelRooms);
            AddRoomCommand = new RelayCommand(AddRoom);
            RegisterCommand = new RelayCommand(ViewRegistration);
            LoginCommand = new RelayCommand(ViewLogin);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void LoadUserHotels()
        {
            if (CurrentUser != null)
            {
                using var context = new AppDbContext();
                var userHotels = context.Hotels
                    .Include(h => h.User)
                    .Include(h => h.Rooms)
                    .Where(h => h.UserId == CurrentUser.Id)
                    .ToList();
                Rooms = new ObservableCollection<Room>(context.Rooms.Include(e => e.Hotel).ToList());
                Hotels = new ObservableCollection<Hotel>(userHotels);
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void LoadData()
        {
            using (var context = new AppDbContext())
            {
                Rooms = new ObservableCollection<Room>(context.Rooms.Include(e => e.Hotel).ToList());
                Hotels = new ObservableCollection<Hotel>(context.Hotels.Include(e => e.User).Include(h => h.Rooms).ToList());
                Users = new ObservableCollection<User>(context.Users.ToList());
            }
        }
        private void AddHotel(object parameter)
        {
            var dialog = new HotelEditWindow(new Hotel(), Users.ToList());
            if (dialog.ShowDialog() == true)
            {
                using (var context = new AppDbContext())
                {
                    context.Hotels.Add(dialog.Hotel);
                    context.SaveChanges();
                    LoadUserHotels();
                }
            }
        }
        private void AddRoom(object parameter)
        {
            if (parameter is Hotel hotel)
            {
                var dialog = new AddRoomDialog();
                if (dialog.ShowDialog() == true)
                {
                    using var context = new AppDbContext();
                    var room = new Room
                    {
                        Number = dialog.RoomNumber,
                        HotelId = hotel.Id
                    };

                    context.Rooms.Add(room);
                    context.SaveChanges();
                    LoadUserHotels();
                }
            }
        }
        private void ViewHotelRooms(object parameter)
        {
            if (parameter is Hotel hotel)
            {
                var roomsWindow = new RoomsWindow(hotel, Rooms);
                roomsWindow.Show();
            }
        }

        private void ViewRegistration(object parameter)
        {
            var registrationWindow = new RegistrationWindow(new User(), Users.ToList());
            if(registrationWindow.ShowDialog() == true)
            {
                using (var context = new AppDbContext())
                {
                    context.Users.Add(registrationWindow.User);
                    context.SaveChanges();
                    LoadData();
                }
            }
        }
        private void ViewLogin(object parameter)
        {
            var registrationWindow = new LoginWindow(new User(), Users.ToList());
            if (registrationWindow.ShowDialog() == true)
            {
                using (var context = new AppDbContext())
                {
                    CurrentUser = registrationWindow.User;
                    Hotels = new ObservableCollection<Hotel>(context.Hotels.Include(e => e.User).Include(h => h.Rooms).Where(u => u.UserId == CurrentUser.Id).ToList());
                    var mainWindow = new MainWindow(CurrentUser);
                    mainWindow.Show();
                    var startWindow = Application.Current.Windows
                        .OfType<StartWindow>()
                        .FirstOrDefault();
                        startWindow?.Close();
                }
            }
        }
    }
}
