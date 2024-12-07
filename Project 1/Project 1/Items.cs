public class Item
{
    // Properties with get/set
    private string Name { get; set; }
    private string Category { get; set; }
    private int Quantity { get; set; }
    private double Price { get; set; }

    // Constructor Overloading (wala pa encapsulation)
    public Item(string name, int quantity, double price)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
        Category = "Uncategorized"; // Default category
    }

    public Item(string name, string category, int quantity, double price)
    {
        Name = name;
        Category = category;
        Quantity = quantity;
        Price = price;
    }
}
