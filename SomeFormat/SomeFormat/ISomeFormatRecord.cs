using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonFormat;
using System.Collections;
using System.Runtime.Serialization;

namespace SomeFormat
{
    /// <summary>
    /// Specific file format
    /// </summary>    
    public interface ISomeFormatRecord: IFormatRecord
    {
        /// <summary>
        /// Field "Date"
        /// </summary>
        string Date { get; set; }

        /// <summary>
        /// Field "BrandName"
        /// </summary>
        string BrandName { get; set; }

        /// <summary>
        /// Field "Price"
        /// </summary>
        int Price { get; set; }
    }
}
