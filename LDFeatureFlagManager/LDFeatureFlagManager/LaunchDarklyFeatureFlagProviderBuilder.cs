using LaunchDarkly.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace LDFeatureFlagManager
{
    public class LaunchDarklyFeatureFlagProviderBuilder
    {
        private const int MIN_POLLING_INTERVAL = 5;
        private const int DEFAULT_POLLING_INTERVAL = 60;

        private const int DEFAULT_REPORT_USAGE_INTERVAL = 2;

        private const int MIN_REPORT_USAGE_BUFFER = 100;
        private const int DEFAULT_REPORT_USAGE_BUFFER = 500;

        private const int MIN_REPORT_USAGE_INTERVAL = 1;
        private const int MAX_REPORT_USAGE_INTERVAL = 9;

        private readonly Configuration _configuration;
        private bool _cacheFlagValuesForDefaultUser;
        private static readonly object LockObject = new object();

        private LaunchDarklyFeatureFlagProviderBuilder(string sdkKey)
        {
            _configuration = Configuration.Default(sdkKey)
                .WithPollingInterval(TimeSpan.FromSeconds(DEFAULT_POLLING_INTERVAL))
                .WithEventQueueFrequency(TimeSpan.FromSeconds(DEFAULT_REPORT_USAGE_INTERVAL))
                .WithEventQueueCapacity(DEFAULT_REPORT_USAGE_BUFFER);
        }

        public static LaunchDarklyFeatureFlagProviderBuilder CreateWithKey(string sdkKey)
        {
            return new LaunchDarklyFeatureFlagProviderBuilder(sdkKey);
        }
    }
}
