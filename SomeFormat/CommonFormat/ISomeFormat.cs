using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonFormat;
using System.Collections;

namespace SomeFormat
{
    /// <summary>
    /// Specific file format
    /// </summary>
    public interface ISomeFormat : IFormat, IEnumerable<ISomeFormat>
    {
        /// <summary>
        /// Field "Date"
        /// </summary>
        String Date { get; set; }

        /// <summary>
        /// Field "BrandName"
        /// </summary>
        String BrandName { get; set; }

        /// <summary>
        /// Field "Price"
        /// </summary>
        int Price { get; set; }
    }
}
