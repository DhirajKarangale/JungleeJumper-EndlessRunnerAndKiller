using System.Collections;
using System;
using UnityEngine;

public class TimeCalculator : MonoBehaviour
{
    DateTime currentTime;
    DateTime oldTime;

    public string saveLocation;

    public static TimeCalculator instance;

    private void Awake()
    {
        instance = this;
        saveLocation = "loadsaveDate1";
    }

    public float CheckDate()
    {
        currentTime = System.DateTime.Now;
        string tempString = PlayerPrefs.GetString(saveLocation, "1");
        long tempLong = Convert.ToInt64(tempString);
        DateTime oldTime = DateTime.FromBinary(tempLong);
        print("Old Time : " + oldTime);
        TimeSpan differnce = currentTime.Subtract(oldTime);
        print("Differnce : " + differnce);

        return (float)differnce.TotalSeconds;
    }

    public void SaveTime()
    {
        PlayerPrefs.SetString(saveLocation, System.DateTime.Now.ToBinary().ToString());
        print("Saving this date to player prepd " + System.DateTime.Now);
    }
}
