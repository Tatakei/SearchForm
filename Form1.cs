using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SearchForm
{
    public partial class Form1 : Form
    {
        private TextBox searchTextBox;
        private Button searchButton;
        private Button removeButton;
        private Button addButton;
        private ListBox resultsListBox;
        private List<string> sampleData; // Full list of items that can be searched
        private List<string> removedItems; // List of removed items
        private string removedItemsFilePath = "removedItems.txt";

        public Form1()
        {
            InitializeComponent();
            InitializeSearchComponents();
            sampleData = new List<string>
            {
                "tatakei1",
                "tatakei2",
                "example",
                "test",
                "demo",
                // ... other sample items
            };
            LoadRemovedItems(); // Restore any removed items
            resultsListBox.Items.AddRange(sampleData.Cast<object>().ToArray()); // Initialize listbox with items
        }

        private void InitializeSearchComponents()
        {
            // Initialize the TextBox
            searchTextBox = new TextBox()
            {
                Location = new System.Drawing.Point(10, 10), // Set appropriate location
                Width = 200
            };

            // Initialize the Search Button
            searchButton = new Button()
            {
                Location = new System.Drawing.Point(220, 10), // Set appropriate location
                Text = "Search"
            };
            searchButton.Click += SearchButton_Click;
            // Initialize the Remove Button
            removeButton = new Button()
            {
                Text = "Remove Selected",
                Location = new System.Drawing.Point(220, 40) // Place appropriately
            };
            removeButton.Click += RemoveButton_Click;

            // Initialize the Add Button
            addButton = new Button()
            {
                Text = "Add Removed Items",
                Location = new System.Drawing.Point(220, 70) // Place appropriately
            };
            addButton.Click += AddButton_Click;

            // Initialize the ListBox to show results
            resultsListBox = new ListBox()
            {
                Location = new System.Drawing.Point(10, 40), // Set appropriate location
                Width = 200,
                Height = 100
            };

            // Add the controls to the Form
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
            Controls.Add(removeButton);
            Controls.Add(addButton);
            Controls.Add(resultsListBox);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                IEnumerable<string> searchResults = PerformSearch(searchTerm);
                resultsListBox.Items.Clear(); // Clear previous results
                resultsListBox.Items.AddRange(searchResults.Cast<object>().ToArray());
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (resultsListBox.SelectedIndex != -1)
            {
                string itemToRemove = resultsListBox.SelectedItem.ToString();
                sampleData.Remove(itemToRemove); // Remove the item from the main list
                removedItems.Add(itemToRemove); // Add the item to the removed list
                resultsListBox.Items.Remove(itemToRemove); // Remove the item from the ListBox
                SaveRemovedItems(); // Save the current state of removed items
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            foreach (var item in removedItems)
            {
                sampleData.Add(item); // Re-add to the main list
                resultsListBox.Items.Add(item); // Re-add to the ListBox
            }
            removedItems.Clear(); // Clear the removed items list
            SaveRemovedItems(); // Save the now empty removed items list
        }

        private void LoadRemovedItems()
        {
            removedItems = new List<string>();
            if (File.Exists(removedItemsFilePath))
            {
                removedItems = File.ReadAllLines(removedItemsFilePath).ToList();
            }
        }

        private void SaveRemovedItems()
        {
            File.WriteAllLines(removedItemsFilePath, removedItems);
        }

        private IEnumerable<string> PerformSearch(string searchTerm)
        {
            // Simple case-insensitive search within `sampleData`
            return sampleData.Where(item => item.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}