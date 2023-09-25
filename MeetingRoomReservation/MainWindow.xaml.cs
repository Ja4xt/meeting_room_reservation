using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConcertReservationSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VM vm = new VM();
        private int totalReservedRooms; //to check if all rooms are reserved or not
        private Button[] buttons; //Array of buttons
        private bool reserveButtonClicked = false;
        public MainWindow()
        {
            InitializeComponent();

            buttons = new Button[]{btnRoom1,btnRoom2,btnRoom3,btnRoom4,};
        }

        private void roomSelector(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = Array.IndexOf(buttons, button);
            tbRoomNumber.Text = (index + 1).ToString();
        }


        private void reserveRoom(object sender, RoutedEventArgs e)
        {
            Room[] rooms = new Room[4];
            for (int i = 0; i < 4; i++)
            {
                rooms[i] = new Room();
                rooms[i].roomNumber = i + 1;
                rooms[i].roomCustomerName = "";
                rooms[i].isReserved = false;

            }
            if (reserveButtonClicked)
            {
                ArrayOfRoom room = vm.ReadFromXml();
                rooms = room.rooms.ToArray();
            }
            reserveButtonClicked = true;

            int roomNumber;
            //if all rooms are reserved messagebox will show message
            if (totalReservedRooms != 4)
            {
                //checking if room number and customer name is provided for reservation
                if (tbRoomNumber.Text != "" && tbCustomerName.Text != "")
                {
                    //if value of room number is not number than messagebox will show message
                    try
                    {
                        roomNumber = int.Parse(tbRoomNumber.Text);
                        rooms[roomNumber - 1].roomNumber = roomNumber;
                        rooms[roomNumber - 1].roomCustomerName = tbCustomerName.Text;
                        //if room is already reserved messagebox will show message
                        if (rooms[roomNumber - 1].isReserved)
                        {
                            MessageBox.Show($"Room {rooms[roomNumber - 1].roomNumber} is already reserved");
                        }
                        else
                        {
                            //buttons[roomNumber - 1].Content = rooms[roomNumber - 1].roomCustomerName;
                            //buttons[roomNumber - 1].Background = new SolidColorBrush(Color.FromRgb(66, 135, 245));
                            rooms[roomNumber - 1].isReserved = true;
                            totalReservedRooms++;
                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Enter room number in correct format");
                    }

                }
                else
                {
                    MessageBox.Show("Enter room number and Customer name for reservation");
                }
            }
            else
            {
                MessageBox.Show("All rooms are reserved");
            }
            vm.WriteXmlSerializer(rooms);
            readRoomArrangement();
        }
        public void readRoomArrangement()
        {
            ArrayOfRoom room = vm.ReadFromXml();
            Room[] rooms = room.rooms.ToArray();
            for (int i = 0; i < rooms.Length; i++)
            {
                if (rooms[i].isReserved)
                {
                    buttons[rooms[i].roomNumber - 1].Content = rooms[rooms[i].roomNumber - 1].roomCustomerName;
                    buttons[rooms[i].Number - 1].Background = new SolidColorBrush(Color.FromRgb(66, 135, 245));
                }
                else
                {
                    buttons[rooms[i].roomNumber - 1].Content = "unreserved";
                    buttons[rooms[i].Number - 1].Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

                }
            }

        }
        private void cancelReservation(object sender, RoutedEventArgs e)
        {
            ArrayOfRoom room = vm.ReadFromXml();
            Room[] rooms = room.rooms.ToArray();
            //if there is no rooms reserved messagebox will show message
            if (totalReservedRooms > 0)
            {
                //if room number is provided for canceling reservation
                if (tbRoomNumber.Text != "")
                {
                    //if value of room number is not number than messagebox will show message
                    try
                    {
                        int roomNumber = int.Parse(tbRoomNumber.Text);

                        for (int i = 0; i < rooms.Length; i++)
                        {
                            if (rooms[i].roomNumber == roomNumber)
                            {
                                //if the room is already empty messagebox will show message
                                if (rooms[i].isReserved)
                                {
                                    //buttons[i].Content = "unreserved";
                                    //buttons[i].Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                                    rooms[i].isReserved = false;
                                    rooms[i].roomCustomerName = "";
                                    totalReservedRooms--;
                                    tbCustomerName.Text = "";
                                    tbRoomNumber.Text = "";
                                }
                                else
                                {
                                    MessageBox.Show("Room is already empty");
                                }
                            }

                        }
                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("Enter room number in correct format");
                    }

                }
                else
                {
                    string name = tbCustomerName.Text;
                    bool multipleRooms = false; //to check if two rooms are reserved with the same name
                    bool isRoomExistWithName = false; //to check if there is any room is reserved with provided name
                                                      //checking for multiple rooms with same name
                    for (int i = 0; i < rooms.Length; i++)
                    {
                        for (int j = i + 1; j < rooms.Length; j++)
                        {
                            if (rooms[i].roomCustomerName == rooms[j].roomCustomerName)
                            {
                                if (rooms[i].roomCustomerName.ToLower() == name.ToLower())
                                {
                                    multipleRooms = true;

                                }
                            }
                        }
                    }
                    //Canceling the reservation using name
                    for (int i = 0; i < rooms.Length; i++)
                    {
                        if (rooms[i].roomCustomerName.ToLower() == name.ToLower())
                        {
                            if (multipleRooms)
                            {
                                MessageBox.Show("There are two rooms with same name. Enter room number");
                                isRoomExistWithName = true;
                                multipleRooms = false;
                                break;
                            }
                            else
                            {
                                buttons[i].Content = "unreserved";
                                buttons[i].Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                                rooms[i].isReserved = false;
                                rooms[i].roomCustomerName = "";
                                totalReservedRooms--;
                                isRoomExistWithName = true;
                                tbCustomerName.Text = "";
                                tbRoomNumber.Text = "";
                            }
                        }
                    }
                    if (!isRoomExistWithName)
                    {
                        MessageBox.Show($"No rooms are reserved with the name {name}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Every room is empty");

            }
            vm.WriteXmlSerializer(rooms);
            readRoomArrangement();
        }

        private void cancelAllReservations(object sender, RoutedEventArgs e)
        {
            ArrayOfRoom room = vm.ReadFromXml();
            Room[] rooms = room.rooms.ToArray();
            if (totalReservedRooms > 0)
            {
                for (int i = 0; i < rooms.Length; i++)
                {
                    if (rooms[i].isReserved)
                    {
                        buttons[i].Content = "unreserved";
                        buttons[i].Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                        rooms[i].isReserved = false;
                        rooms[i].roomCustomerName = "";
                        totalReservedRooms--;
                        tbCustomerName.Text = "";
                        tbRoomNumber.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Every room is empty");

            }
            vm.WriteXmlSerializer(rooms);
            readRoomArrangement();
        }

        private void linqAction(object sender, RoutedEventArgs e)
        {
            ArrayOfRoom room = vm.ReadFromXml();
            Room[] rooms = room.rooms.ToArray();
           
            //linqButtonNames 
            Button button = sender as Button;
            string[] buttons = { "btnLINQ1", "btnLINQ2", "btnLINQ3" };
            if(listBox.Items.Count != 0)
            {
                listBox.Items.Clear();
            }
            if (button.Name == buttons[0])
            {

                var RoomNames = rooms.Where(x => x.isReserved == true).Select(x => x.roomCustomerName).OrderByDescending(x => x).ToList();//from room in rooms where room.isReserved=true select room.roomCustomerName;
                for (int i = 0; i < RoomNames.Count; i++)
                {
                    listBox.Items.Add(RoomNames[i]);
                }
            }
            else if (button.Name== buttons[1])
            {
                var RoomNames = rooms.Where(x => x.isReserved == true).Select(x => x.roomCustomerName).OrderBy(x => x.Length).ToList();
                for (int i = 0; i < RoomNames.Count; i++)
                {
                    listBox.Items.Add(RoomNames[i]);
                }
            }
            else
            {
                var RoomNames = rooms.Where(x => x.isReserved == false).Select(x => x.roomNumber).OrderBy(x => x).ToList();
                for (int i = 0; i < RoomNames.Count; i++)
                {
                    listBox.Items.Add(RoomNames[i]);
                }
            }
            
        }
    }
}

