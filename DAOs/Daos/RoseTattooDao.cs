using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs.Daos
{
    public class RoseTattooDao : GenericDao<RoseTattooType>
    {
        public RoseTattooDao(RoseTattooShop2023DBContext context) : base(context)
        {
        }
    }
}
