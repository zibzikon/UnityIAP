using Kernel.Interfaces;

namespace Demos.Demo_1.Kernel.Test
{
    public class IAPStoreTest
    {
        private readonly IIAPStore _iapStore;

        public IAPStoreTest(IIAPStore iapStore)
        {
            _iapStore = iapStore;
        }

        private void Do()
        {
            _iapStore.Purchase("remove_ads");
        }
    }
}