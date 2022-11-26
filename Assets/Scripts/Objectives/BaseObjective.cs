using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    private void Start()
    {
        this.NotifySelf();
    }

    public void NotifySelf()
    {
        GameManager.Instance.AddObjective(this);
    }


}
