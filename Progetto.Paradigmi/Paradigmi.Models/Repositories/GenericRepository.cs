using Microsoft.EntityFrameworkCore;
using Paradigmi.Models.Context;

namespace Paradigmi.Models.Repositories;

public abstract class GenericRepository<T> where T : class
{
    protected MyDbContext _context;

    public GenericRepository(MyDbContext context)
    {
        _context = context;
    }

    public void Aggiungi(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Modifica(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public T? Ottieni(object id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Elimina(T entity)
    {
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}