using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Completed;



public class GameManager : MonoBehaviour {
    
    public static GameManager instance = null;
    public BoardManager boardScript;
    public int playerFoodPoints = 100;
    [HideInInspector] public bool playersTurn = true;
     public float turnDelay = 0.1f;


    private int level = 6;
    private List<Enemy> enemies;
    private bool enemiesMoving;

        void Awake()
        {
            if(instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            enemies = new List<Enemy>();
            boardScript = GetComponent<BoardManager>();
            InitGame();
        }

        void InitGame()
        {
            enemies.Clear();
            boardScript.SetupScene(level);
        }

        public void GameOver()
        {
            enabled = false;
        }

void Update()
		{
			if(playersTurn || enemiesMoving)
				return;
			StartCoroutine (MoveEnemies ());
		}

    	public void AddEnemyToList(Enemy script)
		{
			enemies.Add(script);
		}

        IEnumerator MoveEnemies()
		{
			//While enemiesMoving is true player is unable to move.
			enemiesMoving = true;
			
			//Wait for turnDelay seconds, defaults to .1 (100 ms).
			yield return new WaitForSeconds(turnDelay);
			
			//If there are no enemies spawned (IE in first level):
			if (enemies.Count == 0) 
			{
				//Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
				yield return new WaitForSeconds(turnDelay);
			}
			
			//Loop through List of Enemy objects.
			for (int i = 0; i < enemies.Count; i++)
			{
				//Call the MoveEnemy function of Enemy at index i in the enemies List.
				enemies[i].MoveEnemy ();
				
				//Wait for Enemy's moveTime before moving next Enemy, 
				yield return new WaitForSeconds(enemies[i].moveTime);
			}
			//Once Enemies are done moving, set playersTurn to true so player can move.
			playersTurn = true;
			
			//Enemies are done moving, set enemiesMoving to false.
			enemiesMoving = false;
		}
	}
