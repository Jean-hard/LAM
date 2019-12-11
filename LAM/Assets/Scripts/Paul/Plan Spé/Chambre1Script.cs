using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chambre1Script : PlaneScript
{
    public override void OnActive()
    {
        base.OnActive();
        Room1Manager.Instance.ShowRoom1Dia();
    }
}
