using System.Collections;
using UnityEditor.EditorTools;
using UnityEngine;

public class BossGeneral : MonoBehaviour
{
    public int Phase = 1;

    public GameObject LHand;
    public GameObject RHand;

    [SerializeField] Hand leftHand;
    [SerializeField] Hand rightHand;

    public bool startBoss;

    public bool Phase1Attacking;
    public bool Phase1Finished;
    public bool Phase2Attacking;

    public bool P1CouStoped = false;
    bool phase1CoroutineRunning = false;


    public string whatAttackuse;

    void Update()
    {

        if (startBoss && !phase1CoroutineRunning && !Phase1Finished)
        {
            StartCoroutine(Phase1Count());
        }

        if (P1CouStoped && !phase1CoroutineRunning && !Phase1Finished)
        {
            P1CouStoped = false;
            StartCoroutine(Phase1Count());
        }

        bool leftDead = leftHand.IsDead;
        bool rightDead = rightHand.IsDead;

        if (leftDead && rightDead)
        {
            Phase1Finished = true;
            Phase1Attacking = false;
            Phase2Attacking = true;
        }
    }

    IEnumerator Phase1Count()
    {
        phase1CoroutineRunning = true;

        if (Phase1Finished)
        {
            phase1CoroutineRunning = false;
            yield break;
        }

        while (!Phase1Attacking && !Phase1Finished)
        {

            int AttackChance = Random.Range(-1, 2);
            if (AttackChance == 0)
            {
                yield return new WaitForSeconds(1f);
                P1CouStoped = true;
                break;
            }

            bool leftDead = leftHand.IsDead;
            bool rightDead = rightHand.IsDead;
            int HandUsed = -1;

            //Attempting to randomly generate a phase to use
            int WhatAttackUseInt = Random.Range(0, 5);
            Debug.Log($"Attack Int: {WhatAttackUseInt}");

            if(WhatAttackUseInt == 0 || WhatAttackUseInt == 1)
            {
                if (!leftDead && !rightDead)
                {
                    HandUsed = Random.Range(0, 2);
                    whatAttackuse = "slam";
                }
                else if (leftDead && !rightDead)
                {
                    HandUsed = 1;
                    whatAttackuse = "slam";
                }
                else if (!leftDead && rightDead)
                {
                    HandUsed = 0;
                    whatAttackuse = "slam";
                }
                else
                {
                    Phase1Finished = true;
                    break;
                }
            }
            else if(WhatAttackUseInt == 2 || WhatAttackUseInt == 3 || WhatAttackUseInt == 4)
            {
                if (!leftDead && !rightDead)
                {
                    HandUsed = 3;
                    whatAttackuse = "slide";
                }
                else if (leftDead && !rightDead)
                {
                    HandUsed = 1;
                    whatAttackuse = "slide";
                }
                else if (!leftDead && rightDead)
                {
                    HandUsed = 0;
                    whatAttackuse = "slide";
                }
                else
                {
                    Phase1Finished = true;
                    break;
                }
            }
            
            Hand selectedHand = null;
            Hand additionalHand = null;
            if(HandUsed == 0) {selectedHand = leftHand;}
            else if(HandUsed == 1) {selectedHand = rightHand;}
            else if (HandUsed == 3) {selectedHand = leftHand; additionalHand = rightHand;}
            if (selectedHand != null && !selectedHand.IsDead)
            {
                Debug.Log(HandUsed);
                if(whatAttackuse == "slam")
                {
                    selectedHand.AttackPhase1();
                    if(additionalHand != null) {additionalHand.AttackPhase1();}
                }
                else if (whatAttackuse == "slide")
                {
                    selectedHand.SlideAttack();
                    if(additionalHand != null) {additionalHand.SlideAttack();}
                }
                Phase1Attacking = true;
            }



            break;
        }

        phase1CoroutineRunning = false;
    }


    IEnumerator Phase2()
    {
        yield return null;
    }
}


