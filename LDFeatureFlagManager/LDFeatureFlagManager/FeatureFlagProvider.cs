using System;
using System.Collections.Generic;
using System.Text;

namespace LDFeatureFlagManager
{
    public static class FeatureFlagProvider
    {
        public static IFeatureFlagProvider Instance { get; set; }
    }
}
