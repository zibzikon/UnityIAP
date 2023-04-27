using Kernel;
using Kernel.IAP_complex.IAP;
using UnityEngine;

namespace Demos.IAPDemo_1.Kernel.Test
{
    public class IAPStoreMonoTest : MonoBehaviour
    {
        [SerializeField] private IAPStore iapStore;
        
        [SerializeField] private bool _do;
        

        private void Update()
        {
            if (_do)
            {
                Do();
                _do = false;
            }
        }

        private void Do()
        {
            iapStore.Purchase("write_hello_world");
        }
    }
}