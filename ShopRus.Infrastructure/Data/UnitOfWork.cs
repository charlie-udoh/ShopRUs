using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Infrastructure.Data.Repositories;
using System;

namespace ShopRUs.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _dbContext;
        private ICustomerRepository _customerRepository;
        private IInvoiceRepository _invoiceRepository;
        private IDiscountRepository _discountRepository;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (_customerRepository == null)
                    _customerRepository = new CustomerRepository(_dbContext);
                return _customerRepository;
            }
        }

        public IInvoiceRepository InvoiceRepository
        {
            get
            {
                if (_invoiceRepository == null)
                    _invoiceRepository = new InvoiceRepository(_dbContext);
                return _invoiceRepository;
            }
        }

        public IDiscountRepository DiscountRepository
        {
            get
            {
                if (_discountRepository == null)
                    _discountRepository = new DiscountRepository(_dbContext);
                return _discountRepository;
            }
        }
    }
}
