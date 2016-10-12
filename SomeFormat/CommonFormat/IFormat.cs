using CommonFormat;

namespace CommonFormat
{

    /// <summary>
    /// Specific format
    /// </summary>
    public interface IFormat
    {
        /// <summary>
        /// Convert to any specified format
        /// </summary>
        /// <typeparam name="T">Type of target format</typeparam>
        /// <returns></returns>
        F Convert<F>() where F : IFormat;


        /// <summary>
        /// Tag implementing format by wildcard "[BusinessDescription].[fileFormatShortName]"
        /// Expamle: "SOMEFORMAT.XML", "SOMEFORMAT.BIN"
        /// </summary>
        string Tag { get; }
    }


    /// <summary>
    /// Actions on file of specific format
    /// </summary>
    public interface IFormat<in R, out R2>: IFormat
        where R:IFormatRecord
        where R2: IFormatRecord
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
        long Add(R record);

        /// <summary>
        /// Modify existing record format
        /// </summary>
        /// <param name="record">Existing record</param>
        /// <param name="positionRecord">Position of record</param>
        void Modify(R record, int positionRecord);


        /// <summary>
        /// Get record specific format
        /// </summary>
        /// <param name="positionRecord"></param>
        /// <returns></returns>
        R2 Get(int positionRecord);

        /// <summary>
        /// Count of records
        /// </summary>
        /// <returns></returns>
        long Count();      
    }


    /// <summary>
    /// Short declare Actions on file of specific format
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public interface IFormat<R> : IFormat<R, R>
      where R : IFormatRecord
    {
    }
}
