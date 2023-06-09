using UnityEngine;

namespace Demos.Demo_1.Kernel.Advertisement
{
    public class AdsRunner : IAdsRunner
    {
        public void Enable()
        {
            Debug.Log("Ads has been enabled");
        }

        public void Disable()
        {
            Debug.Log("Ads has been disabled");
        }
    }

    public interface IAdsRunner
    {
        public void Enable();
        public void Disable();
    }
}