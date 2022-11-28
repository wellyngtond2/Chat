using Chat.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Infrastructure.ExternalServices
{
    public class StockService : IStockService
    {
        public Task GetStockByCode(string code)
        {
            throw new NotImplementedException();
        }
    }
}
