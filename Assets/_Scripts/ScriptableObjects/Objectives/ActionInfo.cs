using System;
using UnityEngine;

namespace _Scripts.ScriptableObjects.Objectives
{
    [Serializable]
    public class ActionInfo
    {
        public string ActionGuid;
        [HideInInspector] public bool Happened;
    }
}