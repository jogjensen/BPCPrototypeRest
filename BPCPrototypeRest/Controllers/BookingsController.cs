using System.Collections.Generic;
using BPCPrototypeRest.Managers;
using BPCPrototypeRest.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BPCPrototypeRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {


        #region Temporary static list
        private static int idCount = 1;
        BookingManager manager = new BookingManager();

        private static readonly List<Bookings> bookingsList = new List<Bookings>()
        {
            new Bookings(idCount++, "Slagelse", "10:00", "12/12/2020", "Roskilde", "12:00", "13/12/2020", "2", "Gravko",
                "Dyret står til højre for siloen"),
            new Bookings(idCount++, "Odense", "12:00", "14/12/2020", "Kastrup", "17:00", "14/12/2020","3" , "Skur",
                "BOX NR 011")
        };

        #endregion

        // GET: api/<BookingsController>
        [HttpGet]
        public IEnumerable<Bookings> Get()
        {
            return manager.GetAllBookings();
        }

        // GET api/<BookingsController>/5
        [HttpGet("{ordNr}")]
        public Bookings Get(int ordNr)
        {
            return bookingsList.Find(o => o.OrdNr == ordNr);
        }

        // POST api/<BookingsController>
        [HttpPost]
        public void Post([FromBody] Bookings value)
        {
            value.OrdNr = idCount++;
            bookingsList.Add(value);
        }

        // PUT api/<BookingsController>/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody] Bookings value)
        {
            Bookings bookings = Get(id);
            if (bookings != null)
            {
                bookings.OrdNr = value.OrdNr;
                bookings.StartAdr = value.StartAdr;
                bookings.StartTime = value.StartTime;
                bookings.StartDate = value.StartDate;

                bookings.EndAdr = value.EndAdr;
                bookings.EndTime = value.EndTime;
                bookings.EndDate = value.EndDate;

                bookings.NumberOcn = value.NumberOcn;
                bookings.TypeOfGoods = value.TypeOfGoods;
                bookings.Comments = value.Comments;

                return 1;
            }

            return 0;
        }

        // DELETE api/<BookingsController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            Bookings bookings = Get(id);
            if (bookings != null)
            {
                BookingsController.bookingsList.Remove(bookings);
                return 1;
            }

            return 0;
        }



}
}