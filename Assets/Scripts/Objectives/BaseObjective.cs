using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

public abstract class BaseObjective : MonoBehaviour
{
    public string TextToShow;
    public Outline OutlineEffect;
    

    private void Start()
    {
        this.NotifySelf();
    }

    public void NotifySelf()
    {
        GameManager.Instance.AddObjective(this);
    }

    public void OnHovered()
    {
        this.OutlineEffect.enabled = true;
    }

    public void OnUnhovered()
    {
        this.OutlineEffect.enabled = false;
    }
}
