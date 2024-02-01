using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace PullAnimals
{
    /// <summary>
    /// FPSのデバッグ用クラス
    /// </summary>
    public class FPSDebugger : MonoBehaviour
    {
        [SerializeField] private Text _text;

        // Update is called once per frame
        void Update()
        {
            _text.text = "FPS is" + (1f / Time.deltaTime).ToString(CultureInfo.InvariantCulture);
        }
    }
}
