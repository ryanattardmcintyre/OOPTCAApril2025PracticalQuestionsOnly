using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPForTCA
{
    public class OrderDbRepository
    {
        private readonly AppDbContext _context;

        public OrderDbRepository(AppDbContext context)
        {
            _context = context;
        }

      
        protected void PlaceOrder(int customerId, List<OrderItem> items)
        {
            Order myOrder = new Order();
            //continue here...
           
        }

      
        public List<CustomerOrderSummaryViewModel> GetCustomerOrderSummaries()
        {
            var list = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .GroupBy(o => new { o.Customer.Id, o.Customer.FullName })
                .Select(g => new CustomerOrderSummaryViewModel
                {
                    CustomerName = g.Key.FullName,
                    TotalOrders = g.Count(),
                    TotalItemsOrdered = g.SelectMany(x => x.Items).Sum(i => i.Quantity)
                })
                .ToList();

            return list;
        }

        public List<Order> GetOrders(Customer c)
        {
            return null;
        }
    }


}
