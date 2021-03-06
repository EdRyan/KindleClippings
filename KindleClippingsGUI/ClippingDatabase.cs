﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KindleClippings;

namespace KindleClippingsGUI
{
    internal static class ClippingDatabase
    {
        private static Dictionary<int, Clipping> _clippings = new Dictionary<int, Clipping>();

        /// <summary>
        /// Adds a clipping to the database and returns a unique identifier for later retrieval
        /// </summary>
        /// <param name="clipping">The clipping to add to the database</param>
        /// <returns>The unique identifier corresponding to this clipping</returns>
        public static int AddClipping(Clipping clipping)
        {
            var id = _clippings.Keys.Count;
            
            _clippings.Add(id, clipping);

            return id;
        }

        /// <summary>
        /// Retrieves a clipping from the database
        /// </summary>
        /// <param name="id">Unique identifier (obtained as the return result of the AddClipping method)</param>
        /// <returns>The clipping associated with the identifier</returns>
        public static Clipping GetClipping(int id)
        {
            return _clippings[id];
        }
    }
}
