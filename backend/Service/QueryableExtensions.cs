using System.Linq.Expressions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter, bool condition)
    {
        if (condition)
        {
            return query.Where(filter);
        }

        return query;
    }
}