using UnityEngine;

namespace Demos.IAPDemo_1.Kernel.UI
{
    public class SwipeMenuRuntimeElementsAddingTest : MonoBehaviour
    {
        [SerializeField] private SwipeMenu swipeMenu;
        [SerializeField] private SwipeMenuContentGroup contentGroup;
        [SerializeField] private RectTransform prefab;
        [SerializeField] private bool addNewElement;
        [SerializeField, Range(0,1)] private int addingMethod;

        private void OnValidate()
        {
            if (addNewElement)
            {
                var instance = Instantiate(prefab);
                
                if(addingMethod == 0) swipeMenu.AddElement(instance);
                if(addingMethod == 1) instance.SetParent(contentGroup.RectTransform);
            }
        }
    }
}