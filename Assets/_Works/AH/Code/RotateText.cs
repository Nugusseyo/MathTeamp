using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Works.AH.Code
{
    public class RotateText : MonoBehaviour
    {
        [SerializeField, MinMaxRangeSlider(0, 10)] private Vector2 duration;
        private RectTransform _rect;
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            transform.DORotate(new Vector3(0, 0, 360), Random.Range(duration.x, duration.y) , RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }
    }
}
