using UnityEngine;
using System.Collections.Generic;

public class HeartUI : MonoBehaviour
{
    public List<GameObject> AliveHearts = new List<GameObject>();
    public List<GameObject> DeadHearts = new List<GameObject>();

    public Movement movement;

    int HealthIndex = 4;

    public void RemoveHeart()
    {
        AliveHearts[HealthIndex].SetActive(false);
        HealthIndex--;
    }

    public void AddHeart()
    {
            HealthIndex++;
            AliveHearts[HealthIndex].SetActive(true);
    }

    private void Update()
    {
        if (HealthIndex <= 0)
        {
            movement.Die();
        }
    }
}
