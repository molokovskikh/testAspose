using CommonFormat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SomeFormat
{
    /// <summary>
    /// Actions on file of specific format
    /// </summary>
    public interface ISomeFormatActions
    {
        /// <summary>
        /// Read data from file
        /// </summary>
        /// <param name="filename">Full path filename</param>
        void Read(String filename);

        /// <summary>
        /// Write data to file
        /// </summary>
        /// <param name="filename">Full path filename</param>
        void Write(String filename);

        /// <summary>
        /// Add new record specific format
        /// </summary>
        /// <param name="record">New Record</param>
        ISomeFormat Add(ISomeFormat record);

        /// <summary>
        /// Modify existing record specific format
        /// </summary>
        /// <param name="record">Existing record</param>
        /// <param name="positionRecord">Position of record</param>
        void Modify(ISomeFormat record, int positionRecord);


    }
}
