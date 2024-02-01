namespace PullAnimals.StatePattern
{
    /// <summary>
    /// 各Stateに実装させるInterface
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// 初期化用処理
        /// </summary>
        /// <param name="stateContainer">Stateをまとめる親クラス</param>
        public void Initialize(StateContainerBase stateContainer);
        /// <summary>
        /// Stateに入った時に1度だけ実行する処理
        /// </summary>
        public void Enter();
        /// <summary>
        /// 毎フレーム実行するクラス処理、Enter()の1フレーム後に呼ばれるので注意！！
        /// </summary>
        public void MyUpdate();
        /// <summary>
        /// 等間隔で実行する処理
        /// </summary>
        public void MyFixedUpdate();
        /// <summary>
        /// Stateから出るときに1度だけ実行する処理
        /// </summary>
        public void Exit();
    }
}