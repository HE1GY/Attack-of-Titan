using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UI
{
    public class ScrollViewUI : MonoBehaviour
    {
        private const int Parts = 5;
        private ScrollRect _scrollRect;
        private float _currentNormalizedPosition;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _currentNormalizedPosition = _scrollRect.horizontalNormalizedPosition;
        }

        public void Minus()
        {
            _currentNormalizedPosition += 1 / Parts;
            SetHorizontalPosition(_currentNormalizedPosition);
        }
        public void Plus()
        {
            _currentNormalizedPosition -= 1 / Parts;
            SetHorizontalPosition(_currentNormalizedPosition);
        }

        private void SetHorizontalPosition(float normalized)
        {
            _currentNormalizedPosition = Mathf.Clamp(_currentNormalizedPosition, 0, 1);
            _scrollRect.horizontalNormalizedPosition = _currentNormalizedPosition;
        }
        
    }
}
