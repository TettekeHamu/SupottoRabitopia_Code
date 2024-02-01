using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using PullAnimals.SceneLoader;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ゲーム終了時のState
    /// </summary>
    public class EndState : IState,IDisposable
    {
        /// <summary>
        /// Stateを変更させるメソッドを持つInterface
        /// </summary>
        private ITransitionState _transitionState;
        /// <summary>
        /// Stateをまとめたクラス
        /// </summary>
        private MainSceneStateContainer _stateContainer;
        /// <summary>
        /// Playerの管理をおこなうクラス
        /// </summary>
        private readonly PlayerBehaviour _player;
        /// <summary>
        /// UniTaskキャンセル用のトークンソース
        /// </summary>
        private CancellationTokenSource _cancellationTokenSource;
        
        private async UniTaskVoid AsyncLoadEndScene(CancellationToken token)
        {
            //ちょっとだけ待つ
            await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
            //シーン遷移
            SceneLoadController.Instance.LoadNextScene("EndSceneVer1.0");
        }
        
        void IState.Initialize(StateContainerBase stateContainer)
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }

        void IState.Enter()
        {
            _player.StopPlayer();
            AsyncLoadEndScene(_cancellationTokenSource.Token).Forget();
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
        
        //コンストラクター
        public EndState(PlayerBehaviour player)
        {
            _player = player;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
        }
    }
}
