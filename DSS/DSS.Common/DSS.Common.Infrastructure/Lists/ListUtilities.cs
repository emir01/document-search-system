using System;
using System.Collections.Generic;
using System.Linq;

namespace DSS.Common.Infrastructure.Lists
{
    /// <summary>
    /// Contains a collection of list utilities used througout the code-base
    /// </summary>
    public static class ListUtilities
    {
        /// <summary>
        /// Return a random sublist of elements from the provided list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="minElements"></param>
        /// <param name="maxElements"></param>
        /// <returns></returns>
        public static List<T> GetSublist<T>(List<T> list, int minElements, int maxElements)
        {
            var rand = new Random(DateTime.Now.Millisecond);

            if (maxElements > list.Count)
            {
                throw new ArgumentException("Max elements is bigger than list length");
            }

            var numberOfElements = rand.Next(minElements, maxElements);

            var randomSortTable = new Dictionary<double, T>();
            foreach (T someType in list)
                randomSortTable[rand.NextDouble()] = someType;

            return randomSortTable.OrderBy(KVP => KVP.Key).Take(numberOfElements).Select(KVP => KVP.Value).ToList();
        }
    }
}
