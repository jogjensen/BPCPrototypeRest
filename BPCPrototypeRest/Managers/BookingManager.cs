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
        #region Connection string
        private const string connString = "Server=tcp:bpcserver.database.windows.net,1433;Initial Catalog=bpcdb;Persist Security Info=False;User ID=bpcadm;Password=Philipersej123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        #endregion
        
        #region GetAllBookings()
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
        #endregion

        #region ReadNextBooking()
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
        #endregion

        #region GetBookingFromOrdNr()

        public Bookings GetBookingFromOdrNr(int bookingOrdnr)
        {
            Bookings bookings = new Bookings();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("Select * from booking where OrdNr = @OrdNr",conn))
                {
                    command.Parameters.AddWithValue("@OrdNr", bookingOrdnr);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        bookings = ReadNextBooking(reader);
                    }
                }

                return bookings;
            }
        }


        #endregion

        #region CreateBooking()

        public bool CreateBooking(Bookings bookings)
        {
            bool created = false;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("Insert into Booking (StartAdr, StartTime, StartDate, EndAdr, EndTime, EndDate, NumberOcn, TypeOfGoods, Comments)"
                                                           + "values(@StartAdr, @StartTime, @StartDate, @EndAdr, @EndTime, @EndDate, @NumberOcn, @TypeOfGoods, @Comments)", conn ))
                {
                    command.Parameters.AddWithValue("@StartAdr", bookings.StartAdr);
                    command.Parameters.AddWithValue("@StartTime", bookings.StartTime);
                    command.Parameters.AddWithValue("@StartDate", bookings.StartDate);
                    command.Parameters.AddWithValue("@EndAdr", bookings.EndAdr);
                    command.Parameters.AddWithValue("@EndTime", bookings.EndTime);
                    command.Parameters.AddWithValue("@EndDate", bookings.EndDate);
                    command.Parameters.AddWithValue("@NumberOcn", bookings.NumberOcn);
                    command.Parameters.AddWithValue("@TypeOfGoods", bookings.TypeOfGoods);
                    command.Parameters.AddWithValue("@Comments", bookings.Comments);

                    int rows = command.ExecuteNonQuery();
                    created = rows == 1;
                }
            }

            return created;
        }

        #endregion
    }
}

