/// <summary>Defines the contract all derived repository objects will fulfill</summary>
/// Note that not all repositories support all forms of reading and writing and so
/// the default implementations of these methods simply throw.
///
/// The types <strong>TEntity</strong> and <strong>TEntityKey</strong> define the
/// entity type and its primary key.
namespace EntityFrameworkBasics.Data;

public interface IRepository<TEntity, TEntityKey>
{
    public void Create(TEntity entity)
    {
        throw new NotSupportedException($"Creating new instances of type {typeof(TEntity)} not supported.");
    }

    public TEntity Read(TEntityKey key)
    {
        throw new NotSupportedException($"Reading instances of type {typeof(TEntity)} not supported.");
    }

    public void Update(TEntity entity)
    {
        throw new NotSupportedException($"Updating instances of type {typeof(TEntity)} not supported.");
    }

    public void Delete(TEntityKey key)
    {
        throw new NotSupportedException($"Deleting instances of type {typeof(TEntity)} not supported.");
    }

    public void SaveChanges();

    ///<summary>Detects changes to relevant DbContexts and returns the tracker long view</summary>
    public string DetectChanges();
}