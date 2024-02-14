using UnityEngine;

namespace PullAnimals
{
    public class WeedRenderController : MonoBehaviour
    {
        /// <summary>
        /// 草のマテリアル
        /// </summary>
        [SerializeField] private Material _material;
        /// <summary>
        /// 草のメッシュ
        /// </summary>
        [SerializeField] private Mesh _mesh;
        /// <summary>
        /// 生やす草の数
        /// </summary>
        [SerializeField]　private int _weedNum;
        /// <summary>
        /// 生やす草の位置
        /// </summary>
        private readonly Vector2[,] _randomVector2 = new Vector2[4,1000];

        private void Start()
        {
            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < _weedNum; ++j)
                {
                    switch (i)
                    {
                        //上方向
                        case 0:
                            _randomVector2[i,j] = new Vector2(Random.Range(-25f, 25f), Random.Range(22f, 80f));
                            break;
                        //右側
                        case 1:
                            _randomVector2[i,j] = new Vector2(Random.Range(15, 50f), Random.Range(-5f, 25f));
                            break;
                        //下側
                        case 2:
                            _randomVector2[i,j] = new Vector2(Random.Range(-25f, 25f), Random.Range(-68f, 0f));
                            break;
                        //左側
                        case 3:
                            _randomVector2[i,j] = new Vector2(Random.Range(-50f, -15f), Random.Range(-5f, 25f));
                            break;
                    }
                }
            }
        }
  
        private void Update()
        {
            var rp = new RenderParams(_material);
            var instData = new Matrix4x4[_weedNum];

            for (var i = 0; i < 4; ++i)
            {
                for (var j = 0; j < _weedNum; ++j)
                {
                    instData[j] = Matrix4x4.Translate(new Vector3(_randomVector2[i,j].x, 0.1f, _randomVector2[i,j].y));    
                }
                
                Graphics.RenderMeshInstanced(rp, _mesh, 0, instData);
            }

        }
    }
}
