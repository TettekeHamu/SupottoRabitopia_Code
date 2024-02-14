using System;
using PullAnimals.StatePattern;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーのStateを管理するクラス
    /// </summary>
    public class PlayerStateMachine : StateMachineBase,IDisposable
    {
        /// <summary>
        /// stateをまとめたクラス
        /// </summary>
        private readonly PlayerStateContainer _playerStateContainer;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public PlayerStateMachine(PlayerComponentsBundle bundle)
        {
            //各Stateを生成
            _playerStateContainer = new PlayerStateContainer(this, bundle);
            //初期化
            _playerStateContainer.Initialize();
            //初期Stateを設定
            Initialize(_playerStateContainer.IdleState);
        }

        /// <summary>
        /// ゲーム終了時にPlayerを止める処理
        /// </summary>
        public void StopPlayer()
        {
            ITransitionState transitionState = this;
            transitionState.TransitionState(_playerStateContainer.IdleState);
        }

        /// <summary>
        /// 破棄されたときの処理
        /// </summary>
        public void Dispose()
        {
            //ここでUniTaskをキャンセル
           _playerStateContainer?.Dispose();
        }
    }
}