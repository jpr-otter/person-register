using System.Windows;
using System.Windows.Controls;

namespace PersonRegister
{
    public partial class AddressEntryWindow : Window
    {
        //public string AddressCategory { get; set; }
        public string Category1 { get; set; }
        //public string AddressCategory2 { get; set; }
        public string Floor { get; private set; }
        public string BuildingNumber { get; private set; }
        public string Street { get; private set; }
        public string PostalCode { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }

        public AddressEntryWindow()
        {
            InitializeComponent();
        }

        public AddressEntryWindow(Address selectedAddress)
        {
            InitializeComponent();
            //CategoryComboBox.SelectedItem = selectedAddress.Category1;
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Content.ToString() == selectedAddress.Category1)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
            FloorTextBox.Text = selectedAddress.Floor; 
            BuildingNumberTextBox.Text = selectedAddress.BuildingNumber; 
            StreetTextBox.Text = selectedAddress.Street; 
            PostalCodeTextBox.Text = selectedAddress.PostalCode; 
            StateTextBox.Text = selectedAddress.State; 
            CountryTextBox.Text = selectedAddress.Country; 
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                Category1 = selectedItem.Content.ToString();
            }
            Floor = FloorTextBox.Text.Trim();
            BuildingNumber = BuildingNumberTextBox.Text.Trim();
            Street = StreetTextBox.Text.Trim();
            PostalCode = PostalCodeTextBox.Text.Trim();
            State = StateTextBox.Text.Trim();
            Country = CountryTextBox.Text.Trim();

            
            if (!string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(BuildingNumber) && !string.IsNullOrWhiteSpace(Category1))
            {
                DialogResult = true; 
            }
            else
            {
                MessageBox.Show("Please enter a complete address with required fields.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; 
        }
    }
}
