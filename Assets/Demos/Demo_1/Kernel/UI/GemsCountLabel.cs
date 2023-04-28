using TMPro;
using UnityEngine;

namespace Demos.Demo_1.Kernel.UI
{
    public class GemsCountLabel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private Player player;

        private void Update()
        {
            UpdateView();
        }

        private void UpdateView()
        {
            textMesh.SetText(player.Gems.ToString());
        }
    }
}