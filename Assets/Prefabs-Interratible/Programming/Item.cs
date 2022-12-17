using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Item : MonoBehaviour, IDrawInterface
{
    public event Action OnItemFound;

    public event Action<string> OnDrawText;
    public event Action<bool> OnDrawCanvas;

    GameObject _fx;

    bool isFound;

    #region Event Subscription

    void OnEnable()
    {
        transform.GetComponentInChildren<Interaction>().OnInteraction += FindItem;
    }

    void OnDisable()
    {
        transform.GetComponentInChildren<Interaction>().OnInteraction -= FindItem;
    }

    #endregion

    #region Initialization

    void Awake()
    {
        if (GetComponentInChildren<ParticleSystem>())
        {
            _fx = GetComponentInChildren<ParticleSystem>().gameObject;
            _fx.SetActive(false);
        }
    }

    #endregion

    public void FindItem()
    {
        if (isFound)
            return;
        
        isFound = true;
        OnItemFound?.Invoke();
        OnDrawText?.Invoke("Bravo ! Tu as trouvé l'objet " + gameObject.name);
        OnDrawCanvas?.Invoke(true);
        if (_fx != null)
            _fx.SetActive(true);
        Destroy(this.gameObject, 4);
    }
}