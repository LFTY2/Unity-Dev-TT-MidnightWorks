using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI 
{
	public class ToggleView : MonoBehaviour
	{
		[SerializeField] private RectTransform _handle;
		[SerializeField] private Image _backgroundImage;
		private float _handlerOffset = 40;
		private bool _state;

		public Action OnToggleSwitch;

		private readonly Color _activeColor = new Color(0.4402813f, 0.9622642f, 0.4910938f),
			_deActiveColor = new Color(0.6232645f, 0.8867924f, 0.8763675f);

		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(Switch);
		}
		
		private void Switch()
		{
			_state = !_state;
			Animation();
			OnToggleSwitch?.Invoke();
		}
		
		public void Initialize(bool state)
		{
			_state = state;
			MoveToggle();
		}
	
		private void Animation()
		{
			MoveToggle();
		}

		private void MoveToggle()
		{
			Vector2 ancoredPos = _handle.anchoredPosition;
			ancoredPos.x = _state ? _handlerOffset : -_handlerOffset;
			_handle.anchoredPosition = ancoredPos;
			_backgroundImage.color = _state ? _activeColor : _deActiveColor;
		}
	}
}
