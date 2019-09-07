using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragAndDrop
{
    void Select();
    void Drag(Vector3 HitLocation);
    void Drop();
}
