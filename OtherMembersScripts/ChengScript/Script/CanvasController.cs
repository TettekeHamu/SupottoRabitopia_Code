using UnityEngine;
using DG.Tweening;

namespace PullAnimals
{
    /// <summary>
    /// Canvasにアタッチするクラス
    /// </summary>
    public class CanvasController: MonoBehaviour
    {
        /// <summary>
        /// SkyパーツのPrefab
        /// </summary>
        [SerializeField] private SkyImage _skyImage;
        /// <summary>
        /// CloudパーツのPrefab
        /// </summary>
        [SerializeField] private CloudImage _cloudImage;
        /// <summary>
        /// FarmパーツのPrefab
        /// </summary>
        [SerializeField] private FarmImage _farmImage;
        /// <summary>
        /// CharacterパーツのPrefab
        /// </summary>
        [SerializeField] private CharacterImage _characterImage;
        /// <summary>
        /// Characterの初期配置X座標
        /// </summary>
        [SerializeField] private float _characterStartX;
        /// <summary>
        /// Characterの初期配置Y座標
        /// </summary>
        [SerializeField] private float _characterStartY;
        /// <summary>
        /// LogoパーツのPrefab
        /// </summary>
        [SerializeField] private LogoImage _logoImage;
        /// <summary>
        /// Logoの初期配置Y座標
        /// </summary>
        [SerializeField] private float _logoStartY = 60.0f;
        /// <summary>
        /// SmokeパーツのPrefab
        /// </summary>
        [SerializeField] private SmokeImage _smokeImage;

        /// <summary>
        /// CharacterとLogoが画面に入場するタイミング(値は背景を0度->90度まで上げる際の角度)
        /// </summary>
        [SerializeField] private float _startInTiming;

        /// <summary>
        /// タイトルで入力を示す用のテキスト
        /// </summary>
        [SerializeField] private PressToStart _pressToStart;

        /// <summary>
        /// テキスト点滅の頻度
        /// </summary>
        [SerializeField] private float _flashRate;

        /// <summary>
        /// Character動きの頻度と速さ用変数
        /// </summary>
        [SerializeField] private float _charaActRate;

        /// <summary>
        /// 各パーツ初期設置座標
        /// </summary>
        private Vector3 _setup = new Vector3(0.0f,0.0f, 0.0f);

        /// <summary>
        /// Characterパーツ入場開始用フラグ
        /// </summary>
        private bool _moveIn = true;

        /// <summary>
        /// ループに設置されるToon用フラグ
        /// </summary>
        private bool _loopStart = true;

