using UnityEngine;

namespace PullAnimals.Singleton
{
    /// <summary>
    /// シーンを跨いでも破棄されない & MonoBehaviourを継承したシングルトンの親クラス
    /// </summary>
    public class DDMonoSingletonBase<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// インスタンス 
        /// </summary>
        private static T _instance;
        
        /// <summary>
        /// インスタンスのGetter 
        /// </summary>
        public static T Instance
        {
            get
            {
                //インスタンスが存在するなら返す
                if (_instance != null) return _instance;
                
                //インスタンスがなければシーン内から探して取得
                _instance = (T)FindObjectOfType(typeof(T));

                //インスタンスがシーン内になければ警告
                if (_instance == null) Debug.LogWarning($"{typeof(T)}がシーン内に存在しません！！");

                //取得したインスタンスを返す
                return _instance;
            }
        }

        /// <summary>
        /// シーン開始時の処理,virtualにすることで上書きを可能に
        /// </summary>
        protected virtual void Awake()
        {
            //複数同じインスタンスがあれば削除する
            SetupInstance();
        }
        

        /// <summary>
        /// シングルトンの設定をおこなう処理
        /// </summary>
        private void SetupInstance()
        {
            if (_instance == null)
            {
                //インスタンスがなければ自身を入れる
                _instance = this as T;
            }
            else
            {
                //インスタンスがあれば自身を破棄する
                Debug.LogWarning($"{typeof(T)}が複数存在したため破棄します");
                Destroy(gameObject);
            }
            
            //シーンが変わっても破棄されないようにする
            DontDestroyOnLoad(gameObject);
        }
    }
}
