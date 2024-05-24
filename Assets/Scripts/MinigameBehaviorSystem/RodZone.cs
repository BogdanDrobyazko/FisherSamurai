using System;
using System.Collections.Generic;
using UnityEngine;
using Kilosoft.Tools;


public class RodZone : MonoBehaviour
{
    [SerializeField] private PolygonCollider2D _collider;
    [SerializeField] private float _rotationSpread = 120f;
    [Range(0, 1)] [SerializeField] private float _rotationSensitivity = 0.1f;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private Transform _leftBorderEndPoint;
    [SerializeField] private Transform _rightBorderEndPoint;
    [SerializeField] private int _rodIndex = 0;

    private Dictionary<int, float> _borderDistanceDictionary = new Dictionary<int, float>
    {
        { 0, 0f },
        { 1, 2f },
        { 2, 4f },
        { 3, 6f },
        { 4, 10f },
    };


    [EditorButton("ChangeRodZoneSize")]
    public void ChangeRodZoneSize()
    {
        ChageRodZoneSize(_rodIndex);
    }

    public PolygonCollider2D GetCollider() => _collider;
    
    public void ChageRodZoneSize(int rodIndex)
    {
        float borderDistance = _borderDistanceDictionary[rodIndex];
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        ChangeBorderDistance(borderDistance);

       
    }

   

    public void FollowMouse()
    {
        float mouseX = Input.mousePosition.x;
        float screenCenterX = Screen.width / 2f;
        float mouseOffsetX = mouseX - screenCenterX;

        float rotationZ = mouseOffsetX * _rotationSensitivity;

        float rotationSpreadMin = -_rotationSpread / 2;
        float rotationSpreadMax = _rotationSpread / 2;


        if (rotationZ > rotationSpreadMax)
        {
            rotationZ = rotationSpreadMax;
        }
        else if (rotationZ < rotationSpreadMin)
        {
            rotationZ = rotationSpreadMin;
        }

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    private void ChangeBorderDistance(float newDistance)
    {
        float borderRotationZ = newDistance / 2;
        _leftBorder.localRotation = Quaternion.Euler(0f, 0f, -borderRotationZ);
        _rightBorder.localRotation = Quaternion.Euler(0f, 0f, borderRotationZ);
        
        Vector3 bottomLeft = _leftBorderEndPoint.position ;
        Vector3 bottomRight = _rightBorderEndPoint.position;
        
        _collider.points = new Vector2[] {
            _collider.points [0],
            _collider.points [1], 
            bottomLeft, 
            bottomRight };
    }
}