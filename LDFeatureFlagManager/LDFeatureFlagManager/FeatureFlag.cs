using System;
using System.Collections.Generic;
using System.Text;

namespace LDFeatureFlagManager
{
    public abstract class FeatureFlag<T>
    { 
        public abstract string Keyname { get; }
    }
}
