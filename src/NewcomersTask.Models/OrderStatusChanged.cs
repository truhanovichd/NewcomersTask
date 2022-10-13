// <copyright file="OrderStatusChanged.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NewcomersTask.Models
{
    public class OrderStatusChanged
    {
        public Guid CorrelationId { get; set; }

        public Status Status { get; set; }
    }
}