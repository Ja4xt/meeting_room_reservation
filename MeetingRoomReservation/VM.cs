using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConcertReservationSystem
{
    
    internal class VM : INotifyPropertyChanged
    {
        #region properties



        #endregion
        #region methods
        public void WriteXmlSerializer(Room[] seats)
        {
            List<Room> seatList = seats.ToList();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
            TextWriter writer = new StreamWriter("test.xml");
            serializer.Serialize(writer, seatList);
            writer.Close();
        }
        public ArrayOfSeat ReadFromXml()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ArrayOfSeat));
            TextReader reader = new StreamReader("test.xml");
            object obj = deserializer.Deserialize(reader);
            ArrayOfSeat XmlData = (ArrayOfSeat)obj;
            reader.Close();
            return XmlData;
        }
        #endregion
        #region propChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void propChanged([CallerMemberName] string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        

        #endregion
    }
}
