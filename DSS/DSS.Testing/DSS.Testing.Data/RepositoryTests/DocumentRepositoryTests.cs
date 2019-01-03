using System;
using System.Linq;
using DSS.Data.Access.Interfaces;
using DSS.Data.Access.Repositories;
using DSS.Data.Model.Entities;
using DSS.Testing.Data.Bootstrap;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSS.Testing.Data.RepositoryTests
{
    [TestClass]
    public class DocumentRepositoryTests
    {
        #region Dependencies

        private static IRepository<Document> _documentRepository;

        #endregion

        public DocumentRepositoryTests()
        {

        }

        #region Initialization

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            TestIoCBootstrap.Bootstrap();

            _documentRepository = new DocumentRepository();
        }

        #endregion

        #region Document Search

        #endregion

        [TestMethod]
        public void DocumentSearch_NoFilterProperties_ShouldReturn_DocumentsWithFullData()
        {
            // Arrange

            // Act
            var documents = _documentRepository.ReadAllAsQueryable().ToList();

            // Assert
            Assert.IsTrue(documents != null && documents.Count > 0);

            // Make sure there is a document with any number of categories
            Assert.IsTrue(documents.Any(x => x.Categories.Count > 0), "No Categories in any of the documents");

            // Make sure there is a document  with any number of keywords
            Assert.IsTrue(documents.Any(x => x.Keywords.Count > 0), "No keywords in any of the documents");
        }

        #region Utilities



        #endregion
    }
}
