using System;
using DG.Tweening;
using UnityEngine;

namespace _Works.AH.Code
{
    public class RotateText : MonoBehaviour
    {
        [SerializeField] private float duration;
        private RectTransform _rect;
        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            transform.DORotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
        }
    }
}
