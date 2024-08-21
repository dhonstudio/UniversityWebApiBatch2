using Application.Interfaces;

namespace Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _schoolContext;

        public UnitOfWork(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void SaveChanges()
        {
            _schoolContext.SaveChanges();
        }
    }
}
