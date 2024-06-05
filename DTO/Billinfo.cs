using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlycantinWF.DTO
{
    public class Billinfo
    {

        public Billinfo(int id, int idbill, int idfood, int count)
        {
            this.ID = id;
            this.IDBill = idbill;
            this.IDfood = idfood;
            this.Count = count;
        }

        public Billinfo(DataRow row)
        {
            this.ID =(int)row ["id"];
            this.IDBill =(int)row ["idbill"];
            this.IDfood =(int)row ["idfood"];
            this.Count =(int)row ["count"];
        }
        private int iD;
        private int iDBill;
        private int iDfood;
        private int count;

        public int ID { get => iD; set => iD = value; }
        public int IDBill { get => iDBill; set => iDBill = value; }
        public int IDfood { get => iDfood; set => iDfood = value; }
        public int Count { get => count; set => count = value; }
    }
}
