using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Vibrato.UI
{
    public sealed class SafeAreaRect : MonoBehaviour
    {
        [SerializeField]
        private Canvas canvas;
        
        private RectTransform rectTransform;
        private Vector2 defaultSize;
        
        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            defaultSize = rectTransform.rect.size;
            
#if !UNITY_EDITOR
            UpdateRect();
#endif
        }
        
#if UNITY_EDITOR
        private Vector2 screenSize;
        private void LateUpdate()
        {
            if (!Mathf.Approximately(Screen.width, screenSize.x)
                || !Mathf.Approximately(Screen.height, screenSize.y))
            {
                screenSize = new Vector2(Screen.width, Screen.height);
                ResetAndUpdate().Forget();
            }

            return;

            async UniTask ResetAndUpdate()
            {
                rectTransform.sizeDelta = Vector2.zero;
                rectTransform.anchoredPosition = Vector2.zero;
                
                await UniTask.NextFrame();
                defaultSize = rectTransform.rect.size;
                
                UpdateRect();
            }
        }
#endif

        private void UpdateRect()
        {
            var safeArea = Screen.safeArea;

            float screenToLocal = defaultSize.x / Screen.width;
            float safeTopInLocal = safeArea.yMax * screenToLocal;
            float heightDifference = defaultSize.y - safeTopInLocal;
            rectTransform.sizeDelta = new Vector2(0, -heightDifference);
            rectTransform.anchoredPosition = new Vector2(0, -heightDifference / 2);
        }
    }
}