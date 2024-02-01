namespace PullAnimals.StatePattern
{
    /// <summary>
    /// Stateを変更させるメソッドを持つInterface
    /// </summary>
    public interface ITransitionState
    {
        /// <summary>
        /// Stateを変更させる処理
        /// </summary>
        /// <param name="newState">変更先のState</param>
        public void TransitionState(IState newState);
    }
}