﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlycantinWF.DTO
{
    public class Table
    {
        private string status;

        private string name;

        private int iD;
        private DataRow item;

        public string Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }
        public int ID { get => iD; set => iD = value; }
       public Table(int id, string name, string status)
        {
            this.ID = id;
            this.Name = name;
            this.Status = status;
        }

        public Table(DataRow row)
        {
            this.iD = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Status = row["status"].ToString();
        }
    }
}
