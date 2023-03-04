using System.Collections;
using System.Collections.Generic;
using Tumo.Managers;
using UnityEngine;

public class BaseObjective : MonoBehaviour
{
    public string TextToShow;
    public Outline OutlineEffect;
    public ParticleSystem PickupParticles;

    public bool IsPickedUp = false;

    private void Start()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Objectives");
        this.OutlineEffect.enabled = false;

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

    public void Pickup()
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
