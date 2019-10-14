using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class LevelLogic: IUpdatable
{
    MainLogic m_MainLogic;

    UnityAction<int> m_OnGameStartCallback;

    UnityAction<int> m_OnLevelStartCallback;

    int m_CurrentLevel = 0;
    int m_LivesOnLevel = 0;
    const int c_minLives = 3;
    const int c_maxLives = 5;
    const int c_minEnemies = 1;
    const int c_maxEnemies = 3;
    const int c_EnemyTerritory = 3;
    int[] m_EnemiesOnLevel = new int [c_EnemyTerritory];

    public int GetLives(){
        return m_LivesOnLevel;
    }

    public int[] GetEnemies(){
        return m_EnemiesOnLevel;
    }

    public int GetCurrentLevel(){
        return m_CurrentLevel;
    }

#region Callbacks

    // game start
    public void AddGameStartListener(UnityAction<int> listener){
        m_OnGameStartCallback += listener;
    }

    public void RemoveGameStartListener(UnityAction<int> listener){
        m_OnGameStartCallback -= listener;
    }

    public void OnGameStart(int level){
        if (m_OnGameStartCallback != null)
            m_OnGameStartCallback(level);
    }

    // level start  
    public void AddLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback += listener;
    }

    public void RemoveLevelStartListener(UnityAction<int> listener){
        m_OnLevelStartCallback -= listener;
    }

    public void OnLevelStart(int level){
        if (m_OnLevelStartCallback != null)
            m_OnLevelStartCallback(level);
    }
#endregion

    public LevelLogic(MainLogic main){

        m_MainLogic = main;

    }

    public void StartGame(int level){
        m_CurrentLevel = level;

        GenerateLevelData();

        OnGameStart(m_CurrentLevel);
    }

    public void RestartCurrLevel(){

        OnLevelStart(m_CurrentLevel);
    }

    public void MoveNext(){
        m_CurrentLevel++;

        GenerateLevelData();

        OnLevelStart(m_CurrentLevel);
    }

    void GenerateLevelData(){

        // Debug.Log("GenerateLevelData " + m_CurrentLevel);

        m_LivesOnLevel = UnityEngine.Random.Range(c_minLives, c_maxLives+1);

        // Debug.Log("m_LivesOnLevel " + m_LivesOnLevel);

        for (int i=0; i<m_EnemiesOnLevel.Length; i++){
            m_EnemiesOnLevel[i] = UnityEngine.Random.Range(c_minEnemies, c_maxEnemies+1);

            // Debug.Log("m_EnemiesOnLevel " + m_EnemiesOnLevel[i]);
        }
    }

    public void UpdateMe(float deltaTime){

        // UpdateTimers(deltaTime);

    }

    // void UpdateTimers(float deltaTime){

    // }
}
