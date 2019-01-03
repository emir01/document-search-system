using AutoMapper;
using DSS.Common.ViewModels.Keywords;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Keywords.Web
{
    public static class KeywordMappings
    {
        public static void Map()
        {
            Mapper.CreateMap<Keyword, DisplayKeywordViewModel>();
            Mapper.CreateMap<DisplayKeywordViewModel, Keyword>();
        }
    }
}
