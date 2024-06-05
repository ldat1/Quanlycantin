using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlycantinWF.DTO
{
    public class Menu
    {
        public Menu(string foodname, int count, float price, float totalprice = 0)
        {
            this.foodname = foodname;
            this.count = count;
            this.price = price;
            this.totalPrice = totalprice;
        }
        public Menu(DataRow row)
        {
            this.Foodname =(string)row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble( row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["totalprice"].ToString());
        }
        private float totalPrice;
        private float price;
        private int count;
        private string foodname;

        public string Foodname { get => foodname; set => foodname = value; }
        public int Count { get => count; set => count = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
    }
}
