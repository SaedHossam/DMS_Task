using DAL.Repositories.Interfaces;

namespace DAL
{
    public interface IUnitOfWork
    {
        IItemRepository Item { get; }
        IOrderRepository Order { get; }
        IOrderItemsRepository OrderItems { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IShoppingCartItemRepository ShoppingCartItem { get; }
        ICustomerRepository Customer { get; }
        IUnitOfMeasureRepository UnitOfMeasure { get; }
        int SaveChanges();
    }
}
