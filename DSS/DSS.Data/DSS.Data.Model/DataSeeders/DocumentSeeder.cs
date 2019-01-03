using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using DSS.Common.Infrastructure.Lists;
using DSS.Data.Model.Context;
using DSS.Data.Model.Entities;
using DSS.Data.Model.SeedDataObjects;

namespace DSS.Data.Model.DataSeeders
{
    /// <summary>
    /// A document seeder used to create test document data during the seed database process
    /// </summary>
    public class DocumentSeeder
    {
        #region Seeding Config

        private static int _minCategories = 2;
        private static int _maxCategories = 5;

        private static int _minKeywords = 3;
        private static int _maxKeywords = 6;

        #endregion

        /// <summary>
        /// Add a collection of test documents that are missing files on disk and are 
        /// used to only test the document interface and file download issues
        /// 
        /// Keywords and categories for document will be randomly selected.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="categories">The list of currently seeded categories</param>
        /// <param name="keywords">List of currently seeded keywords</param>
        public static void AddTestDocuments(DsContext context, List<Category> categories, List<Keyword> keywords)
        {
            var adminUser = context.Users.FirstOrDefault(user => user.Username == SeededUsernames.AdminUser);

            if (adminUser == null)
            {
                throw new NullReferenceException(
                    "Null pre-seeded admin user. Check Seeds and Configuratio before trying to seed Documents");
            }

            // pre-clean the list of categories and keywords

            if (categories == null) categories = new List<Category>();
            if (keywords == null) keywords = new List<Keyword>();


            AddTestDocument(context, adminUser, "Географија", "Дипломски проект за MVC апликација за изучување на географија", "Емир Османоски", categories, keywords);
            AddTestDocument(context, adminUser, "Тутор за изучување јазикот за глувонеми", "3d апликација за интерактивно изучување на македонскиот јазик на глувонемите.", "Марјан Ѓуроски", categories, keywords);
            AddTestDocument(context, adminUser, "Податочно Рударство, Уринарни Инфекции", "Опис на процес, рударење и резултат", "Емир Османоски", categories, keywords);
            AddTestDocument(context, adminUser, "Спецификација за мобилно банкарство", "Опис на систем за мобилно банкарство. Документот се состои од целокупни барања и специфицација за системот", "Група студенти", categories, keywords);
        }

        /// <summary>
        /// Add a single test document uploaded by the provided user and containing
        /// the provided properties. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="documentTitle"></param>
        /// <param name="documentDescription"></param>
        /// <param name="authorName"></param>
        /// <param name="keywords"></param>
        /// <param name="isIndexed">Indicate if the document is indexed and is downloadable. Optional param set to true </param>
        /// <param name="categories"></param>
        public static void AddTestDocument(DsContext context, User user, string documentTitle, string documentDescription, string authorName, List<Category> categories = null, List<Keyword> keywords = null, bool isIndexed = true)
        {
            // Use current date as a mock for all the required date fields
            var currentDate = DateTime.Now;

            // only add the document if there is no such document inserted before based on the document title
            if (context.Documents.FirstOrDefault(x => x.Title == documentTitle) == null)
            {
                var document = new Document()
                {
                    AuthorName = authorName,
                    Description = documentDescription,
                    Title = documentTitle,
                    User = user,
                    Path = "",
                    IsIndexed = isIndexed,
                    DateCreated = currentDate,
                    DateIndexed = currentDate,
                    DateUploaded = currentDate,

                    Categories = ListUtilities.GetSublist(categories, _minCategories, _maxCategories > categories.Count ? categories.Count : _maxCategories),
                    Keywords = ListUtilities.GetSublist(keywords, _minKeywords, _maxKeywords > keywords.Count ? keywords.Count : _maxKeywords),
                };

                context.Documents.AddOrUpdate(document);
            }
        }
    }
}