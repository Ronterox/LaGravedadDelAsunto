using General.Utilities;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        CharacterCombat playerCombat=playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
    protected override void Update()
    {
        if (m_PlayerOnRange && PlayerInput.Instance.Attack) Interact();
    }
    protected override void OnEnterTrigger(Collider other)
    {
        
    }

    protected override void OnExitTrigger(Collider other)
    {
        
    }

  
}
