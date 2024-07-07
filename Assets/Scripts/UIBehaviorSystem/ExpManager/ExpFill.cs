using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpFill : MonoBehaviour
{
    [SerializeField] private int _leftPositionBorder;
    [SerializeField] private int _rightPositionBorder;
    
    public void SetFillPosition(float positionDelta)
    {
        float fillPosition = positionDelta * (_rightPositionBorder - _leftPositionBorder) + _leftPositionBorder;
        transform.localPosition = new Vector3(fillPosition, 0, 0);
    }
}
