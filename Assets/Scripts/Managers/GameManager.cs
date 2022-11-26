using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tumo.Managers
{
    public class GameManager : MonoBehaviour
    {
        public List<BaseObjective> Objectives = new List<BaseObjective>();



        public void AddObjective(BaseObjective objective)
        {
            this.Objectives.Add(objective);
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
