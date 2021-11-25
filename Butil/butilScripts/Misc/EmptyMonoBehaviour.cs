using UnityEngine;

namespace b33bo.utils.emptyMonoBehaviour
{
    /// <summary>An empty gameobject, containing a monobehaviour. This is useful for static scripts</summary>
    public class EmptyMonoBehaviour : MonoBehaviour
    {
        /// <summary>An empty gameobject, containing a monobehaviour. This is useful for static scripts</summary>
        public static EmptyMonoBehaviour EmptyMonobehaviour
        {
            get
            {
                if (m_emptyMonoBehaviour == null)
                {
                    GameObject gameObject = new GameObject("Empty MonoBehaviour");
                    m_emptyMonoBehaviour = gameObject.AddComponent<EmptyMonoBehaviour>();
                }

                return m_emptyMonoBehaviour;
            }
        }

        private static EmptyMonoBehaviour m_emptyMonoBehaviour;
    }
}
