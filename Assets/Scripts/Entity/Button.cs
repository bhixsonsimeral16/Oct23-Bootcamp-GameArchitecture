using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, ISelectable
{
    // [SerializeField] private MeshRenderer buttonRenderer;
    // [SerializeField] private Material defaultMaterial;
    // [SerializeField] private Material triggeredMaterial;

    public UnityEvent onButtonPushEvent;
    public UnityEvent onHoverEnterEvent, onHoverExitEvent;
    public void OnHoverEnter()
    {
        onHoverEnterEvent?.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExitEvent?.Invoke();
    }

    public void OnSelect()
    {
        onButtonPushEvent?.Invoke();
        Debug.Log("Button Pushed");
    }
}
