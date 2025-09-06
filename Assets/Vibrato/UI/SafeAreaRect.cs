using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Vibrato.UI
{
    public sealed class SafeAreaRect : MonoBehaviour
    {
        [SerializeField]
        private Canvas _canvas;
        
        private RectTransform _rectTransform;
        private Vector2 _defaultSize;
        
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defaultSize = _rectTransform.rect.size;
            
#if !UNITY_EDITOR
            UpdateRect();
#endif
        }
        
#if UNITY_EDITOR
        private Vector2 _screenSize;
        private void LateUpdate()
        {
            if (!Mathf.Approximately(Screen.width, _screenSize.x)
                || !Mathf.Approximately(Screen.height, _screenSize.y))
            {
                _screenSize = new Vector2(Screen.width, Screen.height);
                ResetAndUpdate().Forget();
            }

            return;

            async UniTask ResetAndUpdate()
            {
                _rectTransform.sizeDelta = Vector2.zero;
                _rectTransform.anchoredPosition = Vector2.zero;
                
                await UniTask.NextFrame();
                _defaultSize = _rectTransform.rect.size;
                
                UpdateRect();
            }
        }
#endif

        private void UpdateRect()
        {
            var safeArea = Screen.safeArea;

            float screenToLocal = _defaultSize.x / Screen.width;
            float safeTopInLocal = safeArea.yMax * screenToLocal;
            float heightDifference = _defaultSize.y - safeTopInLocal;
            _rectTransform.sizeDelta = new Vector2(0, -heightDifference);
            _rectTransform.anchoredPosition = new Vector2(0, -heightDifference / 2);
        }
    }
}