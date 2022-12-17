using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer), typeof(MeshFilter))]
public class Interaction : MonoBehaviour, IDrawInterface
{
    public event Action OnInteraction;

    public event Action<string> OnDrawText;
    public event Action<bool> OnDrawCanvas;

    bool isInTrigger;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInTrigger)
        {
	    Debug.Log("Tu as utilisé la touche E");
            OnInteraction?.Invoke();
            OnDrawCanvas?.Invoke(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!isInTrigger)
            {
                OnDrawText?.Invoke("Appuie sur E pour interagir");
                OnDrawCanvas?.Invoke(true);
            }
            isInTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnDrawCanvas?.Invoke(false);
            isInTrigger = false;
        }
    }
}