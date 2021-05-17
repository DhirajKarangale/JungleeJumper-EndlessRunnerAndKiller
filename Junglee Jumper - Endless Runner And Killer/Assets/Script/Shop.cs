using UnityEngine;
using System;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text coinCountText;

    private void Start()
    {
        if (GameDataVariable.dataVariables[1] >= 1000)
        {
            coinCountText.text = string.Format("{0}.{1}K", Convert.ToInt32((GameDataVariable.dataVariables[1] / 1000)), int.Parse(((GameDataVariable.dataVariables[1] % 1000) / 100).ToString()[0].ToString()));
        }
        else
        {
            coinCountText.text = GameDataVariable.dataVariables[1].ToString();

        }
    }
}
