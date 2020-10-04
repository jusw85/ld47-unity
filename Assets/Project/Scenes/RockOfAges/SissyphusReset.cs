using System;
using UnityEngine;

public class SissyphusReset : MonoBehaviour
{
    [SerializeField] private SissyphusMovement man;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Rock"))
        {
            man.ButtonBoost = 0f;
        }
    }
}