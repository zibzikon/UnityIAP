using Kernel.PurchaseProcessors;
using UnityEngine;
using UnityEngine.Purchasing;

namespace Demos.Demo_1.Kernel.PurchaseProcessors.Mono
{
    public class AddGems500ProductProcessor : MonoIAPProductProcessor
    {
        [SerializeField] private Player player;
        public override string ProductID => "add_gems_500";
        
        
        public override PurchaseProcessingResult Process(Product product)
        {
            player.AddGems(500);
            
            return PurchaseProcessingResult.Complete;
        }
    }
}