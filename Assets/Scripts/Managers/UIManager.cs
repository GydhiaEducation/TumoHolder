using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tumo.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Tumo.Managers
{
    public class UIManager : MonoBehaviour
    {
        public delegate void ObjectiveHighlighted(BaseObjective objective);
        public delegate void GameEvent();

        public event ObjectiveHighlighted OnObjectiveFocused;
        public event GameEvent OnObjectiveUnfocused;

        #region INTERRACTABLE
        public int VerticalOffset = 10;
        [SerializeField] private TextMeshProUGUI InterractableText;
        #endregion

        #region COMPAS
        public RectTransform CompasNeedle;
        #endregion

        #region HEATBAR
        public Slider Heatbar;
        public Image HeatbarFill;
        public int MinDistanceFromObjective;
        public Color ColdColor;
        public Color HotColor;
        #endregion

        public static UIManager Instance { get; private set; }
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

        private void Start()
        {
            this.InterractableText.gameObject.SetActive(false);

            this.OnObjectiveFocused += _showObjectiveText;
            this.OnObjectiveUnfocused += _hideObjectiveText;

            this.Heatbar.minValue = 0;
            this.Heatbar.maxValue = MinDistanceFromObjective;
            this.Heatbar.value = 0;
        }

        private void _showObjectiveText(BaseObjective selectedObjective)
        {
            this.InterractableText.text = selectedObjective.TextToShow;

            Vector3 worldPos = Camera.main.WorldToScreenPoint(selectedObjective.transform.position);
            Vector2 finalPos = new Vector2(worldPos.x, worldPos.y + VerticalOffset);
            ((RectTransform)this.InterractableText.gameObject.transform).anchoredPosition = finalPos;

            this.InterractableText.gameObject.SetActive(true);
        }

        private void _hideObjectiveText()
        {
            this.InterractableText.gameObject.SetActive(false);
        }

        public void UpdateCompasNeedle(BaseObjective objective, Transform Player)
        {
            float angle = Vector3.Angle(Player.forward, objective.transform.position);

            this.CompasNeedle.eulerAngles = new Vector3(0f, 0f, angle);
        }

        public void UpdateHeatbar(BaseObjective objective, Transform Player)
        {
            float dist = Vector3.Distance(Player.transform.position, objective.transform.position);

            if (dist > this.MinDistanceFromObjective)
                dist = 0;

            this.Heatbar.value = dist;
            this.HeatbarFill.color = Color.Lerp(this.ColdColor, this.HotColor, (dist / this.MinDistanceFromObjective));
        }
    }
}