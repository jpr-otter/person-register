using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace PersonRegister
{
    public partial class PersonEntryWindow : Window
    {
        private MainViewModel viewModel;
        private Person person;
        public Person CurrentPerson { get; set; }

        public PersonEntryWindow(MainViewModel viewModel, Person person = null)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.person = person;
            //CustomFieldsListBox.ItemsSource = person?.CustomFields ?? new ObservableCollection<CustomField>();

            if (person != null)
            {
                IdTextBlock.Text = person.Id;
                LastNameTextBox.Text = person.LastName;
                NameTextBox.Text = person.Name;
                DateOfBirthPicker.SelectedDate = person.DateOfBirth;
                foreach (var address in person.Addresses)
                {
                    AddressesListBox.Items.Add(address);
                }
                foreach (var phone in person.PhoneNumbers)
                {
                    PhoneNumbersListBox.Items.Add(phone);
                }
                foreach (var customField in person.CustomFields)
                {
                    CustomFieldsListBox.Items.Add(customField);
                }
            }
            else
            {
                person = new Person();
                IdTextBlock.Text = person.Id;
            }
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e) { /* Handle focus */ }
        private void LastNameTextBox_GotFocus(object sender, RoutedEventArgs e) { /* Handle focus */ }
        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e) { /* Handle lost focus */ }
        private void LastNameTextBox_LostFocus(object sender, RoutedEventArgs e) { /* Handle lost focus */ }

        private void AddAddress_Click(object sender, RoutedEventArgs e)
        {
            AddressEntryWindow addressEntryWindow = new AddressEntryWindow();

            addressEntryWindow.Left = this.Left;
            addressEntryWindow.Top = this.Top;

            if (addressEntryWindow.ShowDialog() == true)
            {
                var address = new Address(
                    
                    addressEntryWindow.Category1,
                    addressEntryWindow.Floor,
                    addressEntryWindow.BuildingNumber,
                    addressEntryWindow.Street,
                    addressEntryWindow.PostalCode,
                    addressEntryWindow.State,
                    addressEntryWindow.Country);
                if (viewModel.CurrentPerson != null)
                {                    
                    viewModel.CurrentPerson.Addresses.Add(address);
                } 
                AddressesListBox.Items.Add(address);
            }
        }

        private void RemoveAddress_Click(object sender, RoutedEventArgs e)
        {
            if (AddressesListBox.SelectedItem is Address selectedAddress)
            {
                var result = MessageBox.Show($"Are you sure you want to remove the address containing the street '{selectedAddress.Street}'?", "Confirm Delete", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    AddressesListBox.Items.Remove(AddressesListBox.SelectedItem);
                }
            }           
        }

        private void AddPhone_Click(object sender, RoutedEventArgs e)
        {
            PhoneEntryWindow phoneEntryWindow = new()
            {
                Left = this.Left,
                Top = this.Top
            };

            if (phoneEntryWindow.ShowDialog() == true)
            {
                var phoneNumber = new PhoneNumber(
                    phoneEntryWindow.PhoneNumber,
                    phoneEntryWindow.PhoneCategory);
                if (viewModel.CurrentPerson != null)
                {                
                    viewModel.CurrentPerson.PhoneNumbers.Add(phoneNumber); 
                }
                PhoneNumbersListBox.Items.Add(phoneNumber);
            }
        }

        private void RemovePhone_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneNumbersListBox.SelectedItem is PhoneNumber selectedPhoneNumber)
            {
                var result = MessageBox.Show($"Are you sure you want to remove the phone number '{selectedPhoneNumber.Number}'?", "Confirm Delete", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    PhoneNumbersListBox.Items.Remove(PhoneNumbersListBox.SelectedItem);
                }
            }            
        }

        private void AddCustomField_Click(object sender, RoutedEventArgs e)
        {
            CustomFieldEntryWindow customFieldEntryWindow = new CustomFieldEntryWindow();

            customFieldEntryWindow.Left = this.Left;
            customFieldEntryWindow.Top = this.Top;

            if (customFieldEntryWindow.ShowDialog() == true)
            {
                var customField = new CustomField(
                    customFieldEntryWindow.FieldName,
                    customFieldEntryWindow.FieldValue);
                if (viewModel.CurrentPerson != null)
                {
                    viewModel.CurrentPerson.CustomFields.Add(customField);
                }
                CustomFieldsListBox.Items.Add(customField);
            }
        }
        private void RemoveCustomField_Click(object sender, RoutedEventArgs e)
        {
            if (CustomFieldsListBox.SelectedItem is CustomField selectedField)
            {
                var result = MessageBox.Show($"Are you sure you want to remove the custom field '{selectedField.FieldName}'?", "Confirm Delete", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    CustomFieldsListBox.Items.Remove(CustomFieldsListBox.SelectedItem);                    
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text) || string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Names cannot be empty.");
                return;
            }

            string personId = person?.Id ?? Guid.NewGuid().ToString();

            var newPerson = CreateNewPerson(personId);

            if (person != null)
            {
                viewModel.EditPerson(person, newPerson);
            }
            else
            {
                viewModel.AddPerson(newPerson);
            }

            Close();
        }

        private Person CreateNewPerson(string personId)
        {
            var newPerson = new Person(personId, NameTextBox.Text, LastNameTextBox.Text, DateOfBirthPicker.SelectedDate ?? DateTime.Now)
            {
                Addresses = [],
                PhoneNumbers = [],
                CustomFields = []
            };

            AddAddresses(newPerson);
            AddPhoneNumbers(newPerson);
            AddCustomFields(newPerson); 

            return newPerson;
        }

        private void AddAddresses(Person newPerson)
        {
            foreach (Address address in AddressesListBox.Items)
            {
                newPerson.Addresses.Add(address);
            }
        }

        private void AddPhoneNumbers(Person newPerson)
        {
            foreach (PhoneNumber phone in PhoneNumbersListBox.Items)
            {
                newPerson.PhoneNumbers.Add(phone);
            }
        }
        
        private void AddCustomFields(Person newPerson)
        {
            foreach (CustomField customField in CustomFieldsListBox.Items)
            {
                newPerson.CustomFields.Add(customField);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditAddress_Click(object sender, RoutedEventArgs e)
        {
            if (AddressesListBox.SelectedItem != null)
            {
                var selectedAddress = (Address)AddressesListBox.SelectedItem;
                int addressIndex = AddressesListBox.Items.IndexOf(selectedAddress);

                AddressEntryWindow addressEntryWindow = new AddressEntryWindow(selectedAddress);

                addressEntryWindow.Left = this.Left;
                addressEntryWindow.Top = this.Top;

                if (addressEntryWindow.ShowDialog() == true)
                {
                    var updatedAddress = new Address(

                        addressEntryWindow.Category1,                        
                        addressEntryWindow.Floor,
                        addressEntryWindow.BuildingNumber,
                        addressEntryWindow.Street,
                        addressEntryWindow.PostalCode,
                        addressEntryWindow.State,
                        addressEntryWindow.Country
                        );

                    AddressesListBox.Items[addressIndex] = updatedAddress; 
                }
            }
        }

        private void EditPhone_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneNumbersListBox.SelectedItem != null)
            {
                var selectedPhone = (PhoneNumber)PhoneNumbersListBox.SelectedItem;
                PhoneEntryWindow phoneEntryWindow = new PhoneEntryWindow(selectedPhone);

                phoneEntryWindow.Left = this.Left;
                phoneEntryWindow.Top = this.Top;
                 
                if (phoneEntryWindow.ShowDialog() == true)
                {
                    var updatedPhone = new PhoneNumber(
                        phoneEntryWindow.PhoneNumber, 
                        phoneEntryWindow.PhoneCategory
                        );

                    PhoneNumbersListBox.Items[PhoneNumbersListBox.SelectedIndex] = updatedPhone; 
                }
            }
        }
        private void EditCustomField_Click(object sender, RoutedEventArgs e)
        {

            if (CustomFieldsListBox.SelectedItem != null)
            {
                var selectedCustomField = (CustomField)CustomFieldsListBox.SelectedItem;
                int customFieldIndex = CustomFieldsListBox.Items.IndexOf(selectedCustomField);

                CustomFieldEntryWindow customFieldEntryWindow = new CustomFieldEntryWindow(selectedCustomField);

                customFieldEntryWindow.Left = this.Left;
                customFieldEntryWindow.Top = this.Top;

                if (customFieldEntryWindow.ShowDialog() == true)
                {
                    var updatedCustomField = new CustomField(
                        customFieldEntryWindow.FieldName,
                        customFieldEntryWindow.FieldValue
                        );                     

                    CustomFieldsListBox.Items[customFieldIndex] = updatedCustomField;
                }
            }
            //if (CustomFieldsListBox.SelectedItem is CustomField selectedField)
            //{
            //    var customFieldEntryWindow = new CustomFieldEntryWindow
            //    {
            //        FieldNameTextBox = { Text = selectedField.FieldName },
            //        FieldValueTextBox = { Text = selectedField.FieldValue }
            //    };

            //    if (customFieldEntryWindow.ShowDialog() == true)
            //    {
            //        selectedField.FieldName = customFieldEntryWindow.NewCustomField.FieldName;
            //        selectedField.FieldValue = customFieldEntryWindow.NewCustomField.FieldValue;

            //        CustomFieldsListBox.Items.Refresh();
            //    }
            //}
        }
    }
}
