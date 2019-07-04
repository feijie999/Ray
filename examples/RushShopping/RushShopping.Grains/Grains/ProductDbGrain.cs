﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Ray.Core;
using Ray.Core.Event;
using RushShopping.Grains.States;
using RushShopping.IGrains;
using RushShopping.Repository.Entities;

namespace RushShopping.Grains.Grains
{
    [Observer(DefaultObserverGroup.primary, "db", typeof(ProductGrain))]
    public class ProductDbGrain : CrudDbGrain<ProductGrain, ProductState,Guid,Product>, IProductDbGrain
    {
        public ProductDbGrain(ILogger<ProductDbGrain> logger) : base(logger)
        {
        }

        #region Overrides of ObserverGrain<ProductGrain,Guid>

        public override Guid GrainId => this.GetPrimaryKey();

        #endregion

        #region Overrides of CrudDbGrain<ProductGrain,ProductState,Guid,Product>

        public override Task Process(IFullyEvent<Guid> @event)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}