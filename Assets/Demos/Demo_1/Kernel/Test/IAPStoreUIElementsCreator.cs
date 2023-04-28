using Demos.Demo_1.Kernel.UI;
using Kernel;
using Kernel.UI;
using UnityEngine;

namespace Demos.Demo_1.Kernel.Test
{
    public class IAPStoreUIElementsCreator : MonoBehaviour
    {
        [SerializeField] private IAPStoreUIElement iapStoreUIElement;
        [SerializeField] private SwipeMenu swipeMenu;
        [SerializeField] private int instancesCount = 5;

        public void Initialize(IAPStore iapStore)
        {
            for (int i = 0; i < instancesCount; i++)
            {
                var instance = Instantiate(iapStoreUIElement);
                instance.Initialize(iapStore);
                
                swipeMenu.AddElement(instance.GetComponent<RectTransform>());
            }
        }
        
    }
}