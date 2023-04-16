using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {
    public CharacterData_SO characterData;
    public AttackData_SO attackData;

#region Read from CharacterData_SO
    public int MaxHealth {
        get => characterData == null ? 0 : characterData.maxHealth;
        set => characterData.maxHealth = value;
    }

    public int CurrentHealth {
        get => characterData == null ? 0 : characterData.currentHealth;
        set => characterData.currentHealth = value;
    }

    public int BaseDefence {
        get => characterData == null ? 0 : characterData.baseDefence;
        set => characterData.baseDefence = value;
    }

    public int CurrentDefence {
        get => characterData == null ? 0 : characterData.currentDefence;
        set => characterData.currentDefence = value;
    }
#endregion


#region Character Combat
    public void TakeDamage(CharacterStats attacker, CharacterStats defender) {
        int damage = Math.Max(attacker.CurrentDamage() - CurrentDefence, 0);
        CurrentHealth = Math.Max(CurrentHealth - damage, 0);

        if (attacker.attackData.isCritical) {
            defender.GetComponent<Animator>().SetTrigger("Hit");
        }

        // TODO: Update UI
        // TODO: 经验升级
    }

    private int CurrentDamage() {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if (attackData.isCritical) {
            coreDamage *= attackData.criticalMultiplier;
            Debug.Log("暴击！ " + coreDamage);
        }
        return (int)coreDamage;
    }


#endregion

}