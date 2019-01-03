using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StackExchange.Profiling.SqlFormatters
{
    /// <summary>
    /// Formats SQL server queries with a DECLARE up top for parameter values
    /// </summary>
    public class SqlServerFormatter : ISqlFormatter
    {
        private static readonly Dictionary<DbType, Func<SqlTimingParameter, string>> ParamTranslator;
        private static readonly string[] DontQuote = new[] { "Int16", "Int32", "Int64", "Boolean", "Byte[]" };

        private static Func<SqlTimingParameter, string> GetWithLenFormatter(string native)
        {
            var capture = native;
            return p =>
                {
                    if (p.Size < 1)
                    {
                        return capture;
                    }
                    return capture + "(" + (p.Size > 8000 ? "max" : p.Size.ToString(CultureInfo.InvariantCulture)) + ")";
                };
        }

        /// <summary>
        /// Initialises static members of the <see cref="SqlServerFormatter"/> class.
        /// </summary>
        static SqlServerFormatter()
        {   
            ParamTranslator = new Dictionary<DbType, Func<SqlTimingParameter, string>>
            {
                { DbType.AnsiString, GetWithLenFormatter("varchar") },
                { DbType.String, GetWithLenFormatter("nvarchar") },
                { DbType.AnsiStringFixedLength, GetWithLenFormatter("char") },
                { DbType.StringFixedLength, GetWithLenFormatter("nchar") },
                { DbType.Byte, p => "tinyint" },
                { DbType.Int16, p => "smallint" },
                { DbType.Int32, p => "int" },
                { DbType.Int64, p => "bigint" },
                { DbType.DateTime, p => "datetime" },
                { DbType.Guid, p => "uniqueidentifier" },
                { DbType.Boolean, p => "bit" },
                { DbType.Binary, GetWithLenFormatter("varbinary") },
            };
        }

        /// <summary>
        /// Formats the SQL in a SQL-Server friendly way, with DECLARE statements for the parameters up top.
        /// </summary>
        public string FormatSql(string commandText, List<SqlTimingParameter> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return commandText;
            }

            var buffer = new StringBuilder("DECLARE ");
            var first = true;

            foreach (var p in parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    buffer.AppendLine(",").Append(new string(' ', 8));
                }

                DbType parsed;
                string resolvedType = null;
                if (!Enum.TryParse(p.DbType, out parsed))
                {
                    resolvedType = p.DbType;
                }
                
                if (resolvedType == null)
                {
                    Func<SqlTimingParameter, string> translator; 
                    if (ParamTranslator.TryGetValue(parsed, out translator))
                    {
                        resolvedType = translator(p);
                    }
                    resolvedType = resolvedType ?? p.DbType;
                }

                var niceName = p.Name;
                if (!niceName.StartsWith("@"))
                {
                    niceName = "@" + niceName;
                }

                buffer.Append(niceName).Append(" ").Append(resolvedType).Append(" = ").Append(PrepareValue(p));
            }

            return buffer
                .Append(";")
                .AppendLine()
                .AppendLine()
                .Append(commandText)
                .ToString();
        }

        private string PrepareValue(SqlTimingParameter parameter)
        {
            if (parameter.Value == null)
            {
                return "null";
            }

            if (DontQuote.Contains(parameter.DbType))
            {
                if (parameter.DbType == "Boolean")
                {
                    return parameter.Value == "True" ? "1" : "0";
                }

                return parameter.Value;
            }

            var prefix = string.Empty;
            if (parameter.DbType == "String" || parameter.DbType == "StringFixedLength")
            {
                prefix = "N";
            }

            return prefix + "'" + parameter.Value.Replace("'", "''") + "'";
        }
    }
}
