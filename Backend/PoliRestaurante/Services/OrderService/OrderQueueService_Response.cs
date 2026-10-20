using PoliRestaurante.Models.Entity;
using System.Collections.Generic;

namespace PoliRestaurante.Models
{
    public class OrderQueueService_Response
    {
        public bool Response { get; set; } = false;
        public string Description { get; set; } = "";
        public List<Order>? Orders { get; set; } = null;
    }
}
