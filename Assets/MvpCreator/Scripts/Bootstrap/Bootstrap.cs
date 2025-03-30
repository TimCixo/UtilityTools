using System.Collections.Generic;
using UnityEngine;

namespace MvpCreator
{
    public class Bootstrap : MonoBehaviour, IBootstrapable
    {
        [SerializeField, Tooltip("Is this object bootstrapable?")]
        private bool _isBootstrapable;
        [SerializeField, Tooltip("Components to bootstrap")]
        private List<MonoBehaviour> _components = new List<MonoBehaviour>();

        private void Awake()
        {
            if (_isBootstrapable)
            {
                return;
            }

            BootstrapInit();
        }

        public void BootstrapInit()
        {
            foreach (MonoBehaviour component in _components)
            {
                if (component is IBootstrapable bootstrapable)
                {
                    bootstrapable.BootstrapInit();
                }
            }
        }
    }
}