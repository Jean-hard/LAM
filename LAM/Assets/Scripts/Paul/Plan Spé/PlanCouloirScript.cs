using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanCouloirScript : PlaneScript
{
    // Start is called before the first frame update
    public override void OnActive()
    {
        base.OnActive();
        AccueilCouloirManager.Instance.ShowCouloirDia();
    }
}
