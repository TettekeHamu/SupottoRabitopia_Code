using System.Collections;
using DG.Tweening;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ウサギの管理をおこなうクラス
    /// </summary>
    public class RabbitBehaviour : MonoBehaviour,ISetHand,ISelected,ITakePulled
    {
        /// <summary>
        /// 動物のステータス
        /// </summary>
        [SerializeField] private RabbitStatusData _status;
        /// <summary>
        /// ウサギの見た目（実体）を管理するクラス
        /// </summary>
        [SerializeField] private RabbitViewController _viewController;
        /// <summary>
        /// ウサギの物理演算を管理するクラス
        /// </summary>
        [SerializeField] private RabbitPullController _pullController;
        /// <summary>
        /// 自身を破棄するクラス
        /// </summary>
        [SerializeField] private RabbitDestroyer _destroyer;
        /// <summary>
        /// ウサギの生成時間を管理するクラス
        /// </summary>
        [SerializeField] private RabbitRemainTimeController _remainTimeController;
        /// <summary>
        /// ウサギ生成時のパーティクルを管理するクラス
        /// </summary>
        [SerializeField] private RabbitParticleController _particleController;
        /// <summary>
        /// ウサギの元々の大きさ
        /// </summary>
        private Vector3 _rabbitSize;
        /// <summary>
        /// アニマルのStateを管理するクラス
        /// </summary>
        private RabbitStateMachine _stateMachine;
        /// <summary>
        /// 動物のデータを管理するScriptableObject
        /// </summary>
        public RabbitStatusData Status => _status;

        /// <summary>
        /// ウサギが一定時間上に行って消える処理
        /// </summary>
        /// <returns></returns>
        private IEnumerator FlyCoroutine()
        {
            yield return DOTween.Sequence()
                .Append(transform.DOLocalMoveY(10f, 1f))
                .Join(transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360))
                .WaitForCompletion();

            _destroyer.DestroyRabbit();
        }

        void ISetHand.SetHand(HandObject hand)
        {
            transform.parent = hand.transform;
            transform.localPosition = Vector3.zero;
        }

        void ISetHand.ReleaseHand()
        {
            //スコアコントローラに自分が抜かれたことを通知
            var score = FindObjectOfType<PulledRabbitNumberController>();
            score.AddCount(_stateMachine, _status);
            //大きさと位置を調整
            transform.localScale = _rabbitSize;
            transform.parent = null;
            StartCoroutine(FlyCoroutine());
        }

        RabbitBehaviour ISelected.SelectAnimal()
        {
            return this;
        }

        void ITakePulled.StartPulling()
        {
            _pullController.StartPulled();
        }

        void ITakePulled.TakePulling(int current, int max)
        {
            //呼び出されるとびに縦方向に引き延ばす
            //Transformをキャッシュしておく
            var tf = transform;
            var localScale = tf.localScale;
            var scaleY = localScale.y;
            //引っこ抜くのに必要な連打回数によって倍率を変える
            scaleY *= 1 + 1f / max;
            localScale = new Vector3(localScale.x, scaleY, localScale.z);
            tf.localScale = localScale;
        }
        
        public void MyUpdate()
        {
            _stateMachine?.MyUpdate();
        }

        /// <summary>
        /// 初期化処理
        /// </summary>
        public void Initialize(int[] time)
        {
            _rabbitSize = transform.localScale;
            _remainTimeController.SetRemainTime(time);
            _stateMachine = new RabbitStateMachine(_viewController, _pullController, _destroyer, _remainTimeController, _particleController);
        }

        /// <summary>
        /// スコアを返す処理
        /// </summary>
        /// <returns>スコア</returns>
        public int GetScore()
        {
            //Stateに応じてスコアを変更
            var state = _stateMachine.GetCurrentState();
            if (state is SproutState) return 10;
            if (state is GrowingState) return 20;
            if (state is BloomState) return _status.Score;
            return 0;
        }

        /// <summary>
        /// 現在のStateを返す処理
        /// </summary>
        /// <returns>現在のState</returns>
        public IState GetRabbitState()
        {
            return _stateMachine.GetCurrentState();
        }
    }
}
