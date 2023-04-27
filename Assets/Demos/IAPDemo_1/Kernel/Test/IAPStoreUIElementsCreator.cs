using Demos.IAPDemo_1.Kernel.UI;
using Kernel;
using Kernel.Interfaces;
using Kernel.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demos.IAPDemo_1.Kernel.Test
{
    public class IAPStoreUIElementsCreator : MonoBehaviour
    {
        [FormerlySerializedAs("_iapStore")] [SerializeField] private IAPStore iapStore;
        [SerializeField] private IAPStoreUIElement iapStoreUIElement;
        [SerializeField] private SwipeMenu swipeMenu;
        [SerializeField] private int instancesCount = 5;

        private void Start()
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