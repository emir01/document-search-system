using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace DSS.Common.ViewModels.Mappings.CommonMapUtilities
{
    /// <summary>
    /// Auto Mapper formatter that maps Generic Collections to Collection Count Values
    /// </summary>
    public class CollectionToCountFormatter:ValueFormatter<IEnumerable<object>>
    {
        #region Overrides of ValueFormatter<IEnumerable<object>>

        protected override string FormatValueCore(IEnumerable<object> value)
        {
            if (value == null)
            {
                return "0";
            }
            else
            {
                return value.Count().ToString();
            }
        }

        #endregion
    }
}
