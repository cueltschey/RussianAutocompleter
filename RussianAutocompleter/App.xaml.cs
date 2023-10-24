using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RussianAutocompleter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static readonly string DataBaseName = "WordsData.db";
        static readonly string FolderPath = "C:/Users/chase ueltschey/source/repos/RussianAutocompleter/RussianAutocompleter/";
        public static string dbpath = System.IO.Path.Combine(FolderPath, DataBaseName);
    }
}
