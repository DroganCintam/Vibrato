using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Vibrato.UI
{
    [RequireComponent(typeof(Graphic))]
    public class TapToClose : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private float _delay;

        private float _timeAtAwake;
        
        public static Action CloseMethod { get; set; }

        private void Awake()
        {
            _timeAtAwake = Time.realtimeSinceStartup;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_delay > 0 && Time.realtimeSinceStartup - _timeAtAwake <= _delay) return;
            _timeAtAwake = Time.realtimeSinceStartup;

            CloseMethod?.Invoke();
        }
    }
}