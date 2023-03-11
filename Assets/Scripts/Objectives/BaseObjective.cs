using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(BaseObjective))]
public class BaseObjective : MonoBehaviour
{
    public string TextToShow;
    [HideInInspector]
    public Outline OutlineEffect;
    [HideInInspector]
    public ParticleSystem PickupParticles;

    [HideInInspector]
    public bool IsPickedUp = false;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Objectives");
        this.OutlineEffect.enabled = false;

        this.OutlineEffect = this.GetComponentInChildren<Outline>();
        this.PickupParticles = this.GetComponentInChildren<ParticleSystem>();

        this.PickupParticles.gameObject.SetActive(false);

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

    public virtual void Pickup()
    {
        this.IsPickedUp = true;
        StartCoroutine(this._playParticle());
    }

    private IEnumerator _playParticle()
    {
        this.PickupParticles.gameObject.SetActive(true);
        this.PickupParticles.Play();

        yield return new WaitForSeconds(this.PickupParticles.main.duration);

        this.gameObject.SetActive(false);
    }
}
