using UnityEngine;

namespace Demos.IAPDemo_1.Kernel.Advertisement
{
    public class AdsRunner : IAdsRunner
    {
        public void Enable()
        {
            Debug.Log("Ads has been enabled");
        }

        public void Disable()
        {
            Debug.Log("Ads has been disable");
        }
    }

    public interface IAdsRunner
    {
        public void Enable();
        public void Disable();
    }
}