using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InputScrollView : MonoBehaviour
    {
        public event Action<int> Spawn;

        [SerializeField] private float[] _positionX;
        [SerializeField] private RectTransform _content;
        [SerializeField] private float _speed;
        [SerializeField] private Button _buttonSpawn;


        private float _currentX;
        private int _currentIndex;

        private void Awake()
        {
            _currentIndex = 0;
            _currentX = _positionX[_currentIndex];
        }

        private void OnEnable()
        {
            _buttonSpawn.onClick.AddListener(() => Spawn?.Invoke(GetNumber()));
        }

        private void OnDisable()
        {
            _buttonSpawn.onClick.RemoveListener(() => Spawn?.Invoke(GetNumber()));
        }

        public void Minus()
        {
            if (_currentIndex > 0)
            {
                _currentIndex--;
                float targetPos = _positionX[_currentIndex];
                Transition(targetPos);
            }
        }

        public void Plus()
        {
            if (_currentIndex < _positionX.Length - 1)
            {
                _currentIndex++;
                float targetPos = _positionX[_currentIndex];
                Transition(targetPos);
            }
        }

        private void SetHorizontalPosition(float x)
        {
            _content.anchoredPosition = new Vector2(x, 0);
        }

        private async void Transition(float targetPos)
        {
            if (targetPos > _currentX)
            {
                await DoTransitionMax(targetPos);
            }
            else
            {
                await DoTransitionMin(targetPos);
            }
        }

        private async Task DoTransitionMax(float targetPos)
        {
            while (targetPos > _currentX)
            {
                _currentX += _speed * Time.deltaTime;
                SetHorizontalPosition(_currentX);
                await Task.Yield();
            }
        }

        private async Task DoTransitionMin(float targetPos)
        {
            while (targetPos < _currentX)
            {
                _currentX -= _speed * Time.deltaTime;
                SetHorizontalPosition(_currentX);
                await Task.Yield();
            }
        }

        private int GetNumber()
        {
            return _currentIndex + 1;
        }
    }
}