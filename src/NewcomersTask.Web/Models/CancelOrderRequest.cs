// <copyright file="CancelOrderRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NewcomersTask.Web.Models
{
    public class CancelOrderRequest
    {
        public Guid OrderId { get; set; }
    }
}