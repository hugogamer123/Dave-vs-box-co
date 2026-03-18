using System.Collections;
using UnityEngine;

public class BossGeneral : MonoBehaviour
{
    public int Phase = 1;

    public GameObject LHand;
    public GameObject RHand;

    Hand leftHand;
    Hand rightHand;

    public bool startBoss;

    public bool Phase1Attacking;
    public bool Phase1Finished;
    public bool Phase2Attacking;

    public bool P1CouStoped = false;
    bool phase1CoroutineRunning = false;

    void Awake()
    {
        if (LHand != null) leftHand = LHand.GetComponent<Hand>();
        if (RHand != null) rightHand = RHand.GetComponent<Hand>();
    }

    void Update()
    {
        if (leftHand == null || rightHand == null)
        {
            if (LHand != null) leftHand = LHand.GetComponent<Hand>();
            if (RHand != null) rightHand = RHand.GetComponent<Hand>();
        }

        if (leftHand == null || rightHand == null)
        {
            Debug.LogWarning("BossGeneral needs both hands assigned");
            return;
        }

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

        Debug.LogError($"IS HANDL DEAD: {leftDead}");
        Debug.LogError($"IS HANDR DEAD: {rightDead}");

        if (leftDead && rightDead)
        {
            Phase1Finished = true;
            Phase1Attacking = false;
            Phase2Attacking = true;
            Debug.LogError("ALL HANDS HAVE DIED AND SHOULD NOT WORK ANYMORE");
        }
    }

    IEnumerator Phase1Count()
    {
        phase1CoroutineRunning = true;

        if (Phase1Finished)
        {
            Debug.LogError("The Dead hand shouldnt attack because the phase1 finished");
            phase1CoroutineRunning = false;
            yield break;
        }

        while (!Phase1Attacking && !Phase1Finished)
        {
            Debug.Log("Coroutine has started");

            int AttackChance = Random.Range(-1, 2);
            if (AttackChance == 0)
            {
                Debug.Log("Attack chance failed.. Restarting Coroutine");
                yield return new WaitForSeconds(1f);
                P1CouStoped = true;
                break;
            }

            bool leftDead = leftHand.IsDead;
            bool rightDead = rightHand.IsDead;
            int HandUsed = -1;

            if (!leftDead && !rightDead)
            {
                HandUsed = Random.Range(0, 2);
            }
            else if (leftDead && !rightDead)
            {
                HandUsed = 1;
            }
            else if (!leftDead && rightDead)
            {
                HandUsed = 0;
            }
            else
            {
                Phase1Finished = true;
                break;
            }

            Hand selectedHand = (HandUsed == 0) ? leftHand : rightHand;
            if (selectedHand != null && !selectedHand.IsDead)
            {
                Debug.Log(HandUsed);
                selectedHand.AttackPhase1();
                Phase1Attacking = true;
            }

            break;
        }

        phase1CoroutineRunning = false;
    }
    void Restart(IEnumerator myCor)
    {
        string corStr = $"{myCor}";
        Debug.Log($"Restarting Coroutine: {corStr}");
        StopCoroutine(corStr);
        StartCoroutine(corStr);
    }
}
