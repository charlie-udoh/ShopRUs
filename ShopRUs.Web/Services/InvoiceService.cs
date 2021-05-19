using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Core.Interfaces.DomainServices;
using ShopRUs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopRUs.Web.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerRepository _customerRepository;
        private readonly IInvoiceDomainService _invoiceDomainService;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IUnitOfWork unitOfWork, ICustomerRepository customerRepository, IInvoiceDomainService invoiceDomainService, IInvoiceRepository invoiceRepository)
        {
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
            _customerRepository = customerRepository;
            _invoiceDomainService = invoiceDomainService;
        }

        public async Task<InvoiceDTO> GetInvoiceById(int id)
        {
            var invoice = new InvoiceDTO();
            var record = await _invoiceRepository.GetById(id);
            invoice.InvoiceId = record.Id;
            invoice.InvoiceDate = record.InvoiceDate;
            invoice.TotalAmount = record.TotalAmount;
            invoice.DiscountAmount = record.DiscountAmount;
            invoice.CustomerId = record.CustomerId;
            invoice.Customer = record.Customer.Name;
            invoice.ShippingAddress = record.Customer.Address;
            invoice.Items = record.InvoiceItems.Select(s => new InvoiceItemDTO
            {
                ItemDescription = s.Description,
                ItemType = s.Category,
                Quantity = s.Units,
                UnitPrice = s.UnitPrice
            }).ToList();
            return invoice;
        }

        public async Task<ServiceResponse> CreateInvoice(InvoiceDTO bill)
        {
            //Get customer by Customer Id
            var customer = await _customerRepository.GetById(bill.CustomerId);
            if (customer == null)
                return new ServiceResponse { Successful = false, Message = "Customer ID is invalid" };
            //Get total amount of bill
            var totalAmount = CalculateInvoiceTotal(bill.Items);
            //exclude any item price with itemType of Groceries
            var totaAmountWithoutGroceries = CalculateInvoiceTotal(bill.Items.Where(s => s.ItemType.ToLower() != "groceries").ToList());
            //Caculate discount
            var discountAmount = await _invoiceDomainService.CalculateDiscount(totalAmount, totaAmountWithoutGroceries, customer.Role, customer.DateRegistered);
            //Save invoice to db
            var invoiceItems = new List<InvoiceItem>();
            foreach (var item in bill.Items)
            {
                invoiceItems.Add(new InvoiceItem
                {
                    Category = item.ItemType,
                    Description = item.ItemDescription,
                    UnitPrice = item.UnitPrice,
                    Units = item.Quantity,
                });
            }
            var invoice = new Invoice
            {
                InvoiceDate = DateTime.Now,
                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                CustomerId = bill.CustomerId,
                InvoiceItems = invoiceItems
            };
            await _unitOfWork.InvoiceRepository.Insert(invoice);
            _unitOfWork.Commit();
            bill.InvoiceDate = invoice.InvoiceDate;
            bill.InvoiceId = invoice.Id;
            bill.TotalAmount = invoice.TotalAmount;
            bill.DiscountAmount = invoice.DiscountAmount;
            bill.Customer = customer.Name;
            bill.ShippingAddress = customer.Address;

            return new ServiceResponse { Successful = true, Id = invoice.Id, Message = bill };
        }

        private decimal CalculateInvoiceTotal(List<InvoiceItemDTO> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Quantity * item.UnitPrice;
            }
            return total;
        }
    }
}
