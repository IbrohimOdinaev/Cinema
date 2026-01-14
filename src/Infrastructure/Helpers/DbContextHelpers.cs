using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Helpers;

public static class DbContextExtensions
{
    public static T AddOrAttach<T>(this DbContext context, T entity, bool isNew = false)
      where T : class
    {
        var set = context.Set<T>();

        var tracked = set.Local.FirstOrDefault(e => GetId(e).Equals(GetId(entity)));

        if (tracked != null)
        {
            return tracked;
        }

        if (isNew)
        {
            set.Add(entity);
        }
        else
        {
            set.Attach(entity);
        }
        return entity;

        static object GetId(T e) =>
          e.GetType().GetProperty("Id")!.GetValue(e)
          ?? throw new InvalidOperationException("Entity must have id");
    }
}
