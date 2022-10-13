// <copyright file="OrderCreated.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NewcomersTask.Models
{
    public class OrderCreated
    {
        public Guid OrderId { get; set; }

        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerSurname { get; set; }

        public List<OrderItem>? OrderSagaItems { get; set; }
    }
}