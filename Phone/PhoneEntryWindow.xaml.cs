using System.Windows;
using System.Windows.Controls;

namespace PersonRegister
{
    public partial class PhoneEntryWindow : Window
    {
        public string PhoneNumber { get; private set; }
        public string PhoneCategory { get; private set; }

        public PhoneEntryWindow()
        {
            InitializeComponent();
        }

        public PhoneEntryWindow(PhoneNumber selectedPhone)
        {
            InitializeComponent();
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Content.ToString() == selectedPhone.Category)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
            PhoneTextBox.Text = selectedPhone.Number; 
            //CategoryComboBox.SelectedItem = selectedPhone.Category; 
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            PhoneNumber = PhoneTextBox.Text.Trim(); 

          
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                PhoneCategory = selectedItem.Content.ToString(); 
            }

            if (!string.IsNullOrWhiteSpace(PhoneNumber) && !string.IsNullOrWhiteSpace(PhoneCategory))
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a phone number and select a category.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
