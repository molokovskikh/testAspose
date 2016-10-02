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
        /// Convert to any specified format
        /// </summary>
        /// <typeparam name="T">Type of target format</typeparam>
        /// <returns></returns>
        IFormat convert<T>() where T : IFormat;
    }
}
