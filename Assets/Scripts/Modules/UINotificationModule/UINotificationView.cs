using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Modules.UINotificationModule
{

    public sealed class UINotificationView : MonoBehaviour
    {
        public Action<UINotificationView> ON_REMOVE;
        private const float _notificationDuration = 2.5f;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private RectTransform _rectTransform;

        public void Initialize(Vector3 screenPosition, string message, int targetLvl)
        {
            _text.text = message.ToString();
            _lvlText.text = targetLvl.ToString();
            _rectTransform.position = screenPosition;
            StartCoroutine(NotificationCoroutine());
        }

        private IEnumerator NotificationCoroutine()
        {
            yield return new WaitForSeconds(_notificationDuration);
            float height = UnityEngine.Random.Range(100f, 150f);
            float endPositionY = _rectTransform.position.y + height;
            Vector3 pos = _rectTransform.position;
            pos.y = endPositionY;
            _rectTransform.position = pos;
            FireRemove();
        }

        private void FireRemove()
        {
            ON_REMOVE?.Invoke(this);
        }
    }
}