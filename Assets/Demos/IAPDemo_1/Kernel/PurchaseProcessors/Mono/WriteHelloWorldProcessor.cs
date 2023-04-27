using Kernel.PurchaseProcessors;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Demos.IAPDemo_1.Kernel.PurchaseProcessors.Mono
{
    public class WriteHelloWorldProcessor : MonoIAPProductProcessor
    {
        public override string ProductID => "write_hello_world";
        
        public override PurchaseProcessingResult Process(Product product)
        {
            Debug.Log("Hello World!!!!!!!!");
            return PurchaseProcessingResult.Complete;
        }
    }
}