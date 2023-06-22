#region

using UnityEngine;

#endregion

namespace Core
{
    public class LoaderCallback : MonoBehaviour
    {
        private bool _firstUpdate = true;

        private void Update()
        {
            if (_firstUpdate)
            {
                Loader.LoaderCallback();
                _firstUpdate = false;
            }
        }
    }
}