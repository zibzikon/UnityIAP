using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kernel.Extentions
{
    public static class GameObjectExtensions
    {
        public static IEnumerable<RectTransform> GetChildrens(this RectTransform transform) =>
            Enumerable.Range(0, transform.childCount)
                .Select(i => transform.GetChild(i).transform as RectTransform);
    }
}