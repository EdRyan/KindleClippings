using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace KindleClippings
{
    public class MyClippingsParser
    {
        private const string ClippingSeparator = "==========";
        private const string Line1RegexPattern = @"^(.+?)(?: \(([^)]+?)\))?$";

        public IEnumerable<Clipping> Parse(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return Parse(stream);
            }
        }

        public IEnumerable<Clipping> Parse(Stream stream)
        {
            var clippings = new Collection<Clipping>();

            using (var sr = new StreamReader(stream))
            {
                int lineNumber = 0;
                string line = null;
                int clippingLineNumber = 0;
                Clipping clipping = new Clipping();

                try
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        lineNumber++;

                        if (line == ClippingSeparator)
                        {
                            clippings.Add(clipping);
                            clippingLineNumber = 0;
                            clipping = new Clipping();
                        }
                        else
                        {
                            clippingLineNumber++;
                        }

                        switch (clippingLineNumber)
                        {
                            case 1:
                                ParseLine1(line, clipping);
                                break;
                            case 2:
                                ParseLine2(line, clipping);
                                break;
                            case 4:
                                ParseLine4(line, clipping);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error encountered parsing line " + lineNumber + ": " + ex.Message, ex);
                }
            }

            return clippings;
        }

        private void ParseLine1(string line, Clipping clipping)
        {
            var match = Regex.Match(line, Line1RegexPattern);
            if (match.Success)
            {
                var bookName = match.Groups[1].Value.Trim();
                var author = match.Groups[2].Value.Trim();

                clipping.BookName = bookName;
                clipping.Author = author;
            }
            else
            {
                throw new Exception("Clipping Line 1 did not match regex pattern.");
            }
        }

        private void ParseLine2(string line, Clipping clipping)
        {
            var split = line.Split(' ');

            switch (split[2])
            {
                case "Highlight":
                    clipping.ClippingType = ClippingTypeEnum.Highlight;
                    break;
                case "Note":
                    clipping.ClippingType = ClippingTypeEnum.Note;
                    break;
            }

            var hasPageNumber = line.Contains(" on Page ");

            var dtIdx = 8;
            var locIdx = 4;

            if (hasPageNumber)
            {
                var pageNumber = split[5];
                clipping.Page = pageNumber;

                locIdx = 8;

                var hasLocation = line.Contains(" Location ");
                dtIdx = hasLocation ? 12 : 9;
            }

            
            var location = split[locIdx];

            var dateAddedString = String.Join(" ", split[dtIdx], split[dtIdx + 1], split[dtIdx + 2], split[dtIdx + 3], split[dtIdx + 4], split[dtIdx + 5]);

            var dateAdded = DateTime.Parse(dateAddedString);

            
            clipping.Location = location;
            clipping.DateAdded = dateAdded;
        }

        private void ParseLine4(string line, Clipping clipping)
        {
            clipping.Text = line.Trim();
        }
    }
}
