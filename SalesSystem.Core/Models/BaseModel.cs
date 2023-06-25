using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Core.Models
{
    public class BaseModel
    {
        public Guid Id { get; set; } = new Guid();
        public bool DeleteStatus { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int RowNum { get; set; } 
    }
}
