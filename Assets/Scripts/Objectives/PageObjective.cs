using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

public class PageObjective : BaseObjective
{
    public string PageDescription;

    public override void Pickup()
    {
        base.Pickup();

        UIManager.Instance.ShowPage(this);
    }
}
