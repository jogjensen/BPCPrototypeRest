using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BPCPrototypeRest.Model;

namespace BPCPrototypeRest.Managers
{
    public class BookingManager
    {
        private const string connString = "Server=tcp:bpcserver.database.windows.net,1433;Initial Catalog=BPCPrototypeDB;Persist Security Info=False;User ID=bpcadm;Password=Philipersej123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


        public IList<Bookings> GetAllBookings()
        {
            List<Bookings> bookingList = new List<Bookings>();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("Select * from Booking", conn))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        bookingList.Add(ReadNextBooking(reader));
                    }
                }

                return bookingList;
            }


        }

        private Bookings ReadNextBooking(SqlDataReader reader)
        {
            Bookings booking = new Bookings();

            booking.OrdNr = reader.GetInt32(0);
            booking.StartAdr = reader.GetString(1);
            booking.StartTime = reader.GetString(2);
            booking.StartDate = reader.GetString(3);
            booking.EndAdr = reader.GetString(4);
            booking.EndTime = reader.GetString(5);
            booking.EndDate = reader.GetString(6);
            booking.NumberOcn = reader.GetString(7);
            booking.TypeOfGoods = reader.GetString(8);
            booking.Comments = reader.GetString(9);

            return booking;
        }
    }
}

