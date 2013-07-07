using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KindleClippings
{
    public class Author
    {
        public string Name { get; set; }
        public Dictionary<string, Book> Books { get; set; }

        public Author()
        {
            Books = new Dictionary<string, Book>();
        }
    }
}
