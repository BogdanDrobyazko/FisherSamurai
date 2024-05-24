using UnityEngine;
using Random = UnityEngine.Random;

public class HitchingPrepareAnimationEvent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private int _animationCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        _animationCounter = Random.Range(1, 3);
    }

    
    private void Update()
    {
        if (_animationCounter <= 0)
        {
            _animator.Play("PreHitchingFishByte");
            _animationCounter = Random.Range(1, 3);
        }
    }

    public void TriggerEvent()
    {
        _animationCounter--;
    }
}
