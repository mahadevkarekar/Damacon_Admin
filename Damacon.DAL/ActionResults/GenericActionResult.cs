using System;
using System.Collections.Generic;
using System.Linq;

namespace Damacon.DAL
{
    [Serializable]
    public class GenericActionResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }

    [Serializable]
    public class GenericActionResult<T> : GenericActionResult
    {
        public List<T> Result { get; set; }

        public T GetSingleResult()
        {
            if (Result != null && Result.Count == 1)
            {
                return Result.First();
            }
            return default(T);
        }

        internal void SetSingleResult(T instance)
        {
            if (Result == null)
            {
                Result = new List<T>() { instance};
            }
        }
    }
}
