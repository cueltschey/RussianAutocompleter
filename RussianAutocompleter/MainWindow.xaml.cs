using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace RussianAutocompleter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordBank WordsBank;
        public List<WordData> AutocompleteOptions = new List<WordData>();
        private int selectedOption = 0;
        public MainWindow()
        {
            InitializeComponent();

            string cs = @"URI=file:" + App.dbpath;
            var conn = new SQLiteConnection(cs);
            string stm = "SELECT * FROM WordData";
            conn.Open();
            var cmd = new SQLiteCommand(stm, conn);
            SQLiteDataReader rdr = cmd.ExecuteReader();

            List<string> WordsData = new List<string>();

            while (rdr.Read())
            {
                WordsData.Add(rdr.GetString(1));
            }


            WordsBank = new WordBank(WordsData);
        }
        private void WireUpAutoList()
        {
            autoList.ItemsSource = AutocompleteOptions;
        }
        private void OpenAutoList()
        {
            autoListPopup.Visibility = Visibility.Visible;
            autoListPopup.IsOpen = true;
            autoList.Visibility = Visibility.Visible;
        }
        private void CloseAutoList()
        {
            autoListPopup.Visibility = Visibility.Collapsed;
            autoListPopup.IsOpen = false;
            autoList.Visibility = Visibility.Collapsed;
        }
        private void GetAutocompleteOptions()
        {
            AutocompleteOptions = new List<WordData>();
            List<string> searched = WordsBank.SearchBank(autoTextBox.Text.ToString(), 4);
            foreach (string s in searched)
            {
                var data = new WordData();
                data.WordRus = s;
                AutocompleteOptions.Add(data);
            }
            WireUpAutoList();
            selectedOption = 0;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (autoTextBox.Text.ToString() != "" && autoTextBox.Text.ToString()[0] > 1072)
            {
                GetAutocompleteOptions();
                if (AutocompleteOptions.Count != 0)
                {
                    OpenAutoList();
                }
                else
                {
                    CloseAutoList();
                }
            }
            else
            {
                CloseAutoList();
            }
        }
        private void AutofillOption_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock origin = e.Source as TextBlock;
            try
            {
                autoTextBox.Text = origin.Text.ToString();
                autoTextBox.Select(autoTextBox.Text.Length, 0);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CloseAutoList();
        }

        private void autoTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                autoTextBox.Text = AutocompleteOptions[selectedOption].WordRus;
                autoTextBox.Select(autoTextBox.Text.Length, 0);
            }
        }
    }
}
