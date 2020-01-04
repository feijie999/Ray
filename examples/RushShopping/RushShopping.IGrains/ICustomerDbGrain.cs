using System;
using Orleans;
using Ray.Core;
using Ray.Core.Observer;

namespace RushShopping.IGrains
{
    public interface ICustomerDbGrain : IObserver, IGrainWithGuidKey, ICrudDbGrain<Guid>
    {
    }
}