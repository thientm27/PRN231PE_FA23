using BusinessObjects.Models;
using Entities.IRepositories;

namespace BusinessObjects
{
    public interface ITattooStickerRepo : IGenericRepository<TattooSticker>
    {
        public List<TattooSticker> GetByParameter(string searchString, DateTime date);
        public bool AddNew(TattooSticker item);
        public bool DeleteById(int id);
        public bool UpdateEntity(TattooSticker entity);

    }
}