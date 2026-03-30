using UnityEngine;

public class SaveManager : MonoBehaviour
{
    /*
     * SUMMARY
     * 
     * THIS FILE CONTAINS ALL THE VARIABLES AND OTHER STUFF THAT WILL BE SAVED AND LOADED BY UNITY PLAYERPREFS.
     */

    private void Start()
    {
        int startPos = PlayerPrefs.GetInt("StartArea");
        Debug.Log(startPos);
        if (startPos == 0)
        {
            //StartArea 1 is the tutorial (start of game), StartArea 2 is in the haven.
            PlayerPrefs.SetInt("StartArea", 1);
        }
    }

    public void SaveGame()
    {

    }

    public void LoadGame()
    {

    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    //  Used to save bools. EG: PlayerPrefs.SetInt("INSERT NAME FOR SAVED VALUE", boolToInt(INSERT VARIABLE));
    int boolToInt(bool val)
    {
        if (val)
            return 1;
        else
            return 0;
    }

    //  Used to load bools. EG: INSERT VARIABLE = intToBool(PlayerPrefs.GetInt("INSERT NAME FOR SAVED VALUE"));
    bool intToBool(int val)
    {
        if (val != 0)
            return true;
        else
            return false;
    }

}
