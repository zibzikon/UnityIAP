using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kernel.UI
{
    public class IAPStoreUIElementsInitializer : MonoBehaviour
    {
        [SerializeField] private List<IAPStoreUIElement> iapStoreUIElements;

        public void Initialize(IAPStore iapStore)
        {
            foreach (var element in iapStoreUIElements)
                element.Initialize(iapStore);
        }
    }
}