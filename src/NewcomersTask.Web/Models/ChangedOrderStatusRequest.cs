// <copyright file="ChangedOrderStatusRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using NewcomersTask.Models;

namespace NewcomersTask.Web.Models
{
    public class ChangedOrderStatusRequest
    {
        public Guid OrderId { get; set; }

        public Status Status { get; set; }
    }
}