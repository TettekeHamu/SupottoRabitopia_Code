using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// ゲーム開始前の準備中のState
    /// </summary>
    public class PreparationState : IState,IDisposable
    {
        /// <summary>
        /// UniTaskキャンセル用のトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
        /// <summary>
        /// Stateを変更させるメソッドを持つInterface
        /// </summary>
        private readonly ITransitionState _transitionState;
        /// <summary>
        /// Stateをまとめたクラス
        /// </summary>
        private MainSceneStateContainer _stateContainer;
        /// <summary>
        /// ゲームの時間を管理するクラス
        /// </summary>
        private readonly GameTimeController _gameTimeController;
        /// <summary>
        /// チュートリアルを管理するクラス
        /// </summary>
        private readonly TutorialController _tutorialController;

        /// <summary>
        /// チュートリアルを表示する処理
        /// </summary>
        /// <param name="token"></param>
        private async UniTaskVoid AsyncTutorial(CancellationToken token)
        {
            //チュートリアルが無事に再生された or スキップされたを取得する
            var result = await UniTask.WhenAny(_tutorialController.AsyncSendMessage(token), AsyncSkipTutorial(token));
            //スキップされたらダイアログを閉じてからカウントダウンを開始
            if (result.result2)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                _tutorialController.StopTutorial();
                AsyncCountDown(_cancellationTokenSource.Token).Forget();
            }
            //チュートリアルが無事に再生されたらカウントダウン開始
            else if(result.result1)
            {
                _cancellationTokenSource.Cancel();
                _cancellationTokenSource = new CancellationTokenSource();
                AsyncCountDown(_cancellationTokenSource.Token).Forget();
            }
        }

        private async UniTask<bool> AsyncSkipTutorial(CancellationToken token)
        {
            await UniTask.WaitUntil(() => MyInputController.Instance.ChangeSceneKeyDown, cancellationToken: token);
            SePlayer.Instance.Play("SE_Decide");
            return true;
        }
        
        /// <summary>
        /// カウントダウンをおこなう処理
        /// </summary>
        /// <param name="token"></param>
        private async UniTaskVoid AsyncCountDown(CancellationToken token)
        {
            //カウントダウンをおこなう処理
            while (_gameTimeController.CountdownTimeProperty.Value > 0)
            {
                //数秒カウントダウンする
                _gameTimeController.ReduceCountdownTime();
                //カウントダウンの値によってSEを変える
                if (_gameTimeController.CountdownTimeProperty.Value == 0)
                {
                    //ゲームを開始する際のSE
                    SePlayer.Instance.Play("SE_GameStart");
                }
                else
                {
                    //カウントダウン用のSE
                    SePlayer.Instance.Play("SE_CountDown");   
                }
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            }

            //PlayStateに移動
            _transitionState.TransitionState(_stateContainer.PlayState);
        }
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _stateContainer = stateContainer as MainSceneStateContainer;
        }

        void IState.Enter()
        {
            //AsyncCountDown(_cancellationTokenSource.Token).Forget();
            AsyncTutorial(_cancellationTokenSource.Token).Forget();
        }

        void IState.MyUpdate()
        {
            
        }

        void IState.MyFixedUpdate()
        {
            
        }

        void IState.Exit()
        {
            
        }
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public PreparationState(ITransitionState ts, GameTimeController gtc, TutorialController tc)
        {
            _transitionState = ts;
            _gameTimeController = gtc;
            _tutorialController = tc;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}