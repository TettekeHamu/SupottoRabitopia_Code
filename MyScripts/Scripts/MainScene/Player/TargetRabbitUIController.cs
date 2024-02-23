using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// 画面外にいるウサギを知らせるUIを管理するクラス
    /// </summary>
    public class TargetRabbitUIController : MonoBehaviour
    {
        /// <summary>
        /// 矢印の画像があるCanvas
        /// </summary>
        [SerializeField] private Transform _canvasTransform;
        /// <summary>
        /// 矢印の画像
        /// </summary>
        [SerializeField] private Image _arrowImage;
        /// <summary>
        /// 矢印の中の画像
        /// </summary>
        [SerializeField] private Image _arrowInImage;
        /// <summary>
        /// 画面外にいてなおかつ一番近いウサギ
        /// </summary>
        private RabbitPullController _targetRabbit;
        /// <summary>
        /// カメラ
        /// </summary>
        private Camera _mainCamera;

        private void Awake() 
        {
            _mainCamera = Camera.main;
            _arrowImage.enabled = false;
            _arrowInImage.enabled = false;
        }

        /// <summary>
        /// 一番近くにいるウサギの方向に矢印を表示する処理
        /// </summary>
        public void ShowTargetRabbit()
        {
            //シーン内にいるウサギをすべて取得
            var rabbits = FindObjectsOfType<RabbitPullController>();
            //引っこ抜かれていないものだけを選ぶ
            var filteredRabbits = System.Array.FindAll(rabbits, r => !r.IsPulled);
            
            //いなかったらreturn
            if (filteredRabbits.Length == 0)
            {
                _arrowImage.enabled = false;
                _arrowInImage.enabled = false;
                return;
            }
            
            //一番近くにいるウサギをを決定
            var distance = filteredRabbits[0].transform.position - transform.position;
            _targetRabbit = filteredRabbits[0];
            for (var i = 1; i < filteredRabbits.Length; i++)
            {
                var dis = filteredRabbits[i].transform.position - transform.position;
                if (dis.magnitude < distance.magnitude)
                {
                    _targetRabbit = filteredRabbits[i];
                    distance = dis;
                }
            }
            
            //矢印を移動させる
            var canvasScale = _canvasTransform.root.localScale.z;
            var center = 0.5f * new Vector3(Screen.width, Screen.height);

            var pos = _mainCamera.WorldToScreenPoint(_targetRabbit.transform.position) - center;
            if (pos.z < 0f) 
            {
                pos.x = -pos.x;
                pos.y = -pos.y;

                if (Mathf.Approximately(pos.y, 0f)) 
                {
                    pos.y = -center.y;
                }
            }

            var halfSize = 0.5f * canvasScale * _arrowImage.rectTransform.sizeDelta;
            var d = Mathf.Max
            (
                Mathf.Abs(pos.x / (center.x - halfSize.x)),
                Mathf.Abs(pos.y / (center.y - halfSize.y))
            );

            var isOffScreen = (pos.z < 0f || d > 1f);
            _arrowImage.enabled = isOffScreen;
            _arrowInImage.enabled = isOffScreen;
            
            //画面内にウサギがいるならreturn
            if(!isOffScreen) return;
            
            //画像の位置を変更
            pos.x /= d;
            pos.y /= d;
            _arrowImage.rectTransform.anchoredPosition = pos / canvasScale;
            _arrowInImage.rectTransform.anchoredPosition = pos / canvasScale;
            
            _arrowImage.rectTransform.eulerAngles = new Vector3
            (
                0f, 
                0f,
                Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg
            );
        }

        /// <summary>
        /// 矢印の画像を隠す処理
        /// </summary>
        public void HideArrowImage()
        {
            _arrowImage.enabled = false;
        }
    }
}
