using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// チュートリアルを管理するクラス
    /// </summary>
    public class TutorialController : MonoBehaviour
    {
        /// <summary>
        /// チュートリアル時に表示する文字列のデータのScriptableObject
        /// </summary>
        [SerializeField] private TutorialMessageData _tutorialMessageData;
        /// <summary>
        /// 文章を発行するReactiveProperty
        /// </summary>
        private readonly ReactiveProperty<string> _messageProperty = new ReactiveProperty<string>();
        /// <summary>
        /// 文章を発行するReactivePropertyのObservable
        /// </summary>
        public IReadOnlyReactiveProperty<string> MessageProperty => _messageProperty;

        /// <summary>
        /// メッセージを発行する処理
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> AsyncSendMessage(CancellationToken token)
        {
            for (var i = 0; i < _tutorialMessageData.MessageArray.Length; i++)
            {
                _messageProperty.Value = _tutorialMessageData.MessageArray[i];
                await UniTask.DelayFrame(1, cancellationToken: token);
                await UniTask.WaitUntil(() => MyInputController.Instance.HandActionKeyDown, cancellationToken: token);
                SePlayer.Instance.Play("SE_Decide");
            }

            _messageProperty.Value = null;
            return true;
        }

        /// <summary>
        /// チュートリアルを止める処理
        /// </summary>
        public void StopTutorial()
        {
            //空の文字列を発効することでダイアログを閉じる
            _messageProperty.Value = null;
        }
    }
}
