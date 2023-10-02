namespace LibraryIdentityProvider.EFCore
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChanges();
    }
}
