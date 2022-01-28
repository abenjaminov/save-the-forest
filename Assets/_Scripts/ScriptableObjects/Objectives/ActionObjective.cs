using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    [CreateAssetMenu(fileName = "Action Objective", menuName = "Objectives/Action", order = 0)]
    public class ActionObjective : Objective
    {
        public List<ActionInfo> ActionInfos;

        private void OnDisable()
        {
            _ObjectivesChannel.OnActionEvent -= OnActionEvent;
        }

        private void OnEnable()
        {
            _ObjectivesChannel.OnActionEvent += OnActionEvent;
        }

        private void OnActionEvent(string actionGuid)
        {
            var actionInfo = ActionInfos.FirstOrDefault(x => x.ActionGuid == actionGuid);

            if (actionInfo == null) return;

            actionInfo.Happened = true;

            if (ActionInfos.Any(x => !x.Happened)) return;
            
            this.Complete();
        }
    }
}