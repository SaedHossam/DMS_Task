using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private IItemRepository _item;

        private IOrderRepository _order;

        private IOrderItemsRepository _orderItems;

        private IShoppingCartRepository _shoppingCart;

        private IShoppingCartItemRepository _shoppingCartItem;

        private ICustomerRepository _customer;

        private IUnitOfMeasureRepository _unitOfMeasure;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IItemRepository Item => _item ??= new ItemRepository(_context);

        public IOrderRepository Order => _order ??= new OrderRepository(_context);

        public IOrderItemsRepository OrderItems => _orderItems ??= new OrdertemRepository(_context);

        public IShoppingCartRepository ShoppingCart => _shoppingCart ??= new ShoppingCartRepository(_context);

        public IShoppingCartItemRepository ShoppingCartItem => _shoppingCartItem ??= new ShoppingCartItemRepository(_context);

        public ICustomerRepository Customer => _customer ??= new CustomerRepository(_context);

        public IUnitOfMeasureRepository UnitOfMeasure => _unitOfMeasure ??= new UnitOfMeasureRepository(_context);



        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
