using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitcity : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") )
        {
            ArchievementsManager.Instance.UpdateAchievement("achievement1", 1);
        }
    }
}
