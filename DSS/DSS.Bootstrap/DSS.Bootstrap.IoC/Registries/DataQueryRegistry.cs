using DSS.Data.Model.Entities;
using DSS.Data.Query.DataTables;
using DSS.Data.Query.ParameterProcessors;
using DSS.Data.Query.ParameterProcessors.Interfaces;
using DSS.Data.Query.QueryProcessors;
using DSS.Data.Query.QueryProcessors.Interface;
using StructureMap.Configuration.DSL;

namespace DSS.Bootstrap.IoC.Registries
{
    /// <summary>
    /// IoC Registry for Data Query Functionality
    /// </summary>
    public class DataQueryRegistry : Registry
    {
        public DataQueryRegistry()
        {
            For<IDynamicFilterBuilder<string>>().Use<StringDynamicFilterBuilder>();

            RegisterDocumentDataQueryFunctionality();
        }

        private void RegisterDocumentDataQueryFunctionality()
        {
            For<IQueryProcessor<Document>>().Use<QueryProcessor<Document>>();

            For<IQueryParameterProcessor<JQueryDataTableParams, Document>>().Use
                <JQueryDtParameterProcessor<JQueryDataTableParams, Document>>();
        }
    }
}
