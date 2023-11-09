using BusinessObjects.Models;
using DAOs.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class UnitOfWork
    {
        private RoseTattooShop2023DBContext _context;

        private GenericDao<MemberAccount> memberAccountDao;
        private GenericDao<RoseTattooType> roseTattooDao;
        private GenericDao<TattooSticker> tattooStickerDao;

        public UnitOfWork(RoseTattooShop2023DBContext context)
        {
            _context = context;
        }

        public GenericDao<MemberAccount> MemberAccountDao => memberAccountDao ??= new MemberAccountDao(_context);
        public GenericDao<RoseTattooType> RoseTattooDao => roseTattooDao ??= new RoseTattooDao(_context);
        public GenericDao<TattooSticker> TattooStickerDao => tattooStickerDao ??= new TattooStickerDao(_context);
      

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
