using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagement.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ProductDbContext _context;
        private readonly IMapper _mapper;

        public SupplierService(ProductDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SupplierDTO>> GetSuppliersAsync()
        {
            var suppliers = await _context.Suppliers.ToListAsync();
            return _mapper.Map<IEnumerable<SupplierDTO>>(suppliers);
        }

        public async Task<SupplierDTO> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> CreateSupplierAsync(SupplierDTO supplierDto)
        {
            var supplier = _mapper.Map<Supplier>(supplierDto);
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<SupplierDTO> UpdateSupplierAsync(int id, SupplierDTO supplierDto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return null;

            _mapper.Map(supplierDto, supplier);
            await _context.SaveChangesAsync();
            return _mapper.Map<SupplierDTO>(supplier);
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
                return false;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}