using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AH.Code
{
    public class EvasionUI : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform placeholder;
        [SerializeField] private int maxEvadeCount = 5;
        [SerializeField] private float evadeDistance = 150f;
        [SerializeField] private float evadeDuration = 0.25f;
        [SerializeField] private float returnDuration = 0.35f;
        [SerializeField] private float returnDelay = 1f;
        [SerializeField] private float randomAngleRange = 60f;
        [SerializeField] private Ease evadeEase = Ease.OutQuad;
        [SerializeField] private Ease returnEase = Ease.OutQuad;
        [SerializeField] private Ease finalReturnEase = Ease.InOutQuad;
        [SerializeField] private float finalReturnDuration = 0.4f;

        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private Sequence _currentSequence;
        private int _currentEvadeCount = 0;
        private bool _isLocked = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            if (placeholder != null)
            {
                _rectTransform.position = placeholder.position;
            }
            _originalPosition = _rectTransform.anchoredPosition;
        }

        private void OnDestroy()
        {
            _currentSequence?.Kill();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_isLocked) return;
            Evade(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isLocked) return;
            Evade(eventData.position);
        }

        private void Evade(Vector2 pointerScreenPos)
        {
            _currentSequence?.Kill();

            _currentEvadeCount++;

            if (_currentEvadeCount >= maxEvadeCount)
            {
                _isLocked = true;

                _currentSequence = DOTween.Sequence();
                _currentSequence.Append(
                    _rectTransform.DOAnchorPos(_originalPosition, finalReturnDuration).SetEase(finalReturnEase)
                );
                return;
            }

            Vector2 targetPosition = GetRandomEvadePosition(pointerScreenPos);

            _currentSequence = DOTween.Sequence();
            _currentSequence.Append(
                _rectTransform.DOAnchorPos(targetPosition, evadeDuration).SetEase(evadeEase)
            );
            _currentSequence.AppendInterval(returnDelay);
            _currentSequence.Append(
                _rectTransform.DOAnchorPos(_originalPosition, returnDuration).SetEase(returnEase)
            );
        }

        private Vector2 GetRandomEvadePosition(Vector2 pointerScreenPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                pointerScreenPos,
                null,
                out var pointerLocalPos
            );

            Vector2 awayFromPointer = (_rectTransform.anchoredPosition - pointerLocalPos);
            awayFromPointer = awayFromPointer.sqrMagnitude > 0.01f
                ? awayFromPointer.normalized
                : Random.insideUnitCircle.normalized;

            float randomAngle = Random.Range(-randomAngleRange, randomAngleRange);
            Vector2 randomDirection = Quaternion.Euler(0f, 0f, randomAngle) * awayFromPointer;

            float randomDistance = Random.Range(evadeDistance * 0.7f, evadeDistance * 1.3f);

            return _originalPosition + randomDirection * randomDistance;
        }
    }
}