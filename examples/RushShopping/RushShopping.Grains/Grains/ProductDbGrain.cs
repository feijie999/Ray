﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Ray.Core;
using Ray.Core.Event;
using RushShopping.Grains.States;
using RushShopping.IGrains;
using RushShopping.Repository.Entities;
using RushShopping.Share.Dto;
using Ray.Core.Observer;

namespace RushShopping.Grains.Grains
{

    [Observer(DefaultObserverGroup.primary, "db", typeof(ProductGrain))]
    public class ProductDbGrain : CrudDbGrain<ProductGrain, ProductState,Guid,Product>, IProductDbGrain
    {
        #region Overrides of CrudDbGrain<ProductGrain,ProductState,Guid,Product>

        public override Task Process(FullyEvent<Guid> @event)
        {
            return Task.CompletedTask;
        }

        #endregion

        public ProductDbGrain(IGrainFactory grainFactory) : base(grainFactory)
        {
        }
    }
}