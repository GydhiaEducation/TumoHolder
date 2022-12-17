using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer), typeof(MeshFilter))]
[RequireComponent(typeof(DoorOpening))]
public class DoorOpening : MonoBehaviour, IDrawInterface
{
    short nbrItemFound;
    [SerializeField]
    [Header ("Rentre le nombre d'objet à trouver pour terminer le niveau")]
    [Space][Space]
    short nbrItemNeeded = 3;

    // state
    bool isOpenable;

    //UI
    TextUI _textUI;

    public event Action<string> OnDrawText;
    public event Action<bool> OnDrawCanvas;

    #region Event Subscription

    void OnEnable()
    {
        Item[] items = FindObjectsOfType<Item>();
        short count = 0;

        while (count < items.Length)
        {
            items[count].OnItemFound += FindItem;
            count++;
        }

        transform.GetComponentInChildren<Interaction>().OnInteraction += OpenDoor;

        if (nbrItemNeeded > count)
        {
            Debug.LogWarning("Fais attention, tu n'as pas assez d'objets interactifs dans ta scène pour terminer ton niveau");
        }
    }

    void OnDisable()
    {
        Item[] items = FindObjectsOfType<Item>();
        short count = 0;

        while (count < items.Length)
        {
            items[count].OnItemFound -= FindItem;
            count++;
        }

        transform.GetComponentInChildren<Interaction>().OnInteraction -= OpenDoor;
    }

    #endregion

    void FindItem()
    {
        nbrItemFound++;
        if (nbrItemFound >= nbrItemNeeded)
            isOpenable = true;

        //Debug.Log("J'arrive jusqu'ici, voici mon compteur : " + nbrItemFound + " et je suis ouvert : " + isOpenable);
    }

    void OpenDoor()
    {
        if (isOpenable)
        {
            OnDrawText?.Invoke("Bravo, tu as fini le niveau !");
            OnDrawCanvas?.Invoke(true);
        }
        else
        {
            OnDrawText?.Invoke("Il te manque des objets !");
            OnDrawCanvas?.Invoke(true);
        }

        Invoke("EndLevel", 4);
    }

    void EndLevel()
    {
        OnDrawCanvas?.Invoke(false);
    }
}