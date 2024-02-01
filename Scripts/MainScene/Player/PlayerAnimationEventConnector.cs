using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// アニメーションイベントとプレイヤーをつなぐクラス
    /// </summary>
    public class PlayerAnimationEventConnector : MonoBehaviour
    {
        /// <summary>
        /// 引っ張るアニメーションを再生中かどうか
        /// </summary>
        private bool _isPullingAnimation;
        /// <summary>
        /// ウサギを引っこ抜いた瞬間にtrueになるフラグ
        /// </summary>
        private bool _isPullRabbitAnimation;
        /// <summary>
        /// 引っ張るアニメーションを再生中かどうか
        /// </summary>
        public bool IsPullingAnimation => _isPullingAnimation;
        /// <summary>
        /// ウサギを引っこ抜いた瞬間にtrueになるフラグ
        /// </summary>
        public bool IsPullRabbitAnimation => _isPullRabbitAnimation;

        /// <summary>
        /// 引っ張りのアニメーションが開始されたときに呼ぶ処理
        /// AnimationEvent側から呼び出す
        /// </summary>
        public void StartPullingAnimation()
        {
            _isPullingAnimation = true;
            _isPullRabbitAnimation = false;
        }
        
        /// <summary>
        /// 引っ張りのアニメーションが終わったときに呼ぶ処理
        /// AnimationEvent側から呼び出す
        /// </summary>
        public void EndPullingAnimation()
        {
            _isPullingAnimation = false;
        }

        /// <summary>
        /// ウサギを引っこ抜く際のアニメーションの際に呼ぶ処理
        /// AnimationEvent側から呼び出す
        /// </summary>
        public void PullRabbitAnimation()
        {
            _isPullRabbitAnimation = true;
        }
    }
}
