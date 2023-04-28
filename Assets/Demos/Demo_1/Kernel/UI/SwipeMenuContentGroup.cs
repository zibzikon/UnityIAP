using System.Collections.Generic;
using System.Linq;
using Kernel.Extentions;
using UnityEngine;

namespace Demos.Demo_1.Kernel.UI
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]   
    public class SwipeMenuContentGroup : MonoBehaviour
    {
        public IEnumerable<RectTransform> Elements => _elements;
        public RectTransform RectTransform => _rectTransform;
        
        [SerializeField] private float spacing;
        private RectTransform _rectTransform;

        private List<RectTransform> _elements;
        private float _lastSpacing;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _elements = _rectTransform.GetChildrens().ToList();
        }

        private void Update()
        {
            if(!IsChanged()) return;
            
            _lastSpacing = spacing;
            _elements = _rectTransform.GetChildrens().ToList();

            UpdateView();
        }

        private void PositionElements()
        {
            var rectTransformRect = _rectTransform.rect;
            
            var nextHorizontalPosition = rectTransformRect.xMin; 
            foreach (var element in _elements)
            {
                var halfWidth = element.rect.width / 2;
                nextHorizontalPosition += halfWidth;
                
                AdjustElementProperties(element);
                element.localPosition = new Vector3(nextHorizontalPosition, 0, 0);
                
                nextHorizontalPosition += spacing + halfWidth;
            }
        }

        public void AddElementToEnd(RectTransform element)
        {
            element.SetParent(_rectTransform);
            _elements.Add(element);
            PositionElements();
        }

        private void UpdateView()
        {
            AdjustRecTransformProperties();
            PositionElements();
        }
        
        private bool IsChanged()
        {
            if (spacing != _lastSpacing) return true;
            
            var childrens = _rectTransform.GetChildrens().ToList();
            
            if (childrens.Count != _elements.Count) return true;
            
            for (int i = 0; i < childrens.Count; i++)
            {
                var children = childrens[i];
                var element = _elements[i];
                
                if (children != element) return true;
            }

            return true;
        }

        private void AdjustRecTransformProperties()
        {
            var center = (0.5f).AsVector2();
            _rectTransform.pivot = center;

            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.sizeDelta = Vector2.zero;
        }

        private void AdjustElementProperties(RectTransform element)
        {
            var center = (0.5f).AsVector2();
            
            element.pivot = center;
            element.anchorMax = center;
            element.anchorMin = center;
            element.anchoredPosition = Vector3.zero;
        }
    }
}