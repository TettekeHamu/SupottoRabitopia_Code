using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーのアニメーションを管理するクラス
    /// </summary>
    public class PlayerAnimatorController : MonoBehaviour
    {
        /// <summary>
        /// アニメーターコンポーネント
        /// </summary>
        [SerializeField] private Animator _animator;
        /// <summary>
        /// アニメーションの長さと再生倍率をまとめたScriptableObject
        /// </summary>
        [SerializeField] private AnimationData _endPullingAnimationData;
        /// <summary>
        /// アニメーターコンポーネント
        /// </summary>
        public Animator MyAnimator => _animator;
        /// <summary>
        /// ニメーションの長さと再生倍率をまとめたScriptableObject
        /// </summary>
        public AnimationData EndPullingAnimationData => _endPullingAnimationData;
        /// <summary>
        /// 移動時のfloat型パラメータ
        /// </summary>
        public int MoveSpeedHash => Animator.StringToHash("MoveSpeed");
        /// <summary>
        /// ジャンプ・落下時のfloat型パラメータ
        /// </summary>
        public int FallSpeedHash => Animator.StringToHash("FallSpeed");
        /// <summary>
        /// 着地しているかどうかのbool型パラメータ
        /// </summary>
        public int IsGroundedHash => Animator.StringToHash("IsGrounded");
        /// <summary>
        /// ウサギを引っこ抜く始めた際のTrigger型パラメータ
        /// </summary>
        public int StartPullTriggerHash => Animator.StringToHash("StartPullTrigger");
        /// <summary>
        /// ウサギを引っこ抜き終えた際のTrigger型パラメータ
        /// </summary>
        public int EndPullTriggerHash => Animator.StringToHash("EndPullTrigger");

        /// <summary>
        /// プレイヤーのアニメーションをIdleに戻す処理
        /// </summary>
        public void StopPlayerAnimation()
        {
            _animator.SetFloat(MoveSpeedHash, 0);
        }
    }
}