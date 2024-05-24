using Kilosoft.Tools;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private Transform _topRodPoinTransform;
    [SerializeField] private Transform _bottomRodPoinTransform;
    [SerializeField] private LineRenderer _lineRenderer;

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    [EditorButton("Update Line")]
    public void UpdateLine()
    {
        _lineRenderer.SetPosition(0, _topRodPoinTransform.position);
        _lineRenderer.SetPosition(1, _bottomRodPoinTransform.position);
    }
}