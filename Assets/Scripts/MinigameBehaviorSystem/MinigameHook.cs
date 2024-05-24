using UnityEngine;
using UnityEngine.Serialization;


public class MinigameHook : MonoBehaviour
{
    [FormerlySerializedAs("_progresIncreaseColor")] [SerializeField] private Color _progressIncreaseColor;
    [FormerlySerializedAs("_progresDecreaseColor")] [SerializeField] private Color _progressDecreaseColor;

    public void ProgressIncrease()
    {
        GetComponent<SpriteRenderer>().color = _progressIncreaseColor;
    }

    public void ProgressDecrease()
    {
        GetComponent<SpriteRenderer>().color = _progressDecreaseColor;
    }
}