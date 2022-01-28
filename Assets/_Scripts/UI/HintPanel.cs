using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.UI.Models;
using TMPro;
using UnityEngine;

namespace _Scripts.UI
{
    public class HintPanel : MonoBehaviour
    {
        private List<Hint> ActiveHints = new List<Hint>();
        [SerializeField] private UIChannel _UIChannel;
        [SerializeField] private TextMeshProUGUI HintPrefab;
        [SerializeField] private Transform Visuals;

        private List<TextMeshProUGUI> _hintTexts = new List<TextMeshProUGUI>();

        private void Awake()
        {
            _UIChannel.OnShowHintEvent += OnShowHintEvent;
            _UIChannel.OnHideHintEvent += OnHideHintEvent;
        }

        private void OnHideHintEvent(string guid)
        {
            ActiveHints = ActiveHints.Where(x => x.HintGuid != guid).ToList();
            UpdateUI();
        }

        private void OnShowHintEvent(Hint arg0)
        {
            ActiveHints.Add(arg0);

            UpdateUI();
        }

        private void Start()
        {
            UpdateUI();
        }

        public void UpdateUI()
        {
            foreach (var hintText in _hintTexts)
            {
                Destroy(hintText);
            }

            var i = 0;
            
            foreach (var hint in ActiveHints)
            {
                var text = Instantiate(HintPrefab, Visuals);
                text.SetText(hint.HintText);
                i++;

                _hintTexts.Add(text);
            }
        }
    }
}