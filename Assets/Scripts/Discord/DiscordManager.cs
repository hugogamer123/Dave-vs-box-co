using UnityEngine;
using Discord;
using Unity.Collections;
public class DiscordManager : MonoBehaviour
{
    public long applicationId;
    public Discord.Discord discord;

    [Header("Discord Status")]
    public string details;
    public string state;
    [Space]
    public string largeImage;
    public string Text;

    private void Start()
    {
        discord = new Discord.Discord(applicationId, (ulong)Discord.CreateFlags.NoRequireDiscord);
    }

    private void OnApplicationQuit()
    {
        discord.Dispose();
    }

    public void UpdateStatus()
    {
        try 
        {
            var acivityManager = discord.GetActivityManager();
            var activity = new Discord.Activity
            {
                Details = details,
                State = state,
                Assets =
            {
                LargeImage = largeImage,
                LargeText = Text
            }
            };

            acivityManager.UpdateActivity(activity, result =>
            {
                if (result == Discord.Result.Ok)
                {
                    //Debug.Log("Discord status updated successfully.");
                }
                else
                {
                    Debug.LogError($"Failed to update Discord status: {result}");
                }
            });
        }
        catch
        {
            discord.Dispose();
        }
    }

    private void LateUpdate()
    {
        UpdateStatus();
        discord.RunCallbacks();
    }
}

