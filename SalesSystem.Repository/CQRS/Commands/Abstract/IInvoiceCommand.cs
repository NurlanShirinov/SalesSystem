using SalesSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Repository.CQRS.Commands.Abstract
{
    public interface IInvoiceCommand
    {
        Task<Guid> Post(Invoice invoice);
        Task<Invoice> Put(Invoice invoice);
        Task<bool> Delete(Guid id);
    }
}