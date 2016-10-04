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
    public interface ISomeFormatActions: IFormatActions<ISomeFormat>
    {
    }
}
