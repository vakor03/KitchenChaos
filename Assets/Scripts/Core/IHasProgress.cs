#region

using System;

#endregion

namespace Core
{
    public interface IHasProgress
    {
        public event Action<float> OnProgressChanged;
    }
}