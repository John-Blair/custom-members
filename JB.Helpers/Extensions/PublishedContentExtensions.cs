namespace JB.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web.Models;

    public static class PublishedContentExtensions
    {

        public static string ToCsvNames(this IEnumerable<IPublishedContent> collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            // csv list with no leading or trailing comma e.g. A,B,C
            string csv = collection.Aggregate(",", (current, content) => current + $"{content.Name},").Trim(',');

            return csv.EmptyIfNull();
        }

        public static string ToCsvIds(this IEnumerable<IPublishedContent> collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            // csv list with no leading or trailing comma e.g. 1,2,3
            string csv = collection.Aggregate(",", (current, content) => current + $"{content.Id},").Trim(',');

            return csv.EmptyIfNull();
        }

        public static string ToWrappedCsvNames(this IEnumerable<IPublishedContent> collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            // csv list with leading and trailing comma e.g. ,A,B,C,
            // useful to find an exact match on A by searching for ,A, - avoids substring issues when searching.
            string csv = collection.Aggregate(",", (current, content) => current + $"{content.Name},");

            return csv.EmptyIfNull();
        }
        public static string ToWrappedCsvIds(this IEnumerable<IPublishedContent> collection)
        {
            if (collection == null)
            {
                return string.Empty;
            }
            // csv list with leading and trailing comma e.g. ,1,2,3,
            // useful to find an exact match on 1 by searching for ,1, - avoids substring issues when searching.
            string csv = collection.Aggregate(",", (current, content) => current + $"{content.Id},");

            return csv.EmptyIfNull();
        }


        public static IEnumerable<IEnumerable<Link>> Columns(this IEnumerable<Link> collection, string separator)
        {
            // Tuple is column starting index, and column length
            var columnList = new List<Tuple<int, int>>();
            int startIndex = 0;
            int length = 0;

            // Build the columns 
            foreach (var item in collection)
            {
                if (item.Name.StartsWith(separator,StringComparison.OrdinalIgnoreCase))
                {
                    if (length >0)
                    {
                        // Column has values.
                        columnList.Add(new Tuple<int, int>(startIndex, length));
                        
                    }
                    // Updates for next column
                    startIndex += length + 1; // skip over the separator item.
                    length = 0;
                }
                else
                {
                    // This item is a member of the current column - and is not a separator.
                    length++;
                }
               
            }

            if (length > 0)
            {
                // Process the last column as it has values.
                columnList.Add(new Tuple<int, int>(startIndex, length));
            }

            foreach (var column in columnList)
            {
                yield return collection.Skip(column.Item1).Take(column.Item2);
            }

        }
    }
}
