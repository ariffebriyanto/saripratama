using IFA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IFA.Api.Utils
{
    public class SortingExtension
    {
        public static string SortExpressionBuilder(List<SortDescription> sorts)
        {
            string expression = null;
            int count = 0;

            sorts.ForEach((sort) =>
            {
                expression = count == 0 ? (sort.field + " " + sort.dir) : expression + ", " + sort.field + " " + sort.dir;
                count++;
            });

            return expression;
        }
    }
}
