using System;
using UnityEngine.Purchasing;

namespace Kernel.Interfaces
{
    public interface IIAPStore
    {
        event Action<Product, PurchaseFailureReason> PurchaseFailed;
        event Action<Product> PurchaseProcessed;
        event Action<bool, string> TransactionsRestored;

        void RestoreTransactions();
        void Purchase(string productId);
        void AddProcessor(IIAPProductProcessor processor);
        void RemoveProcessor(IIAPProductProcessor processor);
        Product GetProduct(string productId);
    }
}