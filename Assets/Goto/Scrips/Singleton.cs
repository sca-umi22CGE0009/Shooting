using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    
        public interface ISingleton
        {
            bool DestroyTragetGameObject { get; }
            void Init();
            void OnDestroy();
            void OnRelease();
        }

        /// <summary>
        /// シングルトンクラス
        /// クラスのインスタンスを一つに制限する
        /// グローバルなアクセスポイントを提供する
        /// デザインパターンの一種(Singleton)
        /// </summary>(引用URL）
        /// https://marunaka-blog.com/cshap-singleton/5433/
        public class Singleton<T> : MonoBehaviour, ISingleton where T : Singleton<T>
        {
            public virtual bool DestroyTragetGameObject => false;
            public static T Instance { get; private set; } = null;

            ///<summary>
            ///Singletonが有効かどうか
            /// </summary>
            public static bool IsValid() => Instance != null;

            private void Awake()
            {
                if (Instance == null)
                {
                    Instance = this as T;
                    Instance.Init();
                    return;
                }
                if (DestroyTragetGameObject)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(this);
                }
            }

            ///<summary>
            ///派生クラス用のAwake
            /// </summary>
            public virtual void Init() { }

            public void OnDestroy()
            {
                if (Instance == this)
                {
                    Instance = null;
                }
                OnRelease();
            }

            public virtual void OnRelease() { }
        }
    
}