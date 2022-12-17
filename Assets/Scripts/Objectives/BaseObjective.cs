using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BaseObjective : MonoBehaviour
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

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.FireObjectiveFocused(this);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.FireObjectiveUnfocused();
        }
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
