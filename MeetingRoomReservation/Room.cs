using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConcertReservationSystem 
{
    public class Room : INotifyPropertyChanged
    {
        public int roomNumber { get; set; }
        public string roomCustomerName { get; set; }
        public bool isReserved { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
