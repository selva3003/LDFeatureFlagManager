using LaunchDarkly.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFeatureFlagManager
{
    public interface IFeatureFlagProvider
    {
        bool IsFeatureEnabled<T>(User user, bool defaultValue = false) where T : FeatureFlag<bool>, new();

        bool GetBoolFlag<T>(User user, bool defaultValue = false) where T : FeatureFlag<bool>, new();

        int GetIntFlag<T>(User user, int defaultValue) where T : FeatureFlag<int>, new();

        string GetStringFlag<T>(User user, string defaultValue) where T : FeatureFlag<string>, new();
    }
}
