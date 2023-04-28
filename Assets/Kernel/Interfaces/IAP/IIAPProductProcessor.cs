using UnityEngine.Purchasing;

namespace Kernel.Interfaces
{
    public interface IIAPProductProcessor
    {
        string ProductID { get; }
        PurchaseProcessingResult Process(Product product);
        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason);
    }
}