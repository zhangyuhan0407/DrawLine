using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LineBase : MaskableGraphic
{
    public virtual void InitLine() { }
    public virtual void AddPoint(Vector3 point) { }
    public virtual void ClearPoint() { }
    public virtual void StartGame() { }
}
