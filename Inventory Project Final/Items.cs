public class Item
{
    // Static field to hold the current ID
    public static int currentId = 0;

    // Properties
    public int ID { get; set; }
    private string name;
    private string category;
    private int quantity;
    private double price;

    // Encapsulated properties with validation
    public string Name
    {
        get { return name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.");
            name = value;
        }
    }

    public string Category
    {
        get { return category; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                category = "Uncategorized"; // Default value
            else
                category = value;
        }
    }

    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Quantity cannot be negative.");
            quantity = value;
        }
    }

    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Price cannot be negative.");
            price = value;
        }
    }

    // Constructors
    public Item(string name, int quantity, double price)
    {
        ID = ++currentId; // Auto-increment the ID
        Name = name;
        Quantity = quantity;
        Price = price;
        Category = "Uncategorized"; // Default category
    }

    public Item(string name, string category, int quantity, double price)
    {
        ID = ++currentId; // Auto-increment the ID
        Name = name;
        Category = category;
        Quantity = quantity;
        Price = price;
    }

    // Constructor for loading data with a specified ID
    public Item(int id, string name, string category, int quantity, double price)
    {
        ID = id;
        Name = name;
        Category = category;
        Quantity = quantity;
        Price = price;
    }

    // Method to update the static ID counter
    public static void UpdateCurrentId(int maxId)
    {
        if (maxId > currentId)
        {
            currentId = maxId;
        }
    }
}
