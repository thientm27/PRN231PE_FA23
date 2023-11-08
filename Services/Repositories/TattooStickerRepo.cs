using BusinessLogic.Repositories;
using BusinessObjects.Models;


namespace BusinessObjects.Repositories
{
    public class TattooStickerRepo : GenericRepository<TattooSticker>, ITattooStickerRepo
    {
        public TattooStickerRepo(RoseTattooShop2023DBContext context) : base(context)
        {
        }
    }
}
