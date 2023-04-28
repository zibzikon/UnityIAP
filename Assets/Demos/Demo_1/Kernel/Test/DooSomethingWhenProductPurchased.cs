using Kernel;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Demos.Demo_1.Kernel.Test
{
    public class DooSomethingWhenProductPurchased : MonoBehaviour
    {
        [SerializeField] private IAPStore iapStore;

        private void Awake()
        {
            iapStore.PurchaseProcessed += LogOnProductPurchased;
        }

        private void OnDestroy()
        {
            iapStore.PurchaseProcessed -= LogOnProductPurchased;
        }

        private void LogOnProductPurchased(Product product)
        {
            Debug.Log("Some scene actions invoked");
        }
    }
}