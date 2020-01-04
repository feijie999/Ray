using System;
using Orleans;
using Ray.Core.Observer;

namespace RushShopping.IGrains
{

    public interface IProductDbGrain : IObserver, IGrainWithGuidKey, ICrudDbGrain<Guid>
    {

    }
}