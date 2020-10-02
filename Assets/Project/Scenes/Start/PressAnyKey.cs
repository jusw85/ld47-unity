using UnityEngine;
using UnityEngine.Events;

public class PressAnyKey : MonoBehaviour
{
    [SerializeField] private bool disableOnKeydown = true;
    [SerializeField] private UnityEvent onKeyDown;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (disableOnKeydown) enabled = false;
            onKeyDown.Invoke();
        }
    }
}