using Demos.IAPDemo_1.Kernel.Advertisement;
using Kernel.PurchaseProcessors;
using UnityEngine.Purchasing;

namespace Demos.IAPDemo_1.Kernel.PurchaseProcessors.Atom
{
    public class RemoveAdsProcessor : AtomIAPProductProcessor
    {
        private readonly IAdsRunner _adsRunner;
        public override string ProductID => "remove_ads";

        public RemoveAdsProcessor(IAdsRunner adsRunner)
        {
            _adsRunner = adsRunner;
        }
        
        public override PurchaseProcessingResult Process(Product product)
        {
            _adsRunner.Disable();
            
            return PurchaseProcessingResult.Complete;
        }
    }
}