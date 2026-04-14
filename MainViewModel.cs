using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace PersonRegister
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> People { get; set; } = new ObservableCollection<Person>();
        private Person selectedPerson;
        public Person CurrentPerson { get; set; }
        //private CustomField _selectedCustomField;
        //private ObservableCollection<CustomField> _customFields;


        public MainViewModel()
        {
            People = new ObservableCollection<Person>();
        }

        public Person SelectedPerson
        {
            get { return selectedPerson; }
            set
            {
                selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
            }
        }

        //public CustomField SelectedCustomField
        //{
        //    get => _selectedCustomField;
        //    set
        //    {
        //        if (_selectedCustomField != value)
        //        {
        //            _selectedCustomField = value;
        //            OnPropertyChanged(nameof(SelectedCustomField));
        //        }
        //    }
        //}
        //public ObservableCollection<CustomField> CustomFields
        //{
        //    get => _customFields;
        //    set
        //    {
        //        if (_customFields != value)
        //        {
        //            _customFields = value;
        //            OnPropertyChanged(nameof(CustomFields)); 
        //        }
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddPerson(Person person)
        {
            var newPerson = new Person
            {
                //Id = Guid.NewGuid().ToString(),
                LastName = person.LastName,
                Name = person.Name,
                DateOfBirth = person.DateOfBirth,
                Addresses = new ObservableCollection<Address>(person.Addresses),
                PhoneNumbers = new ObservableCollection<PhoneNumber>(person.PhoneNumbers),
                CustomFields = new ObservableCollection<CustomField>(person.CustomFields)
            };

            People.Add(newPerson);
        }

        public void EditPerson(Person originalPerson, Person updatedPerson)
        {

            var index = People.IndexOf(originalPerson);
            if (index >= 0)
            {
                People[index].Name = updatedPerson.Name;
                People[index].LastName = updatedPerson.LastName;
                People[index].DateOfBirth = updatedPerson.DateOfBirth;
                People[index].Addresses = updatedPerson.Addresses;
                People[index].PhoneNumbers = updatedPerson.PhoneNumbers;
                People[index].CustomFields = updatedPerson.CustomFields;
            }
        }

        //public void EditPerson(Person oldPerson, Person newPerson)
        //{
        //    int index = People.IndexOf(oldPerson);
        //    if (index != -1)
        //    {
        //        People[index] = new Person
        //        {
        //            Id = oldPerson.Id,
        //            LastName = newPerson.LastName,
        //            Name = newPerson.Name,
        //            DateOfBirth = newPerson.DateOfBirth,
        //            Addresses = new ObservableCollection<Address>(newPerson.Addresses),
        //            PhoneNumbers = new ObservableCollection<PhoneNumber>(newPerson.PhoneNumbers)
        //        };
        //    }
        //}

        public void DeletePerson(Person person)
        {
            People.Remove(person);
        }

        public void SaveToFile(string fileName)
        {
            StringBuilder sb = new();

            foreach (var person in People)
            {
                string addresses = string.Join(";", person.Addresses.Select(a => $"{a.Category1}" + " " + $"{a.Floor}" + " " + $"{a.BuildingNumber}" + " " + $"{a.Street}" + " " + $"{a.PostalCode}" + " " + $"{a.State}" + " " + $"{a.Country}"));
                string phoneNumbers = string.Join(";", person.PhoneNumbers.Select(b => $"{b.Category}:{b.Number}"));
                string customFields = string.Join(";", person.CustomFields.Select(c => $"{c.FieldName}:{c.FieldValue}"));
                string line = $"{person.Id},{person.LastName},{person.Name},{person.DateOfBirth.ToShortDateString()},{addresses},{phoneNumbers},{customFields}";

                sb.AppendLine(line);
            }

            File.WriteAllText(fileName, sb.ToString());
        }

        public void LoadFromFile(string fileName)
        {
            try
            {
                People.Clear();
                System.Collections.Generic.HashSet<string> seenIds = new System.Collections.Generic.HashSet<string>();
                System.Text.StringBuilder duplicatesWarning = new System.Text.StringBuilder();

                foreach (var line in File.ReadAllLines(fileName))
                    try
                    {
                        var values = line.Split(',');

                        var person = Person.CreateFromCsv(values);

                        if (seenIds.Contains(person.Id))
                        {
                            duplicatesWarning.AppendLine($"- ID: {person.Id}, Name: {person.Name} {person.LastName}");
                        }
                        else
                        {
                            seenIds.Add(person.Id);
                            People.Add(person);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing line: {ex.Message}");
                    }

                if (duplicatesWarning.Length > 0)
                {
                    System.Windows.MessageBox.Show(
                        "The following individuals have not been loaded, because of identical IDs:\n" + duplicatesWarning.ToString(),
                        "IDs not unique",
                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading from file: {ex.Message}");
            }
        }
    }
}
