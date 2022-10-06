// <copyright file="OrderStatusChangedRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NewcomersTask.Web.Models
{
    public class OrderStatusChangedRequest
    {
        public Guid CorrelationId { get; set; }

        public Status Status { get; set; }
    }
}