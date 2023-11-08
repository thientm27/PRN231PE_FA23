using BusinessLogic.Repositories;
using BusinessObjects.Models;
using Microsoft.EntityFrameworkCore;


namespace BusinessObjects.Repositories
{
    public class TattooStickerRepo : GenericRepository<TattooSticker>, ITattooStickerRepo
    {
        public TattooStickerRepo(RoseTattooShop2023DBContext context) : base(context)
        {
        }
        public override TattooSticker? GetById(int id)
        {
            return GetAll().FirstOrDefault(o => o.TattooStickerId == id);
        }

        public bool AddNew(TattooSticker item)
        {
            item.TattooStickerId =  GetAll().Max(o => o.TattooStickerId) + 1;
            Add(item);
            var result = Save();
            if (result > 0) return true;
            return false;
        }
        public bool DeleteById(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
                var result = Save();
                if (result > 0) return true;
            }
            return false;
        }


        public List<TattooSticker> GetByParameter(string searchString, DateTime date)
        {
            var entities = GetAll();
            entities = entities.Where(o 
                => o.TattooStickerDescription.ToLower().Contains(searchString.ToLower())
                || o.ImportDate == date
                ).ToList();
            return entities;
        }
        public bool UpdateEntity(TattooSticker entity)
        {
            var m_update = GetById(entity.TattooStickerId);
            if (m_update != null)
            {
                _context.Entry(m_update).State = EntityState.Detached; // Detach the existing entity
                _context.Update(entity);
                var result = Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
