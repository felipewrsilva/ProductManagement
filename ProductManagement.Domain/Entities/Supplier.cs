using System.Collections.Generic;

namespace ProductManagement.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Cnpj { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}