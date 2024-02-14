using System;
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
        /// チュートリアルのダイアログデータを発行するSubject
        /// </summary>
        private readonly Subject<DialogData> _onSendDialogDataSubject = new Subject<DialogData>();
        /// <summary>
        /// チュートリアルのダイアログデータを発行するSubjectのObservable
        /// </summary>
        public IObservable<DialogData> OnSendDialogDataObservable => _onSendDialogDataSubject;

        /// <summary>
        /// メッセージを発行する処理
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> AsyncSendMessage(CancellationToken token)
        {
            for (var i = 0; i < _tutorialMessageData.DialogDataList.Count; i++)
            {
                _onSendDialogDataSubject.OnNext(_tutorialMessageData.DialogDataList[i]);
                await UniTask.DelayFrame(1, cancellationToken: token);
                await UniTask.WaitUntil(() => MyInputController.Instance.GetCarryStoryKeyDown(), cancellationToken: token);
                SePlayer.Instance.Play("SE_Decide");
            }

            _onSendDialogDataSubject.OnNext(null);
            return true;
        }

        /// <summary>
        /// チュートリアルを止める処理
        /// </summary>
        public void StopTutorial()
        {
            //空の文字列を発効することでダイアログを閉じる
            _onSendDialogDataSubject.OnNext(null);
        }
    }
}
