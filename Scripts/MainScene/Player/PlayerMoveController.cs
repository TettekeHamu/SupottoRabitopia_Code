using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// プレイヤーを移動させる処理
    /// </summary>
    public class PlayerMoveController : MonoBehaviour
    {
        /// <summary>
        /// プレイヤーのコンストラクターをまとめたクラス
        /// </summary>
        [SerializeField] private CharacterController _characterController;
        /// <summary>
        /// 歩いてる時の移動スピード
        /// </summary>
        private float _walkSpeed;
        /// <summary>
        /// 走ってる時の移動スピード
        /// </summary>
        private float _runSpeed;
        /// <summary>
        /// どのくらい滑らかに回転させるか、値が大きいほどゆっくり回転する
        /// </summary>
        private float _rotationSmoothTime;
        /// <summary>
        /// ジャンプするのに使用する値
        /// </summary>
        private float _startJumpPower;
        /// <summary>
        /// 現在の回転の速度
        /// </summary>
        private float _rotationCurrentVelocity;
        /// <summary>
        /// 現在の移動スピード、ジャンプ時のスピードの設定などで使用する
        /// </summary>
        private float _currentSpeed;
        
        /// <summary>
        /// プレイヤーのコンストラクターをまとめたクラス
        /// </summary>
        public bool OnGround => _characterController.isGrounded;
        /// <summary>
        /// ジャンプするのに使用する値
        /// </summary>
        public float StartJumpPower => _startJumpPower;
        
        private void Awake()
        {
            //初期値を設定
            _walkSpeed = 3f;
            _runSpeed = 6f;
            _rotationSmoothTime = 0.15f;
            _startJumpPower = 8f;
            _rotationCurrentVelocity = 0;
            _currentSpeed = 0;
        }
        
        /// <summary>
        /// 移動してないときに下方向にのみ速度を持たせてあげる処理
        /// </summary>
        public void IdlePlayer(PlayerAnimatorController animatorController)
        {
            _currentSpeed = 0;
            _characterController.Move(new Vector3(0, -0.5f, 0) * Time.deltaTime);
            //アニメーションに反映
            animatorController.MyAnimator.SetFloat(animatorController.MoveSpeedHash, 0);
        }

        /// <summary>
        /// プレイヤーを移動させる処理
        /// </summary>
        public void MovePlayer(PlayerAnimatorController animatorController)
        {
            //入力をXZ平面に変換 & 長さを1にする
            var directionVec = new Vector3(MyInputController.Instance.MoveInputVector2.x, 0, MyInputController.Instance.MoveInputVector2.y).normalized;
            
            //実際に移動に使うベクトルを生成
            var moveVec = Vector3.zero;
            
            //一定の入力量がある場合のみ実行
            if (directionVec.magnitude >= 0.01f)
            {
                //移動方向に向かせる
                //カメラと入力方向の向きを一致させるためにLookAt()ではなくAtan2を使用
                //プレイヤーを回転させたい度数を取得する
                var targetAngle = Mathf.Atan2(directionVec.x, directionVec.z) * Mathf.Rad2Deg + Camera.main!.transform.eulerAngles.y;

                //現在の回転量から目標の回転量に滑らかに回転させる量を算出
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
                    targetAngle,
                    ref _rotationCurrentVelocity, 
                    _rotationSmoothTime);

                //回転させる
                transform.rotation = Quaternion.Euler(0, angle, 0);
                
                //カメラの向きにあわせた入力のベクトルを生成
                moveVec = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            }
            
            //スピードを取得
            var speed = MyInputController.Instance.WalkKey ? _walkSpeed : _runSpeed;

            _currentSpeed = speed;

            //移動をおこなう(下方向に別途重力をかけてあげる)
            _characterController.Move(moveVec * (speed * Time.deltaTime) + new Vector3(0, -0.5f, 0) * Time.deltaTime);
            
            //アニメーションに反映
            animatorController.MyAnimator.SetFloat(animatorController.MoveSpeedHash, speed);
        }

        /// <summary>
        /// ジャンプしながらプレイヤーを動かす処理
        /// ボタンの押す長さによってジャンプの距離を変えてあげる
        /// </summary>
        public void JumpMovePlayer(PlayerAnimatorController animatorController, float jumpSpeed)
        {
            //入力をXZ平面に変換する
            var directionVec = new Vector3(MyInputController.Instance.MoveInputVector2.x, 0, MyInputController.Instance.MoveInputVector2.y).normalized;
            
            //実際に移動に使うベクトルを生成
            var moveVec = Vector3.zero;
            
            //一定の入力量がある場合のみ実行
            if (directionVec.magnitude >= 0.01f)
            {
                //移動方向に向かせる
                //カメラと入力方向の向きを一致させるためにLookAt()ではなくAtan2を使用
                //プレイヤーを回転させたい度数を取得する
                var targetAngle = Mathf.Atan2(directionVec.x, directionVec.z) * Mathf.Rad2Deg + Camera.main!.transform.eulerAngles.y;

                //現在の回転量から目標の回転量に滑らかに回転させる量を算出
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
                    targetAngle,
                    ref _rotationCurrentVelocity, 
                    _rotationSmoothTime);
                
                //回転させる
                transform.rotation = Quaternion.Euler(0, angle, 0);
                
                //カメラの向きにあわせたベクトルを生成
                moveVec = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;
            }
            
            //入力に合わせてスピードを変更
            var speed = 0f;
            if (_currentSpeed < 4) speed = _walkSpeed / 2;
            else speed = _runSpeed / 2;
            
            //移動をおこなう
            _characterController.Move(moveVec * (speed * Time.deltaTime) + new Vector3(0, jumpSpeed, 0) * Time.deltaTime);
            
            //アニメーションに反映
            animatorController.MyAnimator.SetFloat(animatorController.MoveSpeedHash, speed);
            //animatorController.MyAnimator.SetFloat(animatorController.FallSpeedHash, jumpSpeed);
        }
    }
}