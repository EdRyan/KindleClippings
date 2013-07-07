using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleClippings
{
    public class Clipping
    {
        public string BookName { get; set; }
        public string Author { get; set; }
        public ClippingTypeEnum ClippingType { get; set; }
        public string Page { get; set; }
        public string Location { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; }

        public int? BeginningPage
        {
            get { return GetBeginningOfRange(Page); }
        }

        public int? BeginningLocation
        {
            get { return GetBeginningOfRange(Location); }
        }

        private int? GetBeginningOfRange(string range)
        {
            if (String.IsNullOrEmpty(range)) return null;

            var hIndex = range.IndexOf('-');

            string first;

            first = (hIndex >= 0) ? range.Substring(0, hIndex) : range;

            return int.Parse(first);
        }
    }
}
