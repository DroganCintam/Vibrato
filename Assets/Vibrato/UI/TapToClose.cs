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
        private float delay;

        private float timeAtAwake;
        
        public static Action CloseMethod { get; set; }

        private void Awake()
        {
            timeAtAwake = Time.realtimeSinceStartup;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (delay > 0 && Time.realtimeSinceStartup - timeAtAwake <= delay) return;
            timeAtAwake = Time.realtimeSinceStartup;

            CloseMethod?.Invoke();
        }
    }
}