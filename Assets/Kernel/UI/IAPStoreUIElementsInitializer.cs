using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kernel.UI
{
    public class IAPStoreUIElementsInitializer : MonoBehaviour
    {
        [SerializeField] private List<IAPStoreUIElement> iapStoreUIElements;
        [SerializeField] private IAPStore iapStore;

        private void Start()
        {
            foreach (var element in iapStoreUIElements)
                element.Initialize(iapStore);
        }
    }
}