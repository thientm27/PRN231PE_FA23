using BusinessObjects.Models;
using Entities.IRepositories;

namespace BusinessObjects
{
    public interface IMemberAccountRepo : IGenericRepository<MemberAccount>
    {
        public MemberAccount? Login(string email, string password);
    }
}