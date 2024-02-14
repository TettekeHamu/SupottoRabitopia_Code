using UniRx;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// ViewとModelをつなぐPresenter
    /// </summary>
    public class UIPresenter : MonoBehaviour
    {
        ///引っこ抜き関係
        /// <summary>
        /// プレイヤーの手の役割（引っこ抜く・運ぶなど）を管理するクラス
        /// </summary>
        [SerializeField] private PlayerHandController _handController;
        /// <summary>
        /// ウサギを引っこ抜く際にUIを表示するクラス
        /// </summary>
        [SerializeField] private PullUIView _pullUIView;
        /// <summary>
        /// チュートリアルを管理するクラス
        /// </summary>
        [SerializeField] private TutorialController _tutorialController;
        /// <summary>
        /// ダイアログを管理するクラス
        /// </summary>
        [SerializeField] private DialogUIController _dialogUIController;

        private void Awake()
        {
            //引っ張りに関する処理
            _handController.OnPullingRabbitObservable
                .Subscribe(b => _pullUIView.ChangePullText(b))
                .AddTo(this);
            //チュートリアルに関する処理
            _tutorialController.OnSendDialogDataObservable
                .Subscribe(data => _dialogUIController.ShowDialog(data))
                .AddTo(this);
        }
    }
}
