using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs.Daos
{
    public class TattooStickerDao : GenericDao<TattooSticker>
    {
        public TattooStickerDao(RoseTattooShop2023DBContext context) : base(context)
        {
        }
    }
}
