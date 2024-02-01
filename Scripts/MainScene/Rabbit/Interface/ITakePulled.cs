namespace PullAnimals
{
    /// <summary>
    /// 引っ張られる処理をまとめたインターフェース
    /// </summary>
    public interface ITakePulled
    {   
        /// <summary>
        /// ウサギに引っこ抜き始めたことを通知する処理
        /// </summary>
        public void StartPulling();

        /// <summary>
        /// 引っ張られているときの処理
        /// </summary>
        /// <param name="current">現在の引っ張り回数</param>
        /// <param name="max">最大値</param>
        public void TakePulling(int current, int max);
    }
}
