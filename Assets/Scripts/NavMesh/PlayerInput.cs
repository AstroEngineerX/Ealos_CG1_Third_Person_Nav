using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMouseClick;

    RaycastHit _HitInfo;
    public LayerMask _clickLayerMask;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out _HitInfo, 100, _clickLayerMask))
            {
                OnMouseClick?.Invoke(_HitInfo.point);
                Debug.Log($"Hit Point: {_HitInfo.point} ");
            }
        }
    }
}
