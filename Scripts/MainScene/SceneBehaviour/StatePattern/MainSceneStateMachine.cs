using System;
using PullAnimals.StatePattern;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// メインシーンのStateを管理するクラス
    /// </summary>
    public class MainSceneStateMachine : StateMachineBase,IDisposable
    {
        /// <summary>
        /// Stateをまとめたクラス
        /// </summary>
        private readonly MainSceneStateContainer _stateContainer;
        
        /// <summary>
        /// コンストラクター
        /// </summary>
        public MainSceneStateMachine(PlayerBehaviour player, GameTimeController gtc, FieldController fc, TutorialController tc)
        {
            //インスタンスを生成
            _stateContainer = new MainSceneStateContainer(this, player, gtc, fc, tc);
            //stateContainerの初期化
            _stateContainer.Initialize();
            //StateMachineを初期化
            Initialize(_stateContainer.PreparationState);
        }

        public void Dispose()
        {
            _stateContainer?.Dispose();
        }

        /// <summary>
        /// EndStateに遷移させる処理
        /// </summary>
        public void TransitionEndState()
        {
            ITransitionState transitionState = this;
            transitionState.TransitionState(_stateContainer.EndState);
        }
    }
}
