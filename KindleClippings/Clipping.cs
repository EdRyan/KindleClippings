using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleClippings
{
    class Clipping
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public ClippingTypeEnum ClippingType { get; set; }
        public string Page { get; set; }
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }
    }
}
