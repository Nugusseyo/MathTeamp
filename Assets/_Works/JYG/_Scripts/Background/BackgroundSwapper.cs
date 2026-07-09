using System.Collections.Generic;
using UnityEngine;

namespace JYG._Scripts.Background
{
    public class BackgroundSwapper : MonoBehaviour
    {
        public List<SpriteRenderer> backgrounds = new List<SpriteRenderer>();
        private int _prevLevel = 0;

        public void SwapWithLevel(int level)
        {
            backgrounds[_prevLevel].enabled = false;
            backgrounds[level].enabled = true;
            _prevLevel = level;
        }
    }
}
