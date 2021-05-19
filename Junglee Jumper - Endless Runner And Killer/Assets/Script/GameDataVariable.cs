using UnityEngine;

public class GameDataVariable : MonoBehaviour
{
  public static int[] dataVariables { get; set; }

    private void Awake()
    {
        dataVariables = new int[6];
    }
}
