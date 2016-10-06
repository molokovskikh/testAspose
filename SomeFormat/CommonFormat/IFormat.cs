using CommonFormat;

namespace SomeFormat
{
    /// <summary>
    /// Actions on file of specific format
    /// </summary>
    public interface IFormat<T> where T:IFormatRecord
    {
        /// <summary>
        /// Read data from file
        /// </summary>
        /// <param name="filename">Full path filename</param>
        void Read(string filename);

        /// <summary>
        /// Write data to file
        /// </summary>
        /// <param name="filename">Full path filename</param>
        void Write(string filename);

        /// <summary>
        /// Add new record format
        /// </summary>
        /// <param name="record">Position added record</param>
        long Add(T record);

        /// <summary>
        /// Modify existing record format
        /// </summary>
        /// <param name="record">Existing record</param>
        /// <param name="positionRecord">Position of record</param>
        void Modify(T record, int positionRecord);


        /// <summary>
        /// Get record specific format
        /// </summary>
        /// <param name="positionRecord"></param>
        /// <returns></returns>
        T Get(int positionRecord);

        /// <summary>
        /// Count of records
        /// </summary>
        /// <returns></returns>
        long Count();


        /// <summary>
        /// Convert to any specified format
        /// </summary>
        /// <typeparam name="T">Type of target format</typeparam>
        /// <returns></returns>
         R Convert<R>() where R: IFormat<T>;


         /// <summary>
         /// Tag implementing format by wildcard "[BusinessDescription].[fileFormatShortName]"
         /// Expamle: "SOMEFORMAT.XML", "SOMEFORMAT.BIN"
         /// </summary>
         string Tag { get; }
    }
}
