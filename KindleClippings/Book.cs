using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace KindleClippings
{
    public class Book
    {
        public string Name { get; set; }
        public Author Author { get; set; }
        public ICollection<Clipping> Clippings { get; set; }

        public Book()
        {
            Clippings = new Collection<Clipping>();
        }
    }
}
