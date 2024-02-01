using PullAnimals.Singleton;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ESCを押したときにゲームを落とす処理
    /// </summary>
    public class GameBreaker : DDMonoSingletonBase<GameBreaker>
    {
        private void Update()
        {
            //アプリを強制終了させる
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
