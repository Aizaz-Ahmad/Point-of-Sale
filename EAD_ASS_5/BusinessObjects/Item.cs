namespace EAD_ASS_5.BusinessObjects
{
    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public bool tax { get; set; }
        public ItemCategory itemCategory { get; set; }
        public Item()
        {

        }
        public Item(Item other)
        {
            id = other.id;
            name = other.name;
            quantity = other.quantity;
            price = other.price;
            itemCategory = other.itemCategory;
            tax = other.tax;
        }
    }
}
