using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GAMESTATE
{
    NONE = 0,
    INGAME,
    ISUPGRADE,
    GAMEOVER,
    CLEAR
}

public static class GameManagement
{
    private static GAMESTATE gameState;
    
    /// <summary>
    /// GANESTATEを変更する
    /// </summary>
    public static GAMESTATE GameState { get => gameState; set => gameState = value; }

    /// <summary>
    /// シーン遷移を行う
    /// 別スクリプトではSceneManagerを使用せず、このメソッドを呼び出す。
    /// シーン遷移はSceneChanger.ChangeSceneを呼び出して行う。
    /// </summary>
    /// <param name="sceneName">遷移先のシーン名</param>
    public static void LoadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
