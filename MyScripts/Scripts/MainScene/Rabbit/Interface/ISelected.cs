namespace PullAnimals
{
    /// <summary>
    /// 選択する処理を持つインターフェース
    /// </summary>
    public interface ISelected
    {
        /// <summary>
        /// 動物を選択する処理
        /// </summary>
        /// <returns>選択されてる動物（自身）を返す</returns>
        public RabbitBehaviour SelectAnimal();
    }
}