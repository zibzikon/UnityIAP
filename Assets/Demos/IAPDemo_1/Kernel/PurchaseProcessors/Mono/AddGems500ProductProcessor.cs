using Demos.IAPDemo_1.Kernel.UI;
using Kernel.PurchaseProcessors;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Demos.IAPDemo_1.Kernel.PurchaseProcessors.Mono
{
    public class AddGems500ProductProcessor : MonoIAPProductProcessor
    {
        [SerializeField] private GemsCountLabel gemsCountLabel;
        public override string ProductID => "add_gems_500";
        
        
        public override PurchaseProcessingResult Process(Product product)
        {
            gemsCountLabel.AddGems(500);
            
            return PurchaseProcessingResult.Complete;
        }
    }
}