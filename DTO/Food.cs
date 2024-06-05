using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlycantinWF.DTO
{
    public class Food
    {
        public Food(int id,string name, int categoryID, float price)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.CategoryID = categoryID;
        }
        public Food(DataRow row)
        {
            this.Id = (int)row["id"];
            this.Name = (string)row["name"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.CategoryID = (int)row["idcategory"];
        }

        private int id;

        private string name;

        private int categoryID;

        private float price;

        public string Name { get => name; set => name = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
        public float Price { get => price; set => price = value; }
        public int Id { get => id; set => id = value; }
    }
}
