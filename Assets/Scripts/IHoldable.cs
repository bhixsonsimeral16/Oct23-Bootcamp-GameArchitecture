using UnityEngine;

public interface IHoldable
{
    public void OnHold(Transform attachTransform);

    public void OnRelease();
}
