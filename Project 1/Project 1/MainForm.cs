using System.Collections;

namespace Project_1
{
    public partial class MainForm : Form
    {
        private ArrayList inventoryList = new ArrayList();
        public MainForm()
        {
            InitializeComponent();
            dgvInventory.Columns.Add("Name", "Item Name");
            dgvInventory.Columns.Add("Category", "Category");
            dgvInventory.Columns.Add("Quantity", "Quantity");
            dgvInventory.Columns.Add("Price", "Price");

            cbCategory.Items.Add("Shoes");
            cbCategory.Items.Add("Food");
        }
        // Method to add a new item to the inventory
        private void AddItem(string name, string category, int quantity, double price)
        {
            // Create a new Item object
            Item newItem = new Item(name, category, quantity, price);

            // Add the new item to the inventory list
            inventoryList.Add(newItem);

            // Refresh the DataGridView to show the updated inventory
            RefreshGrid();
        }
        // Method to refresh the DataGridView with the current inventory data
        private void RefreshGrid()
        {
            // Clear all rows in DataGridView to avoid duplicates
            dgvInventory.Rows.Clear();

            // Loop through each item in the inventory list
            foreach (Item item in inventoryList)
            {
                // Add a new row for each item
                dgvInventory.Rows.Add(item.Name, item.Category, item.Quantity, item.Price);
            }
        }


        //on click Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve input values from form controls
                string name = txtName.Text;
                string category = cbCategory.Text;
                int quantity = int.Parse(txtQuantity.Text);
                double price = double.Parse(txtPrice.Text);

                // Call the AddItem method to add the new item
                AddItem(name, category, quantity, price);

                // Clear input fields after adding the item
                ClearInputs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to clear input fields
        private void ClearInputs()
        {
            txtName.Clear();
            cbCategory.SelectedIndex = -1;
            txtQuantity.Clear();
            txtPrice.Clear();
        }

        // Method to update an existing item
        private void UpdateItem(int index, string name, string category, int quantity, double price)
        {
            if (index >= 0 && index < inventoryList.Count)
            {
                Item item = (Item)inventoryList[index];
                item.Name = name;
                item.Category = category;
                item.Quantity = quantity;
                item.Price = price;

                // Refresh the grid to show the updated data
                RefreshGrid();
            }
        }
        //on click Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgvInventory.SelectedRows[0].Index;

                    // Retrieve input values from form controls
                    string name = txtName.Text;
                    string category = cbCategory.Text;
                    int quantity = int.Parse(txtQuantity.Text);
                    double price = double.Parse(txtPrice.Text);

                    // Update the item at the selected index
                    UpdateItem(selectedIndex, name, category, quantity, price);
                }
                else
                {
                    MessageBox.Show("Please select an item to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to delete an item from the inventory
        private void DeleteItem(int index)
        {
            if (index >= 0 && index < inventoryList.Count)
            {
                inventoryList.RemoveAt(index);
                RefreshGrid(); // Refresh the grid after deletion
            }
        }
        //on click Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    int selectedIndex = dgvInventory.SelectedRows[0].Index;

                    // Call DeleteItem to remove the selected item
                    DeleteItem(selectedIndex);
                }
                else
                {
                    MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to search for items by name or category
        private void SearchItems(string keyword)
        {
            dgvInventory.Rows.Clear();

            foreach (Item item in inventoryList)
            {
                if (item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) || item.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    dgvInventory.Rows.Add(item.Name, item.Category, item.Quantity, item.Price);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            SearchItems(keyword);
        }

        // Method to save the inventory to a file
        private void SaveToFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Item item in inventoryList)
                {
                    writer.WriteLine($"{item.Name},{item.Category},{item.Quantity},{item.Price}");
                }
            }

        }
        //on click SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveToFile("inventory.txt");
            MessageBox.Show("Inventory saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Method to load 
        private void LoadFromFile(string filePath)
        {
            inventoryList.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    string name = parts[0];
                    string category = parts[1];
                    int quantity = int.Parse(parts[2]);
                    double price = double.Parse(parts[3]);
                    inventoryList.Add(new Item(name, category, quantity, price));
                }
            }
            RefreshGrid();

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFromFile("inventory.txt");
            MessageBox.Show("Inventory loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    // Item class to represent each item in the inventory
    public class Item
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        // Constructor for Item with category
        public Item(string name, string category, int quantity, double price)
        {
            Name = name;
            Category = category;
            Quantity = quantity;
            Price = price;
        }

        // Constructor for Item without category
        public Item(string name, int quantity, double price)
        {
            Name = name;
            Category = "Uncategorized";
            Quantity = quantity;
            Price = price;
        }
    }

}
    



