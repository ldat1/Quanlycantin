using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlycantinWF.DTO
{
    public class Bill
    {
        public Bill(int id, DateTime? DateCheckIn, DateTime? DateCheckOut, int status)
        {
            this.iD = id;
            this.DateCheckIn = DateCheckIn;
            this.DateCheckOut = DateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.iD =(int)row ["id"];
            this.DateCheckIn =(DateTime?)row ["DateCheckIn"];

            var datechekcouttemp = row["DateCheckOut"];
            if(datechekcouttemp.ToString() != "" ) 

            this.DateCheckOut = (DateTime?)DateCheckOut;
            this.Status = (int)row["status"];
        }

        private int iD;
        private DateTime? DateCheckIn;
        private DateTime? DateCheckOut;
        private int status;
  
        public int ID { get => iD; set => iD = value; }
        public DateTime? DateCheckIn1 { get => DateCheckIn; set => DateCheckIn = value; }
        public DateTime? DateCheckOut1 { get => DateCheckOut; set => DateCheckOut = value; }
        public int Status { get => status; set => status = value; }
    }
}
