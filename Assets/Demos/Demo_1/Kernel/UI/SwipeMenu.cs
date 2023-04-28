using System.Collections.Generic;
using System.Linq;
using Kernel.Extentions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static Demos.Demo_1.Kernel.UI.SwipeMenu.ExcludedSides;

namespace Demos.Demo_1.Kernel.UI
{
    [RequireComponent(typeof(ScrollRect))]
    public class SwipeMenu : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        public enum ExcludedSides
        {
            None,
            Left,
            Right,
            LeftAndRight,
        }

        public float SnapSpeed { get => snapSpeed; set => snapSpeed = value; }
    
        [Header("Controls")]
    
        [SerializeField, Range(0.1f, 20f)] private float snapSpeed= 5f;
        [SerializeField, Range(1, 5000)] private int snapStrength = 400;
        [SerializeField] private ExcludedSides excludedForSnap;
    

        [FormerlySerializedAs("contentRect")]
        [Header("Other Objects")]
    
        [SerializeField] private SwipeMenuContentGroup contentGroup;

        private bool _isScrolling;

        private ScrollRect _scrollRect;

        private List<RectTransform> _contentChildrens;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        private void Start()
        {
            UpdateContentList();
            AdjustProperties();
        }

        private void Update()
        {
            UpdateContentList();
        
            SnapContent();
        }

        public void AddElement(RectTransform elementTransform) => contentGroup.AddElementToEnd(elementTransform);

        public void OnBeginDrag(PointerEventData eventData)
        {
            _scrollRect.inertia = true;
            _isScrolling = true;
        }

        public void OnEndDrag(PointerEventData eventData) => _isScrolling = false;

        private void UpdateContentList() => _contentChildrens = contentGroup.Elements.ToList();
    
        private void AdjustProperties()
        {
            _scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
        }

        private void SnapContent()
        {
            if (_isScrolling || !_contentChildrens.Any()) return;

            var nearestPosition = FindNearestTarget();

            PositionContent(nearestPosition);
        }

        private void PositionContent(Vector2 nearestPosition)
        {
            var scrollVelocity = Mathf.Abs(_scrollRect.velocity.x);

            if (!IsFocusInRange()) _scrollRect.inertia = false;

            if (scrollVelocity < snapStrength) _scrollRect.inertia = false;
            if (scrollVelocity > snapStrength) return;

            var contentGroupRect = contentGroup.RectTransform;

        
            var contentPosition =
                Mathf.Lerp(contentGroupRect.anchoredPosition.x, nearestPosition.x, snapSpeed * Time.deltaTime).AsXVector2();
            contentGroupRect.anchoredPosition = contentPosition;
        }

        private bool IsFocusInRange()
        {
            var  horizontalPosition = contentGroup.RectTransform.anchoredPosition.x;
        
            return  _contentChildrens.Count > 1 &&
                    horizontalPosition < (-_contentChildrens[0].anchoredPosition.x) &&
                    horizontalPosition > (-_contentChildrens[^1].anchoredPosition.x);
        }
    
        private Vector2 FindNearestTarget()
        {
            var contentGroupRect = contentGroup.RectTransform;
            var nearestDistance = float.MaxValue;
            var nearestPosition = Vector2.zero;
            var length = _contentChildrens.Count;
        
            if(length <= 2) return Vector2.zero;
        
            for (var i = 0; i < _contentChildrens.Count; i++)
            {
                if(i == 0 &&(excludedForSnap is Left or LeftAndRight)) continue;;
                if(i == length - 1 &&(excludedForSnap is Right or LeftAndRight)) continue;
            
                var children = _contentChildrens[i];
                var invertedPosition = -(children.anchoredPosition);

                var distance = Mathf.Abs(contentGroupRect.anchoredPosition.x - invertedPosition.x);

                if (!(distance < nearestDistance)) continue;

                nearestDistance = distance;
                nearestPosition = invertedPosition;
            }

            return nearestPosition;
        }
    }
}