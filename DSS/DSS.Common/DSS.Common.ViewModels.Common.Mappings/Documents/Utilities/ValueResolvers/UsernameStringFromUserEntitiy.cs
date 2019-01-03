using AutoMapper;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers
{
    /// <summary>
    /// Map a Documents Upload User, username to a string property
    /// </summary>
    public class UsernameStringFromUserEntitiy : ValueResolver<Document, string>
    {
        #region Overrides of ValueResolver<Document,string>

        protected override string ResolveCore(Document source)
        {
            if (source.User != null)
            {
                return source.User.Username;
            }
            else
            {
                return "Missing upload user";
            }
        }

        #endregion
    }

}