using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RussianAutocompleter
{
    internal class WordBank
    {
        public Trie root;
        public WordBank(List<string> words)
        {
            root = new Trie();
            foreach(string word in words)
            {
                root.Insert(word);
            }
        }
        public List<string> SearchBank(string word, int maxSize)
        {
            List<string> result = root.Search(word);
            return result.Take(maxSize).ToList();
        }
        public void Show()
        {
            root.Display();
        }
    }
}
