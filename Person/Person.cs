using PersonRegister;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

public class Person : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private static readonly Random random = new Random();

    private string lastName;
    private string name;
    private DateTime dateOfBirth;  

    private ObservableCollection<Address> addresses;
    public ObservableCollection<Address> Addresses
    {
        get { return addresses; }
        set
        {
            addresses = value;
            OnPropertyChanged(nameof(Addresses));
        }
    }

    private ObservableCollection<PhoneNumber> phoneNumbers;
    public ObservableCollection<PhoneNumber> PhoneNumbers
    {
        get { return phoneNumbers; }
        set
        {
            phoneNumbers = value;
            OnPropertyChanged(nameof(PhoneNumbers));
        }
    }

    private ObservableCollection<CustomField> customFields;
    public ObservableCollection<CustomField> CustomFields
    {
        get { return customFields; }
        set
        {
            customFields = value;
            OnPropertyChanged(nameof(CustomFields));
        }
    }
    public string Id { get; private set; }

    public string LastName
    {
        get => lastName;
        set
        {
            lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }

    public string Name
    {
        get => name;
        set
        {
            name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public DateTime DateOfBirth
    {
        get => dateOfBirth;
        set
        {
            dateOfBirth = value;
            OnPropertyChanged(nameof(DateOfBirth));
        }
    }
    public Person(string id, string name, string lastName, DateTime dateOfBirth)
        : this(id) 
    {
        Id = id;
        Name = name;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Addresses = new ObservableCollection<Address>();
        PhoneNumbers = new ObservableCollection<PhoneNumber>();
        CustomFields = new ObservableCollection<CustomField>();

    }

    public Person(string id)
    {
        Id = id;
        Addresses = new ObservableCollection<Address>();
        PhoneNumbers = new ObservableCollection<PhoneNumber>();
        CustomFields = new ObservableCollection<CustomField>();

    }
    public Person()
    {
        Id = $"{(DateTime.Now.Ticks + random.Next(1, 100)) % 1000000}";
        //CustomFields = new ObservableCollection<CustomField>();
    }    

    public static Person CreateFromCsv(string[] values)
    {
        if (values.Length < 6)
            throw new ArgumentException("Insufficient data to create Person.");

        var person = new Person(values[0])
        {
            LastName = values[1],
            Name = values[2],
            DateOfBirth = DateTime.Parse(values[3]),
            Addresses = new ObservableCollection<Address>(),
            PhoneNumbers = new ObservableCollection<PhoneNumber>(),
            CustomFields = new ObservableCollection<CustomField>()
        };

        if (!string.IsNullOrWhiteSpace(values[4]))
        {
            var addressParts = values[4].Split(';');
            foreach (var address in addressParts)
            {
                var parts = address.Split(' ');
                if (parts.Length == 7)
                {
                    // SOMETHING WENT WRONG HERE CAT1 and CAT2 needs to be checked...
                    var newAddress = new Address(
                        parts[0],  // Category HOME/OFFICE                        
                        parts[1],  // Floor
                        parts[2],  // BuildingNumber
                        parts[3],  // Street
                        parts[4],  // PostalCode
                        parts[5],  // State
                        parts[6]   // Country
                    );
                    person.Addresses.Add(newAddress);
                }                
            }
        }

        if (!string.IsNullOrWhiteSpace(values[5]))
        {
            var phoneParts = values[5].Split(';');
            foreach (var phone in phoneParts)
            {
                var parts = phone.Split(':');
                if (parts.Length == 2)
                {
                    var newPhone = new PhoneNumber(parts[1], parts[0]);
                    person.PhoneNumbers.Add(newPhone);
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(values[6]))
        {
            var customFieldParts = values[6].Split(';');
            foreach (var customField in customFieldParts)
            {
                var parts = customField.Split(':');
                if (parts.Length == 2)
                {
                    var newCustomField = new CustomField(parts[0], parts[1]);
                    person.CustomFields.Add(newCustomField);
                }
            }
        }

        return person;
    }

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }    

    //public override string ToString()
    //{
    //    string addresses = Addresses.Count > 0 ? string.Join(", ", Addresses) : "No Addresses";
    //    string phoneNumbers = PhoneNumbers.Count > 0 ? string.Join(", ", PhoneNumbers) : "No Phone Numbers";

    //    return $"ID: {Id}, {LastName}, {Name} (DOB: {DateOfBirth.ToShortDateString()}) - Addresses: {addresses}, Phone Numbers: {phoneNumbers}";
    //}
}
