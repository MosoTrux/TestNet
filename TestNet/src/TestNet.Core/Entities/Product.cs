using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNet.Core.Entities
{
    public partial class Product : BaseEntity
    {
        public string Name { get; set; }

        public bool Status { get; set; }

        public decimal Stock { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? UpdatedUser { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
