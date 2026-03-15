using System.Collections;
using UnityEngine;

public class BossGeneral : MonoBehaviour
{
    public int Phase = 1;
    public Hand hand;

    //The Two Hands
    public GameObject LHand;
    public GameObject RHand;

    public bool startBoss;

    public bool Phase1Attacking;

    public bool P1CouStoped = false;

    void Update()
    {
        if (startBoss)
        {
            StartCoroutine(Phase1Count());
        }
        if(P1CouStoped)
        {
            StartCoroutine(Phase1Count());
            P1CouStoped = false;
        }
    }

    IEnumerator Phase1Count()
    {
        while (!Phase1Attacking)
        {
            Debug.Log("Coroutine has started");
            //Returns if attack chance is false
            int AttackChance = Random.Range(-1, 2);
            if (AttackChance == 0)
            {
                Debug.Log("Attack chance failed.. Restarting Coroutine");
                yield return new WaitForSeconds(1f);
                P1CouStoped = true;
                yield break;
                Debug.Log("If this displays then the coroutine has failed to stop");
            }
            Debug.Log($"Attack chance : {AttackChance}, if \"0\" then the damm couroutine should have stopped");

            //Randomly selects a hand to attack with
            int HandUsed = Random.Range(0, 2);
            if (HandUsed == 0)
            {
                hand = LHand.GetComponent<Hand>();
            }
            else if(HandUsed == 1)
            {
                hand = RHand.GetComponent<Hand>();
            }
            Debug.Log(HandUsed);
            //Starts attack with the selected hand
            hand.Attack();
            Phase1Attacking = true;

        }
    }
    void Restart(IEnumerator myCor)
    {
        string corStr = $"{myCor}";
        Debug.Log($"Restarting Coroutine: {corStr}");
        StopCoroutine(corStr);
        StartCoroutine(corStr);
    }
}
