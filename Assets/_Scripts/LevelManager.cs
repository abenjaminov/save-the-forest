using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjects;
using _Scripts.ScriptableObjects.Channels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts
{
    [Serializable]
    public class SwitchLevelInfo
    {
        public GameAction SwitchLevelAction;
        public Transform NewLocation;
        public GameObject OldLevel;
        public GameObject NewLevel;

    }
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameChannel _GameChannel;
        [SerializeField] private CombatChannel _CombatChannel;
        [SerializeField] private GameObject _Player;
        [SerializeField] private List<SwitchLevelInfo> _SwitchLevelInfos;

        private SwitchLevelInfo _currentSwitchLevelInfo;
        
        private void Awake()
        {
            _GameChannel.OnActionEvent += OnActionEvent;
            _CombatChannel.DeathEvent += DeathEvent;
        }

        private void DeathEvent(Health arg0)
        {
            if (!arg0.CompareTag("Player")) return;
            
            _Player.transform.position = _currentSwitchLevelInfo.NewLocation.position;
            _Player.transform.rotation = _currentSwitchLevelInfo.NewLocation.rotation;
            arg0.HP = 5;
            arg0.Hit(0);
        }

        private void OnActionEvent(GameAction arg0)
        {
            var switchLevelInfo = _SwitchLevelInfos.FirstOrDefault(x => x.SwitchLevelAction.Guid == arg0.Guid);
            
            if (switchLevelInfo == null) return;

            _currentSwitchLevelInfo = switchLevelInfo;
            StartCoroutine(SwitchLevel(switchLevelInfo));
        }

        IEnumerator SwitchLevel(SwitchLevelInfo switchLevelInfo)
        {
            switchLevelInfo.NewLevel.SetActive(true);
            yield return new WaitForEndOfFrame();
            _Player.transform.position = switchLevelInfo.NewLocation.position;
            _Player.transform.rotation = switchLevelInfo.NewLocation.rotation;
            yield return new WaitForEndOfFrame();
            switchLevelInfo.OldLevel.SetActive(false);
        }
    }
}