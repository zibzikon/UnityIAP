using UnityEngine;
using UnityEngine.UI;

namespace Demos.IAPDemo_1.Kernel.UI
{
    public class SnapSpeedSlider : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private SwipeMenu _swipeMenu;

        private void Awake()
        {
            _slider.minValue = 0.1f;
            _slider.maxValue = 20;
        }

        private void Update()
        {
            _swipeMenu.SnapSpeed = _slider.value;
        }
    }
}