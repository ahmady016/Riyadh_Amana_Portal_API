using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using DB.Common;

namespace DB;

public class CRUDService : ICRUDService
{
    private readonly ApplicationContext _db;
    public CRUDService(ApplicationContext db)
    {
        _db = db;
    }

    #region Queries [Select]
    public T Find<T, TKey>(TKey id) where T : Entity<TKey>
    {
        return _db.Set<T>().Find(id);
    }
    public T GetOne<T>(Expression<Func<T, bool>> where) where T : class
    {
        return _db.Set<T>()
                    .AsNoTracking()
                    .Where(where)
                    .FirstOrDefault();
    }

    public T GetOne<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class
    {
        var query = _db.Set<T>().AsNoTracking();
        foreach (var include in includes)
            query = query.Include(include);
        return query.Where(where).FirstOrDefault();
    }

    public T GetOne<T>(Expression<Func<T, bool>> where, params string[] includes) where T : class
    {
        var query = _db.Set<T>().AsNoTracking();
        foreach (var include in includes)
            query = query.Include(include);
        return query.Where(where).FirstOrDefault();
    }

    public List<T> GetAll<T, TKey>() where T : Entity<TKey>
    {
        return _db.Set<T>()
                    .AsNoTracking()
                    .ToList();
    }

    public List<T> GetList<T, TKey>(Expression<Func<T, bool>> where) where T : Entity<TKey>
    {
        return _db.Set<T>()
                    .AsNoTracking()
                    .Where(where)
                    .ToList();
    }

    public List<T> GetList<T>(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes) where T : class
    {
        var query = _db.Set<T>().AsNoTracking();
        foreach (var include in includes)
            query = query.Include(include);
        return query.Where(where).ToList();
    }

    public List<T> GetList<T>(Expression<Func<T, bool>> where, params string[] includes) where T : class
    {
        var query = _db.Set<T>().AsNoTracking();
        foreach (var include in includes)
            query = query.Include(include);
        return query.Where(where).ToList();
    }
    public PageResult<T> GetPage<T>(IQueryable<T> query, int pageSize = 20, int pageNumber = 1) where T : class
    {
        var count = query.Count();
        return new PageResult<T>
        {
            PageItems = query.Skip(pageSize * (pageNumber - 1))
                            .Take(pageSize)
                            .ToList(),
            TotalItems = count,
            TotalPages = (int)Math.Ceiling((decimal)count/pageSize),
        };
    }
    public int Count<T>() where T : class
    {
        return _db.Set<T>().Count();
    }

    public int Count<T>(Expression<Func<T, bool>> where) where T : class
    {
        return _db.Set<T>().Count(where);
    }
    public IQueryable<T> GetQuery<T>() where T : class
    {
        return _db.Set<T>().AsNoTracking();
    }

    public IQueryable<T> GetQuery<T>(Expression<Func<T, bool>> where) where T : class
    {
        return _db.Set<T>().AsNoTracking().Where(where);
    }
    #endregion

        
    #region Commands [Add-Update-Delete]
    public T Add<T, Tkey>(T item) where T : Entity<Tkey>
    {
        return _db.Set<T>().Add(item).Entity;
    }
    public void AddRange<T, Tkey>(List<T> range) where T : Entity<Tkey>
    {
        _db.Set<T>().AddRange(range);
    }
    public List<T> AddAndGetRange<T, Tkey>(List<T> range) where T : Entity<Tkey>
    {
        return range.Select(obj => _db.Set<T>().Add(obj).Entity)
                    .ToList();
    }
    public T Update<T, TKey>(T item) where T : Entity<TKey>
    {
        item.UpdatedBy = "app_dev";
        item.UpdatedAt = DateTime.Now;
        _db.Entry(item).State = EntityState.Modified;
        return item;
    }
    public void UpdateRange<T, TKey>(List<T> range) where T : Entity<TKey>
    {
        range.ForEach(item =>
        {
            item.UpdatedBy = "app_dev";
            item.UpdatedAt = DateTime.UtcNow;
            _db.Entry(item).State = EntityState.Modified;
        });
    }
    public List<T> UpdateAndGetRange<T, TKey>(List<T> range) where T : Entity<TKey>
    {
        range.ForEach(item =>
        {
            item.UpdatedBy = "app_dev";
            item.UpdatedAt = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
        });
        return range;
    }

    public void Activate<T, TKey>(T item) where T : Entity<TKey>
    {
        item.IsActive = true;
        item.ActivatedBy = "app_dev";
        item.ActivatedAt = DateTime.Now;
        _db.Entry(item).State = EntityState.Modified;
    }

    public void ActivateRange<T, TKey>(List<T> range) where T : Entity<TKey>
    {
        range.ForEach(item =>
        {
            item.IsActive = true;
            item.ActivatedBy = "app_dev";
            item.ActivatedAt = DateTime.Now;
            _db.Entry(item).State = EntityState.Modified;
        });
    }

    public void Delete<T>(T item) where T : class
    {
        _db.Set<T>().Remove(item);
    }

    public void DeleteRange<T>(List<T> range) where T : class
    {
        _db.Set<T>().RemoveRange(range);
    }
    public bool GetOneAndDelete<T>(Expression<Func<T, bool>> where) where T : class
    {
        var set = _db.Set<T>();
        T item = set.Where(where).FirstOrDefault();
        if (item != null)
        {
            set.Remove(item);
            return true;
        }
        return false;
    }
    public bool GetListAndDelete<T>(Expression<Func<T, bool>> where) where T : class
    {
        var set = _db.Set<T>();
        var items = set.Where(where).ToList();
        if (items.Count != 0)
        {
            set.RemoveRange(items);
            return true;
        }
        return false;
    }
    public void SoftDelete<T, TKey>(T item) where T : Entity<TKey>
    {
        item.IsDeleted = true;
        item.DeletedBy = "app_dev";
        item.DeletedAt = DateTime.Now.AddHours(3);
        _db.Entry(item).State = EntityState.Modified;
    }

    public void SoftDeleteRange<T, TKey>(List<T> range) where T : Entity<TKey>
    {
        range.ForEach(item =>
        {
            item.IsDeleted = true;
            item.DeletedBy = "app_dev";
            item.DeletedAt = DateTime.Now.AddHours(3);
            _db.Entry(item).State = EntityState.Modified;
        });
    }
    public bool GetOneAndSoftDelete<T, TKey>(Expression<Func<T, bool>> where) where T : Entity<TKey>
    {
        var item = _db.Set<T>().Where(where).FirstOrDefault();
        if (item != null)
        {
            SoftDelete<T, TKey>(item);
            return true;
        }
        return false;
    }
    public bool GetListAndSoftDelete<T, TKey>(Expression<Func<T, bool>> where) where T : Entity<TKey>
    {
        var items = _db.Set<T>().Where(where).ToList();
        if (items.Count != 0)
        {
            SoftDeleteRange<T, TKey>(items);
            return true;
        }
        return false;
    }
    #endregion


    #region UnitOfWork
    public void Attach<T>(T entity) where T : class
    {
        _db.Set<T>().Attach(entity);
    }

    public void SetState<T>(T entity, string state) where T : class
    {
        switch (state)
        {
            case "Added":
                _db.Entry(entity).State = EntityState.Added;
                break;
            case "Modified":
                _db.Entry(entity).State = EntityState.Modified;
                break;
            case "Deleted":
                _db.Entry(entity).State = EntityState.Deleted;
                break;
            default:
                _db.Entry(entity).State = EntityState.Unchanged;
                break;
        }
    }
    public int SaveChanges()
    {
        return _db.SaveChanges();
    }
    #endregion

}
