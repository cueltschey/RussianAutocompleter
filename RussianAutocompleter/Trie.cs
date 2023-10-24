using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RussianAutocompleter
{
    internal class Trie
    {
        public Trie[] children = new Trie[255];
        public List<string> wordsUnder = new List<string>();

        public void Insert(string word)
        {
            Trie temp = this;
            foreach(var c in word)
            {
                int index = c - 1072;
                if (temp.children[index] == null)
                {
                    temp.children[index] = new Trie();
                }
                temp.wordsUnder.Add(word);
                temp = temp.children[index];
            }
            temp.wordsUnder.Add(word);
        }

        public List<string> Search(string word)
        {
            Trie temp = this;
            foreach(var c in word)
            {
                int index = c - 1072;
                if (temp.children[index] == null)
                {
                    return new List<string>();
                }
                temp = temp.children[index];
            }
            return temp.wordsUnder;
        }
        public void Display()
        {
            Trie temp = this;
            foreach(var c in temp.wordsUnder)
            {
                MessageBox.Show(c);
            }
            MessageBox.Show("done");
        }
    }
}
