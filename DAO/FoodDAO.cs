﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanlycantinWF.DTO;

namespace QuanlycantinWF.DAO
{
    public class FoodDAO

    {
        private static FoodDAO instance;

        public static FoodDAO Instance
        {
            get { if (instance == null) instance = new FoodDAO(); return FoodDAO.instance; }
            private set { FoodDAO.instance = value; }
        }

            private FoodDAO() { }

        public List<Food> GetFoodByCategoryID(int id)
        {
            List<Food>list = new List<Food>();

            string query = " select * from food where idcategory = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows) 
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "select * from food";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);


            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public List<Food> SearchFoodByName(string name)
        {
            List<Food> list = new List<Food>();

            string query = "select * from food where name = " + name;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);


            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }

        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("insert dbo.food (name, idcategory, price )VALUES (N'{0}', {1}, {2})", name, id, price);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;


        }

        public bool UpdatetFood(int idfood, string name, int id, float price)
        {
            string query = string.Format("update dbo.food set name = N'{0}', idcategory = {1}, price = {2} where id = {3}", name, id, price, idfood);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;


        }
        public bool DeleteFood(int idfood)
        {

            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idfood);
            string query = string.Format("delete food where id = {0}", idfood);
            int result = DataProvider.Instance.ExecuteNonquery(query);

            return result > 0;


        }
    }
}
