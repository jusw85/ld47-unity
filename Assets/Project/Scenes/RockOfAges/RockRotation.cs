using Jusw85.Common;
using k;
using MyBox;
using Prime31;
using UnityEngine;

public class RockRotation : MonoBehaviour
{
    private void Awake()
    {
    }

    public void RotateTransform()
    {
        transform.Rotate(new Vector3(0, 0, -1));
    }
}