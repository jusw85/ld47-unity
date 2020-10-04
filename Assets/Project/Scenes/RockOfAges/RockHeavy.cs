using System;
using UnityEngine;

public class RockHeavy : MonoBehaviour
{
    [SerializeField] private float newMass = 10f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Rock"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().mass = newMass;
        }
    }
}