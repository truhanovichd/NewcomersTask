// <copyright file="CreateOrderRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewcomersTask.Web.Models
{
    public class CreateOrderRequest
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerSurname { get; set; }

        public List<OrderItemRequest>? OrderSagaItems { get; set; }
    }
}