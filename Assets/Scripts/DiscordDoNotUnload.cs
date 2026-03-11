using UnityEngine;

public class DiscordDoNotUnload : MonoBehaviour
{
    [SerializeField] GameObject discordObject;

    private void Awake()
    {
        GameObject spawnObject = Instantiate(discordObject);

        DontDestroyOnLoad(spawnObject);
    }
}
