using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SearchForm
{
    public partial class Form1 : Form
    {
        private TextBox searchTextBox;
        private Button searchButton;
        private ListBox resultsListBox; // Assuming you want to display results in a ListBox

        public Form1()
        {
            InitializeComponent();
            InitializeSearchComponents();
        }

        private void InitializeSearchComponents()
        {
            // Initialize the TextBox
            searchTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10), // Set appropriate location
                Width = 200
            };

            // Initialize the Button
            searchButton = new Button
            {
                Location = new System.Drawing.Point(220, 10), // Set appropriate location
                Text = "Search"
            };
            searchButton.Click += SearchButton_Click;

            // Initialize the ListBox to show results
            resultsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 40), // Set appropriate location
                Width = 210,
                Height = 100
            };

            // Now add the controls to the Form
            Controls.Add(searchTextBox);
            Controls.Add(searchButton);
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

        // Dummy search implementation for demo purpose
        // In a real-world scenario, you might search a database, file, etc.
        private IEnumerable<string> PerformSearch(string searchTerm)
        {
            List<string> sampleData = new List<string>
        {
            "tatakei1",
            "tatakei2",
            "example",
            "test",
            "demo",
            // ... You can add more strings to search through here
        };

            // Performing a simple case-insensitive search.
            // In a real scenario, you might use more complex searching like regex, Levenshtein distance, etc.
            return sampleData.Where(str => str.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        }
    }
}