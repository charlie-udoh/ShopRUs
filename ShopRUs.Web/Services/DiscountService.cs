using ShopRUs.Core.Entities;
using ShopRUs.Core.Interfaces.Data;
using ShopRUs.Web.Interfaces;
using ShopRUs.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopRUs.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountService(IDiscountRepository discountRepository, IUnitOfWork unitOfWork)
        {
            _discountRepository = discountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<DiscountDTO>> GetAllDiscounts()
        {
            var discounts = new List<DiscountDTO>();
            var records = await _discountRepository.GetAll();
            foreach (var rec in records)
            {
                discounts.Add(new DiscountDTO
                {
                    Id = rec.Id,
                    DiscountAppliesTo = rec.DiscountAppliesTo,
                    DiscountValue = rec.DiscountValue,
                    DiscountValueType = rec.DiscountValueType,
                    Units = rec.Units,
                });
            }
            return discounts;
        }

        public async Task<DiscountDTO> GetDiscountById(int id)
        {
            var discount = new DiscountDTO();
            var record = await _discountRepository.GetById(id);
            if (record == null)
                return null;
            discount.Id = record.Id;
            discount.DiscountAppliesTo = record.DiscountAppliesTo;
            discount.DiscountValue = record.DiscountValue;
            discount.DiscountValueType = record.DiscountValueType;
            discount.Units = record.Units;
            return discount;
        }

        public async Task<DiscountDTO> GetDiscountByType(string type)
        {
            var discount = new DiscountDTO();
            var record = await _discountRepository.GetByType(type);
            if (record == null)
                return null;
            discount.Id = record.Id;
            discount.DiscountAppliesTo = record.DiscountAppliesTo;
            discount.DiscountValue = record.DiscountValue;
            discount.DiscountValueType = record.DiscountValueType;
            discount.Units = record.Units;
            return discount;
        }

        public async Task<DiscountDTO> CreateDiscount(DiscountDTO discount)
        {
            var newDiscount = new Discount
            {
                DiscountAppliesTo = discount.DiscountAppliesTo,
                DiscountValue = discount.DiscountValue,
                DiscountValueType = discount.DiscountValueType,
                Units = discount.Units
            };
            await _unitOfWork.DiscountRepository.Insert(newDiscount);
            _unitOfWork.Commit();
            discount.Id = newDiscount.Id;
            return discount;
        }
    }
}
