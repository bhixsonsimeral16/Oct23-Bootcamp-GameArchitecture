using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractor : Interactor
{
    [SerializeField] private float interactDistance;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactLayerMask;

    //Ray cast
    private RaycastHit raycastHitInfo;
    private ISelectable selectable;

    public override void Interact()
    {
        // Three different ways to generate a ray from the center of the screen
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Debug.DrawRay(cam.transform.position, cam.transform.forward * interactDistance, Color.red);
        if(Physics.Raycast(ray, out raycastHitInfo, interactDistance, interactLayerMask))
        {
            selectable = raycastHitInfo.transform.GetComponent<ISelectable>();
            if(selectable != null)
            {
                Debug.Log($"Looking at interactable {raycastHitInfo.transform.name}");
                selectable.OnHoverEnter();

                if(playerInput.interactPressed)
                {
                    selectable.OnSelect();
                }
            }
        }

        if(raycastHitInfo.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }
    }
}
