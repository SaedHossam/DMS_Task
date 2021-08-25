using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(ApplicationDbContext context) : base(context)
        { }

        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public IEnumerable<Item> ItemsList()
        {
            var items = _appContext.Items.Include(i => i.UnitOfMeasure).Where(i => i.IsVisible == true);
            return items;
        }

        public Item ItemData(int id)
        {
            return _appContext.Items.Include(i => i.UnitOfMeasure).Where(i => i.Id == id).FirstOrDefault();
        }

        public IEnumerable<Item> AvalibleItems()
        {
            return ItemsList().Where(i => i.AvalibleQuantity > 0);
        }
    }
}
