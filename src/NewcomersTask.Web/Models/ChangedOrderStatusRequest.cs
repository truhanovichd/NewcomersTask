// <copyright file="ChangedOrderStatusRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace NewcomersTask.Web.Models
{
    public class ChangedOrderStatusRequest
    {
        public Guid OrderId { get; set; }

        public Status Status { get; set; }
    }
}