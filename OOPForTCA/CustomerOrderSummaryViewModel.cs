using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPForTCA
{

    public class CustomerOrderSummaryViewModel
    {
        public string CustomerName { get; set; }
        public int TotalOrders { get; set; }
        public int TotalItemsOrdered { get; set; }
    }
}
