using BusinessLogic.Repositories;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Repositories
{
    public class RoseTattooRepo : GenericRepository<RoseTattooType>, IRoseTattooRepo
    {
        public RoseTattooRepo(RoseTattooShop2023DBContext context) : base(context)
        {
        }
    }
}
