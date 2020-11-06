using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPCPrototypeRest.Model
{
    public class Datastructures
    {
        //Used for booking status
        public enum Status
        {
            PendingAccept = 0,
            Open = 1,
            PendingClosing = 2,
            Closed = 3
        };

        public enum TableName
        {
            Booking,
            Car,
            CarBooking,
            Customer,
            Truckdriver
        };

    }
}
