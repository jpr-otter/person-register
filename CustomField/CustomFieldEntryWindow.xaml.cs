using System.Windows;

namespace PersonRegister
{
    public partial class CustomFieldEntryWindow : Window
    {
        //public CustomField NewCustomField { get; private set; }
        public string FieldName { get; set; }
        public string FieldValue { get; set; }

        public CustomFieldEntryWindow()
        {
            InitializeComponent();
        }
        public CustomFieldEntryWindow(CustomField selectedField)
        {
            InitializeComponent();
            FieldValueTextBox.Text = selectedField.FieldValue;
            FieldNameTextBox.Text = selectedField.FieldName;
        }

        private void AddCustomField_Click(object sender, RoutedEventArgs e)
        {
            FieldName = FieldNameTextBox.Text.Trim();
            FieldValue = FieldValueTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(FieldName) || string.IsNullOrWhiteSpace(FieldValue))
            {
                MessageBox.Show("Both field name and value must be filled out.");
                return;
            }

            // NewCustomField = new CustomField(FieldName, FieldValue);
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
