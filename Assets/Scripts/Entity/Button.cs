using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, ISelectable
{
    [SerializeField] private MeshRenderer buttonRenderer;
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material triggeredMaterial;

    public UnityEvent onButtonPushEvent;
    public void OnHoverEnter()
    {
        buttonRenderer.material = triggeredMaterial;
    }

    public void OnHoverExit()
    {
        buttonRenderer.material = defaultMaterial;
    }

    public void OnSelect()
    {
        onButtonPushEvent?.Invoke();
    }
}
