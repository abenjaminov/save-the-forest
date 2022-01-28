using System;
using System.Collections;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using _Scripts.ScriptableObjects.Channels.Input;
using _Scripts.ScriptableObjects.Channels.Input.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.UI
{
    public class StoryPanel : MonoBehaviour
    {
        [SerializeField] private InputChannel _InputChannel;
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private float timeBetweenLetters;
        [SerializeField] private TextMeshProUGUI _storyText;
        [SerializeField] private GameObject _Visuals;
        
        private StoryItem CurrentStoryItem;
        private bool _skipStory;

        private void Awake()
        {
            _InputChannel.SubscribeAction(InputActionTypes.SkipStory, OnSkipStory);
            _GameChannel.OnShowStoryEvent += OnShowStoryEvent;
            _storyText.text = "";
            _Visuals.SetActive(false);
        }

        private void OnSkipStory(InputActionOptions arg0)
        {
            _skipStory = true;
        }

        private void OnShowStoryEvent(StoryItem storyItem)
        {
            CurrentStoryItem = storyItem;
            _Visuals.SetActive(true);
            StartCoroutine(nameof(WriteStory));
        }

        IEnumerator WriteStory()
        {
            foreach (var storyPart in CurrentStoryItem.StoryParts)
            {
                for (int i = 1; i <= storyPart.Length; i++)
                {
                    if (_skipStory)
                    {
                        _storyText.text = storyPart;
                        _skipStory = false;
                        break;
                    }
                    else
                    {
                        _storyText.text = storyPart.Substring(0, i);    
                    }
                    
                    yield return new WaitForSeconds(timeBetweenLetters);
                }

                while (!_skipStory)
                {
                    yield return new WaitForEndOfFrame();
                }

                _skipStory = false;
            }

            _Visuals.SetActive(false);
            _GameChannel.OnStoryTold(CurrentStoryItem);
        }
    }
}