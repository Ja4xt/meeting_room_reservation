using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConcertReservationSystem
{
    [XmlRoot("ArrayOfRoom")]
    public class ArrayOfRoom
    {
        [XmlElement("Room")]
        public List<Room> rooms = new List<Room>();
    }
}
