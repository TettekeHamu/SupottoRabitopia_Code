using UniRx;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace PullAnimals
{
    /// <summary>
    /// 時間に応じてSkyBoxを変化させるクラス
    /// </summary>
    public class GameLightController : MonoBehaviour
    {
        /// <summary>
        /// ゲーム開始時と終了時のライト情報を格納するScriptableObject
        /// </summary>
        [SerializeField] private SceneLightingData _lightingData;
        /// <summary>
        /// DirectionalLight（太陽・月明り）
        /// </summary>
        [SerializeField] private Light _light;
        /// <summary>
        /// スカイボックスのMaterial
        /// </summary>
        [SerializeField] private Material _skyBoxMaterial;
        /// <summary>
        /// ゲームに関する時間を管理するクラス
        /// </summary>
        [SerializeField] private GameTimeController _timeController;
        /// <summary>
        /// 空の色（明るさ）を変更する際に使うプロパティ
        /// </summary>
        private static readonly int CubemapTransition = Shader.PropertyToID("_CubemapTransition");
        /// <summary>
        /// 空の色（配色）を変更する際に使うプロパティ
        /// </summary>
        private static readonly int TintColor = Shader.PropertyToID("_TintColor");
        /// <summary>
        /// スタート時の空のカラー
        /// </summary>
        private Color _startSkyBoxColor;
        /// <summary>
        /// 終了時の空のカラー
        /// </summary>
        private Color _endSkyBoxColor;
        /// <summary>
        /// スタート時のDirectionalLightのカラー
        /// </summary>
        private Color _startLightColor;
        /// <summary>
        /// 終了時のDirectionalLightのカラー
        /// </summary>
        private Color _endLightColor;

        private void Awake()
        {
            //カラーコードを変換
            _startSkyBoxColor = ChangeColorFromCode(_lightingData.StartSkyBoxColorCode);
            _endSkyBoxColor = ChangeColorFromCode(_lightingData.EndSkyBoxColorCode);
            _startLightColor = ChangeColorFromCode(_lightingData.StartLightColorCode);
            _endLightColor = ChangeColorFromCode(_lightingData.EndLightColorCode);
            //初期化
            _skyBoxMaterial.SetFloat(CubemapTransition,0f);
            _skyBoxMaterial.SetColor(TintColor, _startSkyBoxColor);
            _light.color = _startSkyBoxColor;
            _timeController.GameTimeProperty
                .Subscribe(x => UpdateLighting(_timeController.MaxTime - x, _timeController.MaxTime))
                .AddTo(this);
        }

        /// <summary>
        /// 段々とLightingを暗くしていく処理
        /// </summary>
        private void UpdateLighting(float currentTime, float maxTime)
        {
            var newColor = Color.Lerp(_startSkyBoxColor, _endSkyBoxColor, currentTime / maxTime);
            _skyBoxMaterial.SetFloat(CubemapTransition, currentTime / maxTime);
            _skyBoxMaterial.SetColor(TintColor, newColor);
            var newLightColor = Color.Lerp(_startLightColor, _endLightColor, currentTime / maxTime);
            _light.color = newLightColor;
        }

        /// <summary>
        /// カラーコードからColor型に変換をおこなう処理
        /// </summary>
        /// <param name="colorCode">カラーコード</param>
        /// <returns>カラー（変換できなければ真っ赤にする）</returns>
        private Color ChangeColorFromCode(string colorCode)
        {
            return ColorUtility.TryParseHtmlString(colorCode, out var color) ? color : Color.red;
        }
    }
}
