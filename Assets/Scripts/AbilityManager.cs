using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public int CurrentAbilityP1 = 0;
    public int CurrentAbilityP2 = 0;
    public BallBehaviorScript BBS;

    public float AbilityCooldownTimeP1 = 3f;
    public float AbilityCooldownTimeP2 = 3f;

    private bool isP1OnCooldown = false;
    private bool isP2OnCooldown = false;

    public void RunCurrentAbilityP1()
    {
        if (isP1OnCooldown) return;

        switch (CurrentAbilityP1)
        {
            case 1:
                HardHittingSwing(true);
                break;
            case 2:
                FreezeBall(true);
                break;
                
                
        }

        isP1OnCooldown = true;
        StartCoroutine(CooldownP1());
    }

    public void RunCurrentAbilityP2()  
    {
        if (isP2OnCooldown) return;

        switch (CurrentAbilityP2)
        {
            case 1:
                HardHittingSwing(false);
                break;
            case 2:
                FreezeBall(true);
                break;
            case 3:
                //slowtime
                break;
            case 4:
                //multipleballs
                break;
            case 5:
                //invisball
                break;


        }

        isP2OnCooldown = true;
        StartCoroutine(CooldownP2());
    }

    public void HardHittingSwing(bool isPlayer1)
    {
        BBS.ApplySpeedBoost(10f, 0.3f , 0);
    }

    public void FreezeBall(bool isPlayer1)
    {
        BBS.ApplySpeedBoost(-BBS.speed , 0.3f , 1);
    }

    public bool RequiresHit(int abilityId)
    {
        switch (abilityId)
        {
            case 1: return true;   // if a bility req hit set here
            default: return false;
        }
    }

    private IEnumerator CooldownP1()
    {
        yield return new WaitForSeconds(AbilityCooldownTimeP1);
        isP1OnCooldown = false;
    }

    private IEnumerator CooldownP2()   
    {
        yield return new WaitForSeconds(AbilityCooldownTimeP2);
        isP2OnCooldown = false;
    }

}
