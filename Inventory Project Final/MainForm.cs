using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Project_1
{
    public partial class MainForm : Form
    {
        private ArrayList inventoryList = new ArrayList();

        public MainForm()
        {
            InitializeComponent();
            InitializeDataGridView();
            InitializeCategoryComboBox();
        }

        private void InitializeDataGridView()
        {
            dgvInventory.Columns.Add("ID", "ID"); // Add ID column
            dgvInventory.Columns.Add("Name", "Item Name");
            dgvInventory.Columns.Add("Category", "Category");
            dgvInventory.Columns.Add("Quantity", "Quantity");
            dgvInventory.Columns.Add("Price", "Price");
        }

        private void InitializeCategoryComboBox()
        {
            cbCategory.Items.Add("Shoes");
            cbCategory.Items.Add("Food");
        }

        // Method to add a new item to the inventory
        private void AddItem(string name, string category, int quantity, double price)
        {
            Item newItem = new Item(name, category, quantity, price);
            inventoryList.Add(newItem);
            RefreshGrid();
        }

        // Method to refresh the DataGridView with the current inventory data
        private void RefreshGrid()
        {
            dgvInventory.Rows.Clear();
            foreach (Item item in inventoryList)
            {
                dgvInventory.Rows.Add(item.ID, item.Name, item.Category, item.Quantity, item.Price);
            }
        }

        // Button click handler for Add
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text;
                string category = cbCategory.Text;
                int quantity = (int)nudQuantity.Value; // Get the value from NumericUpDown
                double price = double.Parse(txtPrice.Text);

                AddItem(name, category, quantity, price);
                ClearInputs();
            }
            catch (Exception ex)
            {
                ShowError("Error adding item", ex.Message);
            }
        }

        // Method to clear input fields
        private void ClearInputs()
        {
            txtName.Clear(); // Clear the text in the Name textbox
            cbCategory.SelectedIndex = -1; // Reset the ComboBox selection
            cbCategory.ResetText();
            nudQuantity.Value = 0; // Reset NumericUpDown to its default value
            txtPrice.Clear(); // Clear the text in the Price textbox
            txtSearch.Clear(); // Optionally clear the search textbox
        }


        // Method to update an existing item
        private void UpdateItem(int id, string name, string category, int quantity, double price)
        {
            Item item = inventoryList.OfType<Item>().FirstOrDefault(i => i.ID == id);
            if (item != null)
            {
                item.Name = name;
                item.Category = category;
                item.Quantity = quantity;
                item.Price = price;
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button click handler for Update
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    int id = (int)dgvInventory.SelectedRows[0].Cells["ID"].Value;
                    string name = txtName.Text;
                    string category = cbCategory.Text;
                    int quantity = (int)nudQuantity.Value;
                    double price = double.Parse(txtPrice.Text);
                    UpdateItem(id, name, category, quantity, price);
                }
                else
                {
                    MessageBox.Show("Please select an item to update.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error updating item", ex.Message);
            }
        }

        // Method to delete an item from the inventory
        private void DeleteItem(int id)
        {
            Item item = inventoryList.OfType<Item>().FirstOrDefault(i => i.ID == id);
            if (item != null)
            {
                inventoryList.Remove(item);
                RefreshGrid();
            }
            else
            {
                MessageBox.Show("Item not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Button click handler for Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    int id = (int)dgvInventory.SelectedRows[0].Cells["ID"].Value;
                    DeleteItem(id);
                }
                else
                {
                    MessageBox.Show("Please select an item to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error deleting item", ex.Message);
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
                    dgvInventory.Rows.Add(item.ID, item.Name, item.Category, item.Quantity, item.Price);
                }
            }
        }

        // Button click handler for Search
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            SearchItems(keyword);
        }

        // Method to save the inventory to a file
        private void btnSave_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\Benji\Documents\inventory.csv";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    foreach (Item item in inventoryList)
                    {
                        writer.WriteLine($"{item.ID},{item.Name},{item.Category},{item.Quantity},{item.Price}");
                    }
                }
                MessageBox.Show("Inventory saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Error saving inventory", ex.Message);
            }
        }

        // Method to load the inventory from a file
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadInventory(@"C:\Users\Benji\Documents\inventory.csv");
        }

        // Method to load the inventory from a specified file path
        private void LoadInventory(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    inventoryList.Clear(); // Clear the current list before loading new data
                    int maxId = 0; // Track the highest ID in the file
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 5 &&
                            int.TryParse(parts[0], out int id) &&
                            int.TryParse(parts[3], out int quantity) &&
                            double.TryParse(parts[4], out double price))
                        {
                            Item newItem = new Item(id, parts[1], parts[2], quantity, price);
                            inventoryList.Add(newItem);
                            if (id > maxId) maxId = id;
                        }
                        else
                        {
                            ShowError("Error", "Invalid data format in file.");
                            return;
                        }
                    }
                    // Update the static ID counter to prevent duplicates
                    Item.UpdateCurrentId(maxId);
                }
                RefreshGrid(); // Refresh the DataGridView with the loaded data
                MessageBox.Show("Inventory loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowError("Error loading inventory", ex.Message);
            }
        }


        // Helper method to show error messages
        private void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

   
}
