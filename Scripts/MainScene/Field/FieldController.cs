using System.Collections.Generic;
using UnityEngine;

namespace PullAnimals
{
    /// <summary>
    /// フィールドを管理するクラス
    /// </summary>
    public class FieldController : MonoBehaviour
    {
        /// <summary>
        /// ゲーム時間を管理するクラス
        /// </summary>
        [SerializeField] private GameTimeController _gameTimeController;
        /// <summary>
        /// フィールドのグリッドをまとめた配列
        /// </summary>
        [Header("フィールド上のタイル")]
        [SerializeField] private FieldTileController[] _fieldTileObjects;
        /// <summary>
        /// ウサギの生成を管理するデータ
        /// </summary>
        [Header("ウサギの生成に関するデータ")]
        [SerializeField] private List<RabbitCreateData> _rabbitCreateDataList = new List<RabbitCreateData>();
        /// <summary>
        /// フィールドのグリッドをまとめたリスト
        /// </summary>
        private readonly List<FieldTileController> _fieldGridList = new List<FieldTileController>();
        /// <summary>
        /// フィールドの大きさ
        /// </summary>
        private Vector2Int _fieldSize;
        /// <summary>
        /// フィールドのフェーズ
        /// </summary>
        //private FieldPhaseType _fieldPhase;
        private int _currentPhase;

        private void Awake()
        {
            //フェーズをリセット
            _currentPhase = 0;
            //ステージのサイズを設定
            _fieldSize = new Vector2Int(6, 4);
        }

        /// <summary>
        /// ウサギを生成する処理
        /// </summary>
        /// <param name="rabbitType">生成するウサギの種類</param>
        /// <param name="rabbitCreateData">生成位置・時間などをまとめたデータ</param>
        /// <param name="random">生成する位置を決める乱数</param>
        private void CreateRabbits(RabbitType rabbitType, RabbitCreateData rabbitCreateData, int random)
        {
            //生成する位置を決定
            var vec2 = new Vector2Int[] { };
            switch (rabbitType)
            {
                case RabbitType.Normal:
                    vec2 = rabbitCreateData.RabbitCreatePosDataArray[random].NormalCreatePos;
                    break;
                case RabbitType.Silver:
                    vec2 = rabbitCreateData.RabbitCreatePosDataArray[random].SilverCreatePos;
                    break;
                case RabbitType.Gold:
                    vec2 = rabbitCreateData.RabbitCreatePosDataArray[random].GoldCreatePos;
                    break;
                default:
                    Debug.LogWarning("そんなウサギは存在しません！！");
                    break;
            }
            
            //生成する場所を数値に直す
            var createPos = new int[vec2.Length];
            for (var i = 0; i < createPos.Length; i++)
            {
                createPos[i] = vec2[i].x + vec2[i].y * _fieldSize.x;
            }
            //生成をおこなう
            foreach (var pos in createPos)
            {
                _fieldGridList[pos].CreateRabbit(rabbitCreateData.ShowRabbitTime, rabbitType);
            }
        }
        
        /// <summary>
        /// フェーズを確認する処理
        /// </summary>
        /// <param name="currentPhase">現在のフェーズ</param>
        private int UpdateCheckPhase(int currentPhase)
        {
            //フェーズの最大値よりも大きければリターン
            if(currentPhase >= _rabbitCreateDataList.Count) return currentPhase;
            //フェーズをintに変更
            //フェーズを変更するかのチェック
            if (_gameTimeController.MaxTime - _gameTimeController.GameTimeProperty.Value >= _rabbitCreateDataList[currentPhase].CreateTime)
            {
                //どこに生成するかを決定する
                var rabbitCreateData = _rabbitCreateDataList[currentPhase];
                //生成位置のデータが複数ある場合どれを採用するか抽選する
                var random = Random.Range(0, rabbitCreateData.RabbitCreatePosDataArray.Length);
                //ノーマルウサギの生成
                CreateRabbits(RabbitType.Normal, rabbitCreateData, random);
                //銀ウサギの生成
                CreateRabbits(RabbitType.Silver, rabbitCreateData, random);
                //金ウサギの生成
                CreateRabbits(RabbitType.Gold, rabbitCreateData, random);
                //フェーズを変更
                currentPhase++;
            }
            
            //フェーズを返す
            return currentPhase;
        }

        /// <summary>
        /// 初期化用の処理
        /// </summary>
        public void Initialize()
        {
            //フィールドタイルをListに格納
            for (var y = 0; y < _fieldSize.y; y++)
            {
                for (var x = 0; x < _fieldSize.x; x++)
                {
                    _fieldGridList.Add(_fieldTileObjects[x + y * _fieldSize.x]);
                }
            }
        }

        /// <summary>
        /// 毎フレームおこなう処理
        /// </summary>
        public void MyUpdate()
        {
            //フェーズの更新チェックをおこなう
            _currentPhase = UpdateCheckPhase(_currentPhase);
            
            //グリッドの更新をおこなう
            foreach (var grid in _fieldGridList)
            {
                grid.MyUpdate();
            }
        }
    }
}
