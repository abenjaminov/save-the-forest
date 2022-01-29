using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    [Serializable]
    public class ActionInfo
    {
        public GameAction GameAction;
        public int RepeatAmount = 1;
        [HideInInspector] public int AmountsLeft;
        [HideInInspector] public bool Happened;
    }
}