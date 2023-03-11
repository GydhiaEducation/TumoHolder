using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tumo.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Events
        public delegate void ObjectiveHighlighted(BaseObjective objective);
        public delegate void GameEvent();

        public event ObjectiveHighlighted OnObjectiveFocused;
        public event GameEvent OnObjectiveUnfocused;

        public void FireObjectiveFocused(BaseObjective objective)
        {
            this.SelectedObjective = objective;
            this.SelectedObjective.OnHovered();

            this.OnObjectiveFocused?.Invoke(objective);
        }

        public void FireObjectiveUnfocused()
        {
            if (this.SelectedObjective != null)
                this.SelectedObjective.OnUnhovered();

            this.OnObjectiveUnfocused?.Invoke();
        }
        #endregion


        public readonly static string ObjectiveLayer = "Objectives";
        public readonly static float RaycastDistance = 2.2f;

        public int _objectivesLayer;
        private int _frameCounter = 0;

        public GameObject PlayerRef;
        public Camera MainCamera;

        public List<BaseObjective> Objectives = new List<BaseObjective>();
        public BaseObjective NearestObjective = null;

        public HelperPreset HelperPreset;

        public BaseObjective SelectedObjective = null;
        
        private void Start()
        {
            this.PlayerRef.GetComponent<Transform>();
            
            this.PlayerRef = FindObjectOfType<SUPERCharacterAIO>().gameObject;
        }

        public void AddObjective(BaseObjective objective)
        {   
            this.Objectives.Add(objective);
        }

        private void Update()
        {
            int layerMask = 1 << this._objectivesLayer;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit t, RaycastDistance, layerMask))
            {
                if(t.collider != null)
                {
                    var obj = t.collider.GetComponent<BaseObjective>();
                    if (!obj.IsPickedUp)
                    {
                        if (this.SelectedObjective != null)
                            this.SelectedObjective.OnUnhovered();

                        this.FireObjectiveFocused(obj);
                    }
                }
            }
            else
            {
                if(this.SelectedObjective != null)
                {
                    this.FireObjectiveUnfocused();
                    this.SelectedObjective = null;
                }
            }

            if (this.SelectedObjective != null && Input.GetKeyUp(KeyCode.E))
            {
                this.SelectedObjective.Pickup();
                this.SelectedObjective = null;
                this.FireObjectiveUnfocused();
            }

            // Update once every 30 frames
            if (this._frameCounter < 30)
                this._frameCounter++;

            this._frameCounter = 0;

            this.UpdateNearestObjective();
        }

        public void UpdateNearestObjective()
        {
            if (this.Objectives.Count == 0)
                return;
                
            int nearestIndex = 0;
            float currNearest = float.MaxValue;


            for (int i = 0; i < this.Objectives.Count; i++)
            {
                float dist = Vector3.SqrMagnitude(this.PlayerRef.transform.position - this.Objectives[i].transform.position);
                if (dist < currNearest)
                {
                    currNearest = dist;
                    nearestIndex = i;
                }
            }

            this.NearestObjective = this.Objectives[nearestIndex];
        }

        public static GameManager Instance { get; private set; }
        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            int lNb = LayerMask.NameToLayer(ObjectiveLayer);

            // Means the layer doesn't exist
            if(lNb == -1)
            {
                new LayerCreator().AddNewLayer(ObjectiveLayer);
            }

            this._objectivesLayer = LayerMask.NameToLayer(ObjectiveLayer);
        }
    }
}
