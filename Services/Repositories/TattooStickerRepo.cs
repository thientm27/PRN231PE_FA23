using BusinessLogic.Repositories;
using BusinessObjects.Models;


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

        //public List<TattooSticker?> GetByParameter(int groupId, DateTime minBirthday, DateTime maxBirthday)
        //{
        //    var entities = GetAll();
        //    if (entities.Any()) return entities;
        //    return null;
        //}

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
                Update(entity);
                var result = Save();
                if (result > 0) return true;
            }
            return false;
        }
    }
}
