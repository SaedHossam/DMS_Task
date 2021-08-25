using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        public IEnumerable<Item> ItemsList();
        public IEnumerable<Item> AvalibleItems();
        public Item ItemData(int id);

    }
}
