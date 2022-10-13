// <copyright file="OrderSagaItem.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NewcomersTask.Models.DB
{
    [Table("OrderSagaItem")]
    public class OrderSagaItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        public string? Sku { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [JsonIgnore]
        public OrderState? OrderSaga { get; set; }
    }
}