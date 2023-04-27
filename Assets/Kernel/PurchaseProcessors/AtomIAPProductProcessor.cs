using Kernel.Interfaces;
using UnityEngine.Purchasing;

namespace Kernel.PurchaseProcessors
{
    public abstract class AtomIAPProductProcessor : IIAPProductProcessor
    {
        public abstract string ProductID { get; }
        public abstract PurchaseProcessingResult Process(Product product);
        
        public virtual void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) { }
    }
}