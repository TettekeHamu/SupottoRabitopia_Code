namespace PullAnimals
{
    /// <summary>
    /// 手の位置に自信を持っていく処理を持つインターフェース
    /// </summary>
    public interface ISetHand
    {
        /// <summary>
        /// 手の位置に自身も持っていく処理
        /// </summary>
        /// <param name="hand">手の位置</param>
        public void SetHand(HandObject hand);
        /// <summary>
        /// 親子関係を解消させる処理
        /// </summary>
        public void ReleaseHand();
    }
}