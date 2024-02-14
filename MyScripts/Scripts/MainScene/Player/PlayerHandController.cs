using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーの手の役割（引っこ抜く・運ぶなど）を管理するクラス
    /// </summary>
    public class PlayerHandController : MonoBehaviour
    {
        /// <summary>
        /// Joyconを振った/振ってないとみなすスピードを管理するScriptableObject
        /// </summary>
        [SerializeField] private JoyconSwingData _joyconSwingData;
        /// <summary>
        /// 動物を引っこ抜ける範囲
        /// </summary>
        [SerializeField] private PullRangeObject _pullRangeObject;
        /// <summary>
        /// 手に該当する箇所のオブジェクト
        /// </summary>
        [SerializeField] private HandObject _handObject;
        /// <summary>
        /// ウサギを引っこ抜くときに発効するSubject
        /// </summary>
        private readonly Subject<bool> _onPullingRabbitSubject = new Subject<bool>();
        /// <summary>
        /// 運んでいる動物
        /// </summary>
        private RabbitBehaviour _carryingRabbit;
        /// <summary>
        /// Joyconを振った/振ってないとみなすスピードを管理するScriptableObject
        /// </summary>
        public JoyconSwingData SwingData => _joyconSwingData;
        /// <summary>
        /// 運んでいる動物
        /// </summary>
        public RabbitBehaviour CarryingRabbit => _carryingRabbit;
        /// <summary>
        /// ウサギを引っこ抜くときに発効するSubjectのObservable
        /// </summary>
        public IObservable<bool> OnPullingRabbitObservable => _onPullingRabbitSubject;

        /// <summary>
        /// 引っこ抜き始めた際にウサギの方に向けるコルーチン
        /// </summary>
        /// <returns></returns>
        private IEnumerator LookRabbitCoroutine(Vector3 rabbitPos)
        {
            var vec = rabbitPos - transform.position;
            vec.y = 0;
            var lookRot = Quaternion.LookRotation(vec, Vector3.up);
            var timer = 0f;
            var targetTime = 0.2f;
            while (true)
            {
                timer += Time.deltaTime;
                var r = timer / targetTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, r);
                if(r > 1f) break;
                yield return null;
            }
        }
        
        /// <summary>
        /// 動物を引っこ抜く処理
        /// </summary>
        /// <returns>動物を持てればtrueを返す</returns>
        public bool PullAnimal()
        {
            //そもそも動物を持っていたらfalseを返す
            if (_carryingRabbit) return false;

            //範囲内に動物がいるかどうか確認していればtrueを返す
            _carryingRabbit = _pullRangeObject.CurrentSelectAnimal;

            //いるかどうかを返す
            if (_pullRangeObject.CurrentSelectAnimal != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ウサギをを引っこ抜き始めたときの処理
        /// </summary>
        public void StartPulling(bool isFever)
        {
            StartCoroutine(LookRabbitCoroutine(_carryingRabbit.transform.position));
            ITakePulled pullingRabbit = _carryingRabbit;
            pullingRabbit.StartPulling();
            
            //Fever中ならUIを表示しない
            if(isFever) return;
            
            if (_carryingRabbit.Status.ShakeCount > 0)
            {
                //連打が必要なときのみ発効する
                var state = _carryingRabbit.GetRabbitState();
                if (state is BloomState)
                {
                    _onPullingRabbitSubject.OnNext(true);      
                }
            }
        }

        /// <summary>
        /// 手の位置に動物をセットする処理
        /// </summary>
        public void SetAnimalHandPos()
        {
            ISetHand setHand = _carryingRabbit;
            setHand.SetHand(_handObject);
        }

        /// <summary>
        /// ウサギを引っ張ってるときの処理
        /// </summary>
        public void PullRabbit(int current, int max)
        {
            ITakePulled pullingRabbit = _carryingRabbit;
            pullingRabbit.TakePulling(current, max);
        }

        /// <summary>
        /// ウサギを投げ飛ばす処理
        /// </summary>
        public void ThrowRabbit()
        {
            _onPullingRabbitSubject.OnNext(false);
            //親子関係を無くす & 上方向に投げる
            ISetHand setHand = _carryingRabbit;
            //ウサギ側に抜かれたことを伝える
            setHand.ReleaseHand();
            //運んでいるウサギをなくす
            _carryingRabbit = null;
        }
    }
}
