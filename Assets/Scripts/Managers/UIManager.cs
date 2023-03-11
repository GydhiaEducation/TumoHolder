using Newtonsoft.Json.Converters;
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
        #region INTERRACTABLE
        public int VerticalOffset = 10;
        [SerializeField] private TextMeshProUGUI _interractableText;
        #endregion

        public GameObject TearedPage;
        public TextMeshProUGUI PageText;

        private bool _focusingPage = false;

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
            this._interractableText.gameObject.SetActive(false);

            bool minimap = GameManager.Instance.HelperPreset.MinimapActive;
            bool heatbar = GameManager.Instance.HelperPreset.BarreActive;
            bool compas = GameManager.Instance.HelperPreset.BoussolleActive;

            GameManager.Instance.OnObjectiveFocused += _showObjectiveText;
            GameManager.Instance.OnObjectiveUnfocused += _hideObjectiveText;
        }

        private void _showObjectiveText(BaseObjective selectedObjective)
        {
            this._interractableText.text = selectedObjective.TextToShow;
            this._interractableText.gameObject.SetActive(true);
        }

        private void _hideObjectiveText()
        {
            this._interractableText.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (this._focusingPage)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.HidePage();
                }
            }
        }

        public void ShowPage(PageObjective Page)
        {
            this._focusingPage = true;

            Time.timeScale = 0;

            this.TearedPage.SetActive(true);
            this.PageText.text = Page.PageDescription;
        }

        public void HidePage()
        {
            this._focusingPage = false;

            Time.timeScale = 1;

            this.TearedPage.SetActive(false);
        }
    }
}