using BusinessLogic.Repositories;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Repositories
{
    public class MemberAccountRepo : GenericRepository<MemberAccount>, IMemberAccountRepo
    {
        public MemberAccountRepo(RoseTattooShop2023DBContext context) : base(context)
        {

        }
        public MemberAccount? Login(string email, string password)
        {
            var entity = GetAll().FirstOrDefault(o
                => o.MemberEmail.ToLower().Equals(email.ToLower())
                && o.MemberPassword.Equals(password));

            return entity;
        }
    }
}