        public bool SetComplete()
        {
            if(_characterImage.transform.position.y <= 0.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 各パーツを初期化
        /// </summary>
        public void SetTitleImage()
        {
            /// Skyパーツの初期化
            {
                var createPos = new Vector3(0.0f, -540.0f, 0.0f);
                var worldPos = transform.TransformPoint(createPos);
                Quaternion createRot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                _skyImage.transform.position = worldPos;
                _skyImage.transform.rotation = createRot;
                _skyImage.MyImage.color = Color.black;
                
            }

            /// Cloudパーツの初期化
            {
                var createPos = new Vector3(0.0f, -540.0f, 0.0f);
                var worldPos = transform.TransformPoint(createPos);
                Quaternion createRot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                _cloudImage.transform.position = worldPos;
                _cloudImage.transform.rotation = createRot;
                _cloudImage.MyImage.color = Color.black;
            }

            /// Farmパーツの初期化
            {
                var createPos = new Vector3(0.0f, -540.0f, 0.0f);
                var worldPos = transform.TransformPoint(createPos);
                Quaternion createRot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                _farmImage.transform.position = worldPos;
                _farmImage.transform.rotation = createRot;
                _farmImage.MyImage.color = Color.black;
            }

            /// Characterパーツの初期化
            {
                var createPos = new Vector3(_characterStartX, _characterStartY, 0.0f);
                var worldPos = transform.TransformPoint(createPos);
                _characterImage.transform.position = worldPos;
            }

            _characterImage.SetStarImage();

            /// Logoパーツの初期化
            {
                var createPos = new Vector3(0.0f, _logoStartY, 100.0f);
                var worldPos = transform.TransformPoint(createPos);
                _logoImage.transform.position = createPos;
            }

            /// Smokeパーツの初期化
            {
                var createPos = new Vector3(0.0f, -540.0f, 0.0f);
                var worldPos = transform.TransformPoint(createPos);
                Quaternion createRot = Quaternion.Euler(90.0f, 0.0f, 0.0f);
                _smokeImage.transform.position = worldPos;
                _smokeImage.transform.rotation = createRot;
                _smokeImage.MyImage.color = Color.black;
            }

            /// PressToStartの初期化
            _pressToStart.MyImage.color = new Color(255.0f, 255.0f, 255.0f, 0f);
        }

        /// <summary>
        /// タイトルが始まる時、各パーツの挙動
        /// </summary>
        public void SetTitleAction()
        {
            {
                _moveIn = true;

                _skyImage.transform.DORotate(_setup, 0.5f);
                _skyImage.MyImage.DOColor(Color.white, 1f)
                    .SetLink(gameObject);

                _cloudImage.transform.DORotate(_setup, 0.5f);
                _cloudImage.MyImage.DOColor(Color.white, 1f)
                    .SetLink(gameObject);

                _farmImage.transform.DORotate(_setup, 0.5f);
                _farmImage.MyImage.DOColor(Color.white, 1f)
                    .SetLink(gameObject);
                
                _smokeImage.transform.DORotate(_setup, 0.5f);
                _smokeImage.MyImage.DOColor(Color.white, 1f)
                    .SetLink(gameObject);
            }
        }

        /// <summary>
        /// タイトル画像がセットした後、各パーツの挙動
        /// </summary>
        public void LoopTitleAction()
        {
            float BGRotX = _skyImage.transform.rotation.x * Mathf.Rad2Deg;
            if (_characterImage.transform.position.y <= 1.0f)
            {
                _characterImage.ShowStar();
            }

            if (BGRotX <= _startInTiming && _moveIn)
            {
                _moveIn = false;
                _characterImage.transform.DOLocalMoveX(0.0f, 1.5f).SetLink(gameObject);
                _characterImage.transform.DOLocalMoveY(0.0f, 1.5f).SetEase(Ease.OutBounce, 5)
                    .SetLink(gameObject);
                _logoImage.transform.DOLocalMoveY(0.0f, 1.5f).SetEase(Ease.OutBounce, 5)
                    .SetLink(gameObject);
            }

            if(SetComplete())
            {
                if(_loopStart)
                {
                    //DOTweenでLoopを設置するため、1回しか呼ばれないようにBoolを掛ける
                    _loopStart = false;

                    /// テキストの表示
                    _pressToStart.gameObject.SetActive(true);
                    _pressToStart.MyImage.DOFade(1f, _flashRate).SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);;

                    /// Cloudの動作
                    _cloudImage.transform.DOLocalPath(
                            new[]
                            {
                                new Vector3(40.0f, -500.0f, 0f),
                                new Vector3(80.0f, -540.0f, 0f),
                                new Vector3(40.0f, -580.0f, 0f),
                                new Vector3(0f, -540.0f, 0f),
                                new Vector3(-40.0f, -500.0f, 0f),
                                new Vector3(-80.0f, -540.0f, 0f),
                                new Vector3(-40.0f, -580.0f, 0f),

                            }, 50f, PathType.CatmullRom
                        )
                        .SetEase(Ease.Linear)
                        .SetOptions(true)
                        .SetLoops(-1, LoopType.Restart)
                        .SetLink(gameObject);

                    /// Characterの動作
                    _characterImage.transform.DOLocalMoveY(100.0f, _charaActRate).SetLoops(-1, LoopType.Yoyo)
                        .SetDelay(0.1f)
                        .SetLink(gameObject);
                }

                
            }

        }

        public void TestAction()
        {
            //_characterImage.transform.DORestart();
            //_characterImage.transform.DOLocalMoveY(100.0f, _charaActRate).SetLoops(-1, LoopType.Yoyo);
        }
    }

}
