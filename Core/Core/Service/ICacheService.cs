using System;
using System.Collections.Generic;
using System.Text;

namespace Core
{
   public interface ICacheService
    {
        T Get<T>(string key);
        void Add(string key, object value);
        void Remove(string key);
        void Clear();
        bool Any(string key);
    }
}
