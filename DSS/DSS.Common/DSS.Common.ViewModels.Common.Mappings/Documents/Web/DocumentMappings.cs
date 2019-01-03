using System;
using System.Collections.Generic;
using AutoMapper;
using DSS.Common.ViewModels.Documents;
using DSS.Common.ViewModels.Mappings.CommonMapUtilities;
using DSS.Common.ViewModels.Mappings.Documents.Utilities.TypeConverters;
using DSS.Common.ViewModels.Mappings.Documents.Utilities.ValueResolvers;
using DSS.Data.Model.Entities;

namespace DSS.Common.ViewModels.Mappings.Documents.Web
{
    public static class DocumentMappings
    {
        public static void Map()
        {
            // Create mappings from the entitiy models to view models
            FromEntitiy();

            // Create mappings from view models to entities
            ToEntity();
        }

        /// <summary>
        /// Contains mappings from domain entities to view models
        /// </summary>
        private static void FromEntitiy()
        {
            // The mapping definition between the document entitiy and the display 
            // document view modek
            Mapper.CreateMap<Document, DisplayDocumentViewModel>()
            .ForMember(x => x.Categories, opt => opt.ResolveUsing<CategoryNamesFromEntities>())
            .ForMember(x => x.Keywords, opt => opt.ResolveUsing<KeywordNamesFromEntities>());

            Mapper.CreateMap<Document, AddDocumentViewModel>();

            // The mapping for the grid model
            Mapper.CreateMap<Document, AdminDocumentGridViewModel>().ForMember(x => x.IsIndexed_Descriptive, opt => { opt.MapFrom(d => d.IsIndexed); opt.AddFormatter(new BooleanToYesNoFormatter()); });

            // The mapping from a single document to the stats for the document view model.
            Mapper.CreateMap<Document, DocumentStatsViewModel>()
                .ForMember(x => x.TotalDownloads, opt => opt.MapFrom(d => d.DocumentDownloads == null ? 0 : d.DocumentDownloads.Count))
                .ForMember(x => x.TotalDownvotes, opt => opt.MapFrom(d => d.DocumentDownvotes == null ? 0 : d.DocumentDownvotes.Count))
                .ForMember(x => x.TotalUpvotes, opt => opt.MapFrom(d => d.DocumentUpvotes == null ? 0 : d.DocumentUpvotes.Count))
                .ForMember(x => x.UploadDateString, opt =>
                                                        {
                                                            opt.MapFrom(d => d.DateUploaded);
                                                            opt.AddFormatter(new DatetimeToStringFormatter());
                                                        })
                .ForMember(x => x.UploadedUsername, opt => opt.ResolveUsing<UsernameStringFromUserEntitiy>());
        }

        /// <summary>
        /// Create mappings from view models to entities
        /// </summary>
        private static void ToEntity()
        {
            // Will map a AddDocumentView Model to a Document
            Mapper.CreateMap<AddDocumentViewModel, Document>()
                .ForMember(doc => doc.Categories, opt => opt.ResolveUsing<CategoryListFromGuidListResolver>())
                .ForMember(doc => doc.Keywords, opt => opt.ResolveUsing<KeywordsFromStringResolver>());

            // We want a resolver that maps a List of Add Document View Models
            // to the total list of unique keywords in all those AddDocumentViewModels
            // Bassically for a collecton of uploaded documents get all the unique keyword objects
            Mapper.CreateMap<List<AddDocumentViewModel>, List<Keyword>>().ConvertUsing<UniqueKeywordsFromNewDocumentsTypeConverter>();

            // We want a resolver that will extract a list of selected unique categories from a single
            // AddDocumentViewModel list
            Mapper.CreateMap<List<AddDocumentViewModel>, List<Category>>().ConvertUsing<UniqueCategoryFromNewDocumentsTypeConverter>();

        }
    }
}
