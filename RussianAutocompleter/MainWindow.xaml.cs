using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace RussianAutocompleter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WordBank WordsBank;
        public List<WordData> AutoResults = new List<WordData>();
        public MainWindow()
        {
            InitializeComponent();

            string cs = @"URI=file:" + App.dbpath;
            var conn = new SQLiteConnection(cs);
            string stm = "SELECT * FROM WordData LIMIT 250";
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
            autoList.ItemsSource = AutoResults;
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (autoTextBox.Text.ToString() != "" && autoTextBox.Text.ToString()[0] > 1072)
            {
                AutoResults = new List<WordData>();
                List<string> searched = WordsBank.SearchBank(autoTextBox.Text.ToString(), 5);
                foreach (string s in searched)
                {
                    var data = new WordData();
                    data.WordRus = s;
                    AutoResults.Add(data);
                }
                WireUpAutoList();
                if (AutoResults.Count != 0)
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
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            CloseAutoList();
        }
    }
}
