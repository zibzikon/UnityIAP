using TMPro;
using UnityEngine;

namespace Demos.IAPDemo_1.Kernel.UI
{
    public class GemsCountLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMesh;
        private int _gems;

        public void AddGems(int count)
        {
            _gems += count;
            UpdateView();
        }

        public void SetGems(int count)
        {
            _gems = count;
            UpdateView();
        }

        private void UpdateView()
        {
            _textMesh.SetText(_gems.ToString());
        }
    }
}