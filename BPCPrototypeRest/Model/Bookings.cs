using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BPCPrototypeRest.Model
{
    public class Bookings
    {
        #region Instance fields
        private int _ordNr;
        //Start information
        private string _startAdr;
        private string _startTime;
        private string _startDate;

        //End information
        private string _endAdr;
        private string _endTime;
        private string _endDate;

        //Generel information
        private string _numberOCN;
        private string _typeOfGoods;
        private string _comments;
        #endregion

        #region Contructors

        public Bookings(int ordNr, string startAdr, string startTime, string startDate, string endAdr, string endTime, string endDate, string numberOcn, string typeOfGoods, string comments)
        {
            _ordNr = ordNr;
            _startAdr = startAdr;
            _startTime = startTime;
            _startDate = startDate;
            _endAdr = endAdr;
            _endTime = endTime;
            _endDate = endDate;
            _numberOCN = numberOcn;
            _typeOfGoods = typeOfGoods;
            _comments = comments;
        }

        public Bookings()
        {

        }
        #endregion

        #region Properties

        public int OrdNr
        {
            get => _ordNr;
            set => _ordNr = value;
        }

        public string StartAdr
        {
            get => _startAdr;
            set => _startAdr = value;
        }

        public string StartTime
        {
            get => _startTime;
            set => _startTime = value;
        }

        public string StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public string EndAdr
        {
            get => _endAdr;
            set => _endAdr = value;
        }

        public string EndTime
        {
            get => _endTime;
            set => _endTime = value;
        }

        public string EndDate
        {
            get => _endDate;
            set => _endDate = value;
        }

        public string NumberOcn
        {
            get => _numberOCN;
            set => _numberOCN = value;
        }

        public string TypeOfGoods
        {
            get => _typeOfGoods;
            set => _typeOfGoods = value;
        }

        public string Comments
        {
            get => _comments;
            set => _comments = value;
        }

        #endregion


        #region Methods

        public override string ToString()
        {
            return $"{nameof(OrdNr)}: {OrdNr}, {nameof(StartAdr)}: {StartAdr}, {nameof(StartTime)}: {StartTime}, {nameof(StartDate)}: {StartDate}, {nameof(EndAdr)}: {EndAdr}, {nameof(EndTime)}: {EndTime}, {nameof(EndDate)}: {EndDate}, {nameof(NumberOcn)}: {NumberOcn}, {nameof(TypeOfGoods)}: {TypeOfGoods}, {nameof(Comments)}: {Comments}";
        }

        #endregion



    }
}
