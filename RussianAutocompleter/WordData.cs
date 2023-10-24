using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace RussianAutocompleter
{
    public class WordData
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public string WordRus { get; set; }
        public string WordEng { get; set; }
    }
}
