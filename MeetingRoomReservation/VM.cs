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
        public void WriteXmlSerializer(Room[] rooms)
        {
            List<Room> roomList = rooms.ToList();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Room>));
            TextWriter writer = new StreamWriter("test.xml");
            serializer.Serialize(writer, roomList);
            writer.Close();
        }
        public ArrayOfRoom ReadFromXml()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(ArrayOfRoom));
            TextReader reader = new StreamReader("test.xml");
            object obj = deserializer.Deserialize(reader);
            ArrayOfRoom XmlData = (ArrayOfRoom)obj;
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
