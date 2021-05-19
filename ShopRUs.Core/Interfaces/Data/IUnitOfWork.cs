namespace ShopRUs.Core.Interfaces.Data
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IDiscountRepository DiscountRepository { get; }
        void Commit();
    }
}
