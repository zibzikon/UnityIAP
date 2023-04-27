using Kernel.Interfaces;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Kernel.PurchaseProcessors
{
    public abstract class MonoIAPProductProcessor : MonoBehaviour, IIAPProductProcessor
    {
        public abstract string ProductID { get; }
        public abstract PurchaseProcessingResult Process(Product product);


        public virtual void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) { }
    }
}