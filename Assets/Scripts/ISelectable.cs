using UnityEngine;

public interface ISelectable
{
    Transform transform { get; }

    void Select();
}
