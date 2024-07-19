using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string UserId { get; private set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
            
        }

        public Order(Address address, string userId)
        {
            _orderItems = new();
            CreatedDate = DateTime.Now;
            Address = address;
            UserId = userId;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            if (!_orderItems.Any(x => x.ProductId == productId))
            {
                var orderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(orderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
