using SUPERCharacter;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tumo.Managers
{
    public class GameManager : MonoBehaviour
    {
        private int _frameCounter = 0;

        public GameObject PlayerRef;
        public Camera MainCamera;

        public List<BaseObjective> Objectives = new List<BaseObjective>();
        public BaseObjective NearestObjective = null;

        public Camera MinimapCam;
        public HelperPreset HelperPreset;

        private void Start()
        {
            this.PlayerRef = FindObjectOfType<SUPERCharacterAIO>().gameObject;
            this.MainCamera = this.PlayerRef.GetComponentInChildren<Camera>();
        }

        public void AddObjective(BaseObjective objective)
        {   
            this.Objectives.Add(objective);
        }

        private void Update()
        {
            // Update once every 30 frames
            if (this._frameCounter < 30)
                this._frameCounter++;

            this._frameCounter = 0;

            this.MinimapCam.transform.position = new Vector3(
                PlayerRef.transform.position.x,
                MinimapCam.transform.position.y,
                PlayerRef.transform.position.z);

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

            UIManager.Instance.UpdateCompasNeedle(this.NearestObjective, this.PlayerRef.transform);
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
        }
    }
}
