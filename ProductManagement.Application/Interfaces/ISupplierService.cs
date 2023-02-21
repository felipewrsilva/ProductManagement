using ProductManagement.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagement.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<IEnumerable<SupplierDTO>> GetSuppliersAsync();
        Task<SupplierDTO> GetSupplierByIdAsync(int id);
        Task<SupplierDTO> CreateSupplierAsync(SupplierDTO supplierDto);
        Task<SupplierDTO> UpdateSupplierAsync(int id, SupplierDTO supplierDto);
        Task<bool> DeleteSupplierAsync(int id);
    }
}