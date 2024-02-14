namespace PullAnimals.StatePattern
{
    /// <summary>
    /// Stateをまとめるクラスの親クラス
    /// </summary>
    public abstract class StateContainerBase
    {
        /// <summary>
        /// 各Stateを初期化させす処理
        /// </summary>
        public abstract void Initialize();
    }
}
