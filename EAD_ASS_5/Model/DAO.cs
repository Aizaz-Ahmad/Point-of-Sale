using EAD_ASS_5.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
namespace EAD_ASS_5.Model
{
    class DAO
    {
        //no need to change connection String, it will automatically adjust according to Device on which it's running
        //but in case of error, try replacing the string
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.GetFullPath("../../Database/POS.mdf") + ";Integrated Security=True";
        private static SqlConnection connection = new SqlConnection(connectionString);

        public static List<ItemCategory> GetItemCategories()
        {
            List<ItemCategory> itemCategories = new List<ItemCategory>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM item_categories", connection);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                    itemCategories.Add(new ItemCategory { id = Convert.ToInt32(dr.GetInt32(0)), name = dr.GetString(1) });
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return itemCategories;
        }
        public static List<Item> GetItems()
        {
            List<Item> items = new List<Item>();
            try
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM item I, item_categories IC WHERE I.item_category_id = IC.id", connection);
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    items.Add(new Item
                    {
                        id = dr.GetInt32(0),
                        name = dr.GetString(1),
                        quantity = dr.GetInt32(2),
                        price = dr.GetDouble(3),
                        itemCategory = new ItemCategory { id = dr.GetInt32(6), name = dr.GetString(7) },
                        tax = dr.GetBoolean(5)
                    });
                }

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return items;
        }
        /// <summary>
        /// Function to save Order to the Database and updating item quantities accordingly
        /// </summary>
        /// <param name="order">Order to be saved</param>
        /// <returns>Unique ID of the order saved to the DB</returns>
        public static int saveOrder(Order order)
        {
            try
            {
                connection.Open();
                order.date_created = DateTime.Now;
                SqlCommand command = new SqlCommand("INSERT INTO food_order (discount, subTotal, tax, grandTotal, date_created) VALUES (@discount, @subTotal, @tax, @grandTotal, @date_created); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.Add(new SqlParameter("@discount", order.discountPercentage));
                command.Parameters.Add(new SqlParameter("@subTotal", order.subTotal));
                command.Parameters.Add(new SqlParameter("@tax", order.Tax));
                command.Parameters.Add(new SqlParameter("@grandTotal", order.grandTotal));
                command.Parameters.Add(new SqlParameter("@date_created", order.date_created));
                int lastInsertedId = Convert.ToInt32(command.ExecuteScalar());
                SqlCommand insertOrderItemCommand = new SqlCommand("INSERT INTO order_items (order_id, item_id, comments) VALUES (@order_id, @item_id, @comments)", connection);
                SqlCommand updateItemQuantity = new SqlCommand("UPDATE item SET quantity = quantity - @newQuantity WHERE id = @item_id", connection);
                foreach (OrderItem orderItem in order.orderItems)
                {
                    insertOrderItemCommand.Parameters.Add(new SqlParameter("@order_id", lastInsertedId));
                    insertOrderItemCommand.Parameters.Add(new SqlParameter("@item_id", orderItem.id));
                    insertOrderItemCommand.Parameters.Add(orderItem.comments == null ? new SqlParameter("@comments", DBNull.Value) : new SqlParameter("@comments", orderItem.comments));
                    if (insertOrderItemCommand.ExecuteNonQuery() != -1)
                    {
                        updateItemQuantity.Parameters.Add(new SqlParameter("@newQuantity", orderItem.quantity));
                        updateItemQuantity.Parameters.Add(new SqlParameter("@item_id", orderItem.id));
                        updateItemQuantity.ExecuteNonQuery();
                        updateItemQuantity.Parameters.Clear();
                    }
                    insertOrderItemCommand.Parameters.Clear();
                }
                return lastInsertedId;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public static ObservableCollection<string> GetadditionalItemsName()
        {
            ObservableCollection<string> names = new ObservableCollection<string>();
            names.Add("Milk");
            names.Add("Sugar");
            names.Add("Salt");
            names.Add("Cream");
            names.Add("Spicy");
            names.Add("Sauce");
            names.Add("Ice");
            names.Add("Tomotto");
            return names;
        }
    }
}
