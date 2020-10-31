﻿using System.Threading.Tasks;
using Orleans;
using Ray.DistributedTx.Abstractions;

namespace TxTransfer.IGrains
{
    public interface IAccount : IDistributedTx, IGrainWithIntegerKey
    {
        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <returns></returns>
        Task<decimal> GetBalance();
        /// <summary>
        /// 增加账户金额
        /// </summary>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        Task<bool> TopUp(decimal amount);
        /// <summary>
        /// 转账扣费
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="transactionIdt">分布式事务Id</param>
        /// <returns></returns>
        Task<bool> TransferDeduct(decimal amount, string transactionId);

        /// <summary>
        /// 转账到账
        /// </summary>
        /// <param name="amount"></param>
        /// <param name=""></param>
        /// <param name="transactionId">分布式事务Id</param>
        /// <returns></returns>
        Task TransferArrived(decimal amount, string transactionId);
    }
}
