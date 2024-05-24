using UnityEngine;

namespace MinigameBehaviorSystem
{
    public class PullingProgressBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _progresBarFill;

        private const float _fillMaxSizeX = 5f;
        private const float _fillMinSizeX = 0.1f;
        public void SetProgress(float progressDelta)
        {
            float fillSizeX =  _fillMaxSizeX * progressDelta + _fillMinSizeX;
            _progresBarFill.size = new Vector2( fillSizeX, _progresBarFill.size.y);
        }
    }
}