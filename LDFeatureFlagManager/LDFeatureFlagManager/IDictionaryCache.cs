using System;
using System.Collections.Generic;
using System.Text;

namespace LDFeatureFlagManager
{
    internal interface IDictionaryCache<T>
    {
        T GetOrLoadNewObject(string key, Func<T> loadNewObject);
        uint LifeSpan { get; set; }
    }
}
