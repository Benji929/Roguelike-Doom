using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager singleton { private set; get; }//Singleton
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    #endregion

    public PlayerController playerController;

    public enum GameState
    {
        init,
        pre_firstLevel,
        firstLevel
    }
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameState = GameState.init;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
