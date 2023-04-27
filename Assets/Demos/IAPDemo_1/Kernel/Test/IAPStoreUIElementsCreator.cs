using Demos.IAPDemo_1.Kernel.UI;
using Kernel.Interfaces;
using UnityEngine;

namespace Demos.IAPDemo_1.Kernel.Test
{
    public class IAPStoreUIElementsCreator : MonoBehaviour
    {
        [SerializeField] private IAPStoreUIElement iapStoreUIElement;
        [SerializeField] private SwipeMenu swipeMenu;
        [SerializeField] private int instancesCount = 5;
        private IIAPStore _iapStore;

        private void Start()
        {
            for (int i = 0; i < instancesCount; i++)
            {
                var instance = Instantiate(iapStoreUIElement);
                instance.Initialize(_iapStore);
                
                swipeMenu.AddElement(instance.GetComponent<RectTransform>());
            }
        }

        public void Initialize(IIAPStore iapStore)
        {
            _iapStore = iapStore;
        }
    }
}