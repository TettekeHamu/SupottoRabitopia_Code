namespace PullAnimals.SceneLoader
{
    /// <summary>
    /// シーン開始時にゲームのセットアップをおこなう処理を持つInterface、神クラスに持たせる
    /// </summary>
    public interface ISetUpScene
    {
        /// <summary>
        /// シーン切り替え時におこないたい処理
        /// </summary>
        public void SetUpScene();
    }
}
