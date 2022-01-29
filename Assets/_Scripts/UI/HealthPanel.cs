using _Scripts.ScriptableObjects.Channels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPanel : MonoBehaviour
{
    [SerializeField] List<Image> Hearts;
    [SerializeField] CombatChannel CombatChannel;
    [SerializeField] Health PlayerHealth;

    private void Awake()
    {
        CombatChannel.HitEvent += OnPlayerHealthChange;
    }

    private void OnPlayerHealthChange(HitObject hitObject)
    {
        if (hitObject.ObjectHealth.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                if (i < PlayerHealth.HP)
                {
                    Hearts[i].enabled = true;
                }
                else
                {
                    Hearts[i].enabled = false;
                }
            }
        }
    }
}
