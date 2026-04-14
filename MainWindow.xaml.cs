using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PersonRegister
{
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainViewModel();
            PeopleDataGrid.ItemsSource = viewModel.People;
        }

        private void AddPerson_Click(object sender, RoutedEventArgs e)
        {
            //PersonEntryWindow personEntryWindow = new PersonEntryWindow(viewModel);
            var personEntryWindow = new PersonEntryWindow(viewModel);

            personEntryWindow.Left = this.Left;  
            personEntryWindow.Top = this.Top;            

            personEntryWindow.ShowDialog();

            //var personEntryWindow = new PersonEntryWindow(viewModel);
            //personEntryWindow.ShowDialog();
        }

        private void EditPerson_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleDataGrid.SelectedItem is Person selectedPerson)
            {
                var personEntryWindow = new PersonEntryWindow(viewModel, selectedPerson);
                personEntryWindow.Left = this.Left;
                personEntryWindow.Top = this.Top;
                personEntryWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a person to edit.");
            }
        }

        private void DeletePerson_Click(object sender, RoutedEventArgs e)
        {
            if (PeopleDataGrid.SelectedItem is Person selectedPerson)
            {
                var result = MessageBox.Show(
                    "Are you sure you want to delete this person?",
                    "Confirm Delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    viewModel.DeletePerson(selectedPerson);
                }
            }
            else
            {
                MessageBox.Show("Please select a person to delete.");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FileNameTextBox.Text))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save File",
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    DefaultExt = "txt"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    FileNameTextBox.Text = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            try
            {
                viewModel.SaveToFile(FileNameTextBox.Text);
                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving data: {ex.Message}");
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a File",
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == true)
            {
                FileNameTextBox.Text = openFileDialog.FileName;
                try
                {
                    viewModel.LoadFromFile(FileNameTextBox.Text);
                    MessageBox.Show("Data loaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading data: {ex.Message}");
                }
            }
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // 
            //
        }
    }
}

