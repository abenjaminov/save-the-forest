using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    [Serializable]
    public class ActionInfo
    {
        public GameAction GameAction;
        [HideInInspector] public bool Happened;
    }
}