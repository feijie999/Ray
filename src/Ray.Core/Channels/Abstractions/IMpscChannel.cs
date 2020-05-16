﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ray.Core.Channels
{
    public interface IMpscChannel<T> : IBaseMpscChannel
    {
        void BindConsumer(Func<List<T>, Task> consumer);
        void Config(int maxBatchSize, int maxMillisecondsDelay);
        ValueTask<bool> WriteAsync(T data);
    }
}
