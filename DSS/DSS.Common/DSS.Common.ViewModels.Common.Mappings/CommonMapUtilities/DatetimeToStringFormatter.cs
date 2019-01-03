using System;
using AutoMapper;

namespace DSS.Common.ViewModels.Mappings.CommonMapUtilities
{
    /// <summary>
    /// Auto mapper formatter that maps a boolean value to Yes/No Values
    /// </summary>
    public class DatetimeToStringFormatter:ValueFormatter<DateTime>
    {
        #region Overrides of ValueFormatter<DateTime>

        protected override string FormatValueCore(DateTime value)
        {
            return value.ToString();
        }

        #endregion
    }
}
