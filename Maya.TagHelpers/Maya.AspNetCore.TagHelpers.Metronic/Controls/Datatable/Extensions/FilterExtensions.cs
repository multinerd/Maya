using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using Maya.AspNetCore.TagHelpers.Core.Enumerations;
using Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Models;

namespace Maya.AspNetCore.TagHelpers.Metronic.Controls.Datatable.Extensions
{
    public static class FilterExtensions
    {
        public static IQueryable<TEntity> Sort<TEntity>(
            this IQueryable<TEntity> query,
            DatatableAjaxRequest request)
        {
            if (!string.IsNullOrEmpty(request.Sorting.Field))
                query = request.Sorting.Direction == SortDirection.Ascending
                    ? query.OrderBy(request.Sorting.Field)
                    : query.OrderBy(request.Sorting.Field + " descending");
            return query;
        }

        public static IQueryable<TEntity> Select<TEntity>(
            this IQueryable<TEntity> query,
            DatatableAjaxRequest request)
        {
            if (request.Pagination.PerPage > 0)
            {
                query = Queryable.Skip(query, (request.Pagination.Page - 1) * request.Pagination.PerPage);
                query = Queryable.Take(query, request.Pagination.PerPage);
            }

            return query;
        }

        public static IQueryable<TEntity> Search<TEntity>(
            this IQueryable<TEntity> query,
            DatatableAjaxRequest request)
        {
            if (!string.IsNullOrEmpty(request.GeneralSearch) &&
                request.Columns.Any(c => c.IsSearchable))
            {
                var stringBuilder = new StringBuilder();
                var list = request.Columns.Where(c =>
                {
                    if (c.IsSearchable)
                        return !string.IsNullOrEmpty(c.Field);
                    return false;
                }).ToList();
                foreach (var column in list)
                {
                    if (!column.Equals(list.Last()))
                        stringBuilder.Append(column.Field + ".ToString().ToLower().Contains(\"" +
                                             request.GeneralSearch.ToLower() + "\") OR ");
                    else
                        stringBuilder.Append(column.Field + ".ToString().ToLower().Contains(\"" +
                                             request.GeneralSearch.ToLower() + "\")");
                }

                if (stringBuilder.Length > 0)
                    query = query.Where(stringBuilder.ToString());
            }

            return query;
        }
    }
}