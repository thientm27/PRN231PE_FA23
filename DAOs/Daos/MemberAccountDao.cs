using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs.Daos
{
    public class MemberAccountDao : GenericDao<MemberAccount>
    {
        public MemberAccountDao(RoseTattooShop2023DBContext context) : base(context)
        {
        }
    }
}
