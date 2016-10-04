using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonFormat
{
    /// <summary>
    /// Common interface for any format file
    /// </summary>
    public interface IFormat
    {
       
        /// <summary>
        /// Tag implementing format by wildcard "[BuissnessDescription].[fileFormatShortName]"
        /// Expamle: "SOMEFORMAT.XML", "SOMEFORMAT.BIN"
        /// </summary>
        string Tag { get; }
    }
}
