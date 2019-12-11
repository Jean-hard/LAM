using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilScript : PlaneScript
{
    public override void OnActive()
    {
        base.OnActive();
        AccueilCouloirManager.Instance.ShowAccueilDia();
    }
}
