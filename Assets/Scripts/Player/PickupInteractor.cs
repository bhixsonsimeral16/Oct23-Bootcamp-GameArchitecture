using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{

    [SerializeField] private Camera cam;
    [SerializeField] private float holdDistance;
    [SerializeField] private Transform attachPointTransform;
    [SerializeField] private LayerMask holdableLayerMask;


    private RaycastHit raycastHitInfo;
    private bool isHolding = false;
    private IHoldable holdable;

    public override void Interact()
    {
        HoldAndRelease();
    }

    void HoldAndRelease()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if(Physics.Raycast(ray, out raycastHitInfo, holdDistance, holdableLayerMask))
        {
            if(raycastHitInfo.transform.TryGetComponent<IHoldable>(out holdable))
            {

                if(playerInput.interactPressed && !isHolding)
                {
                    holdable.OnHold(attachPointTransform);
                    isHolding = true;
                    return;
                }
            }
        }

        if(playerInput.interactPressed && isHolding)
        {
            holdable.OnRelease();
            isHolding = false;
        }
    }
}
