using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーの移動スピードを管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "PlayerMoveData", menuName = "ScriptableObjects/PlayerMoveData")]
    public class PlayerMoveData : ScriptableObject
    {
        /// <summary>
        /// 走る移動のスピード
        /// </summary>
        [Header("走る移動のスピード")]
        [SerializeField] private int _runSpeed;
        /// <summary>
        /// 歩く移動時のスピード
        /// </summary>
        [Header("歩く移動時のスピード")]
        [SerializeField] private int _walkSpeed;
        /// <summary>
        /// 通常移動のスピード
        /// </summary>
        public int RunSpeed => _runSpeed;
        /// <summary>
        /// 振ってないとみなすスピード
        /// </summary>
        public int WalkSpeed => _walkSpeed;
    }
}
