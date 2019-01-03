using AutoMapper;

namespace DSS.Common.ViewModels.Mappings.CommonMapUtilities
{
    /// <summary>
    /// Auto mapper formatter that maps a boolean value to Yes/No Values
    /// </summary>
    public class BooleanToYesNoFormatter:ValueFormatter<bool>
    {
        protected override string FormatValueCore(bool value)
        {
            if(value)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
    }
}
