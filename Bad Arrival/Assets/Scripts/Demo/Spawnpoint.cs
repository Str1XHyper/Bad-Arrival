using UnityEngine;

public class Spawnpoint : MonoBehaviour
{
    #region Singleton
    public static Spawnpoint instance;

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            instance = this;
        }
    }
    #endregion
}
