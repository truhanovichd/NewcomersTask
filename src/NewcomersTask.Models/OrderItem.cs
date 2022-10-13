// <copyright file="OrderItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NewcomersTask.Models
{
    public class OrderItem
    {
        public string? Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}