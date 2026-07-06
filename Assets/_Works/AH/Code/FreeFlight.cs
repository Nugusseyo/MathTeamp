using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace _Works.AH.Code
{
    public class FreeFlightUI : MonoBehaviour
    {
        [SerializeField] private Vector2 paddingFromEdge = new Vector2(50f, 50f);

        [SerializeField, MinMaxRangeSlider(0, 2000)] private Vector2 speed = new Vector2(400f, 800f);
        [SerializeField] private Ease moveEase = Ease.InOutSine;

        [SerializeField] private bool rotateTowardsMoveDirection = false;
        [SerializeField] private float rotationOffset = 0;
        [SerializeField] private bool autoStart = true;

        private RectTransform _rectTransform;
        private RectTransform _parentRect;
        private Canvas _canvas;
        private Sequence _flightSequence;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _parentRect = _rectTransform.parent as RectTransform;
            _canvas = GetComponentInParent<Canvas>();
        }

        private void Start()
        {
            if (autoStart)
            {
                StartFlight();
            }
        }

        private void OnDestroy()
        {
            _flightSequence?.Kill();
        }

        public void StartFlight()
        {
            _flightSequence?.Kill();
            FlyToNextPoint();
        }

        public void StopFlight()
        {
            _flightSequence?.Kill();
        }

        private void FlyToNextPoint()
        {
            Vector2 targetPosition = GetRandomScreenPoint();
            float distance = Vector2.Distance(_rectTransform.anchoredPosition, targetPosition);
            float moveSpeed = Random.Range(speed.x, speed.y);
            float moveDuration = Mathf.Max(distance / moveSpeed, 0.1f);

            if (rotateTowardsMoveDirection)
            {
                Vector2 direction = targetPosition - _rectTransform.anchoredPosition;
                if (direction.sqrMagnitude > 0.01f)
                {
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    _rectTransform.DORotate(new Vector3(0f, 0f, angle + rotationOffset), moveDuration * 0.3f);
                }
            }

            _flightSequence = DOTween.Sequence();
            _flightSequence.Append(
                _rectTransform.DOAnchorPos(targetPosition, moveDuration).SetEase(moveEase)
            );
            _flightSequence.OnComplete(FlyToNextPoint);
        }

        private Vector2 GetRandomScreenPoint()
        {
            float screenHalfWidth = Screen.width * 0.5f;
            float screenHalfHeight = Screen.height * 0.5f;

            float randomScreenX = Random.Range(paddingFromEdge.x, Screen.width - paddingFromEdge.x);
            float randomScreenY = Random.Range(paddingFromEdge.y, Screen.height - paddingFromEdge.y);

            Vector2 screenPoint = new Vector2(randomScreenX, randomScreenY);

            Camera cam = (_canvas != null && _canvas.renderMode != RenderMode.ScreenSpaceOverlay)
                ? _canvas.worldCamera
                : null;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _parentRect,
                screenPoint,
                cam,
                out var localPoint
            );

            return localPoint;
        }
    }
}