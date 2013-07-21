using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using KindleClippings;

namespace KindleClippingsGUI
{
    internal static class Common
    {
        private static string ResourceNamespace = "KindleClippingsGUI.Resources.";
        public static Icon PersonIcon = Icon.FromResource(ResourceNamespace + "person.ico");
        public static Icon BookIcon = Icon.FromResource(ResourceNamespace + "book.ico");
        public static Icon HighlighterIcon = Icon.FromResource(ResourceNamespace + "highlighter.ico");
        public static Icon NoteIcon = Icon.FromResource(ResourceNamespace + "note.ico");
        public static Icon BookmarkIcon = Icon.FromResource(ResourceNamespace + "bookmark.ico");
        public static Icon TextIcon = Icon.FromResource(ResourceNamespace + "text.ico");

        public const string DefaultKindleVolumeLabel = "Kindle";
        public const string MyClippingsRelativePath = @"documents\My Clippings.txt";

        /// <summary>
        /// Gets a human-readable description of a clipping type from the ClippingTypeEnum value
        /// </summary>
        /// <param name="type">ClippingTypeEnum value</param>
        /// <returns>Human-readable description</returns>
        public static string GetClippingType(ClippingTypeEnum type)
        {
            switch (type)
            {
                case ClippingTypeEnum.Highlight:
                    return "Highlight";
                case ClippingTypeEnum.Note:
                    return "Note";
                case ClippingTypeEnum.Bookmark:
                    return "Bookmark";
                default:
                    return "Unknown Clipping Type";
            }
        }

        /// <summary>
        /// Gets the corresponding icon for a clipping type from the ClippingTypeEnum value
        /// </summary>
        /// <param name="type">ClippingTypeEnum value</param>
        /// <returns>Corresponding icon</returns>
        public static Icon GetClippingTypeIcon(ClippingTypeEnum type)
        {
            switch (type)
            {
                case ClippingTypeEnum.Bookmark:
                    return BookmarkIcon;
                case ClippingTypeEnum.Highlight:
                    return HighlighterIcon;
                case ClippingTypeEnum.Note:
                    return NoteIcon;
                default:
                    return TextIcon;
            }
        }

        /// <summary>
        /// Gets the removable drives currently connected to the system
        /// </summary>
        /// <returns>DriveInfo's for removable drives</returns>
        public static IEnumerable<DriveInfo> GetRemovableDrives()
        {
            return DriveInfo.GetDrives().Where(di => di.DriveType == DriveType.Removable);
        }
    }
}
