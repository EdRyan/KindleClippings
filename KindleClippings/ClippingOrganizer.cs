using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleClippings
{
    public static class ClippingOrganizer
    {
        public static IDictionary<string, Author> GroupClippingsByAuthorAndBook(IEnumerable<Clipping> clippings)
        {
            var authors = new Dictionary<string, Author>();

            foreach (var clipping in clippings)
            {
                var authorName = clipping.Author;
                if (!authors.ContainsKey(authorName)) authors.Add(authorName, new Author { Name = authorName });
                var author = authors[authorName];

                var bookName = clipping.BookName;
                if (!author.Books.ContainsKey(bookName)) author.Books.Add(bookName, new Book { Name = bookName, Author = author });
                var book = author.Books[bookName];

                book.Clippings.Add(clipping);
            }

            return authors;
        }
    }
}
