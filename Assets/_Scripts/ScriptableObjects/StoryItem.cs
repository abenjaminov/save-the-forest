using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Story Item", menuName = "Story Item", order = 0)]
    public class StoryItem : ScriptableObject
    {
        public string Guid;
        public List<string> StoryParts;
    }
}