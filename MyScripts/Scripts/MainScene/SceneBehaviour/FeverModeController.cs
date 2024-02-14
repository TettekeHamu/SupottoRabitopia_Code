using System;
using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// フィーバ中に画面演出をおこなうクラス
    /// </summary>
    public class FeverModeController : MonoBehaviour
    {
        /// <summary>
        /// フィーバー開始時に発行するSubject
        /// </summary>
        private static readonly Subject<Unit> OnStartFeverSubject = new Subject<Unit>();
        /// <summary>
        /// フィーバー終了時に発行するSubject
        /// </summary>
        private static readonly Subject<Unit> OnStopFeverSubject = new Subject<Unit>();
        /// <summary>
        /// フィーバー開始時に発行するSubjectのObservable
        /// </summary>
        public static IObservable<Unit> OnStartFeverObservable => OnStartFeverSubject;
        /// <summary>
        /// フィーバー終了時に発行するSubjectのObservable
        /// </summary>
        public static IObservable<Unit> OnStopFeverObservable => OnStopFeverSubject;
        
        /// <summary>
        /// フィーバー演出を開始する処理 
        /// </summary>
        /// <param name="player">プレイヤー以外が実行できないようにする</param>
        public void StartFever(PlayerFeverController player)
        {
            OnStartFeverSubject.OnNext(Unit.Default);
        }

        /// <summary>
        /// フィーバー演出を終了する処理 
        /// </summary>
        /// <param name="player">プレイヤー以外が実行できないようにする</param>
        public void StopFever(PlayerFeverController player)
        {
            OnStopFeverSubject.OnNext(Unit.Default);
        }
    }
}
