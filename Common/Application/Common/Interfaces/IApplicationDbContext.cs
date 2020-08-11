namespace Application.Common.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IApplicationDbContext
    {
        //DbSet<TodoList> TodoLists { get; set; }

        //DbSet<TodoItem> TodoItems { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
