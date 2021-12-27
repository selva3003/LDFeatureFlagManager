using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using LaunchDarklyClient = global::LaunchDarkly.Client;
using LaunchDarkly.Client;

namespace LDFeatureFlagManager
{
    public class LaunchDarklyFeatureFlagProvider : IFeatureFlagProvider
    {
        private readonly bool _cacheFlagValuesForDefaultUser;
        private readonly LaunchDarklyClient.ILdClient _ldClient;
        private readonly IDictionaryCache<object> _defaultUserFlagCache = new DictionaryCache<object>();

        public LaunchDarklyFeatureFlagProvider(LaunchDarklyClient.Configuration configuration, bool cacheFlagValuesForDefaultUser = false)
        {
            _cacheFlagValuesForDefaultUser = cacheFlagValuesForDefaultUser;
            _ldClient = new LaunchDarklyClient.LdClient(configuration);
            _defaultUserFlagCache.LifeSpan = (uint)configuration.EventQueueFrequency.TotalSeconds;

           
        }

        public bool IsFeatureEnabled<T>(User user, bool defaultValue = false) where T : FeatureFlag<bool>, new()
        {
            var ldUser = CreateUser(user);
            return _ldClient.BoolVariation(new T().Keyname, ldUser, defaultValue);
        }


        public bool GetBoolFlag<T>(User user, bool defaultValue = false) where T : FeatureFlag<bool>, new()
        {
            var ldUser = CreateUser(user);
            return _ldClient.BoolVariation(new T().Keyname, ldUser, defaultValue);
        }

        public int GetIntFlag<T>(User user, int defaultValue) where T : FeatureFlag<int>, new()
        {
            var ldUser = CreateUser(user);
            return _ldClient.IntVariation(new T().Keyname, ldUser, defaultValue);
        }

        public string GetStringFlag<T>(User user, string defaultValue) where T : FeatureFlag<string>, new()
        {
            var ldUser = CreateUser(user);
            return _ldClient.StringVariation(new T().Keyname, ldUser, defaultValue);
            
        }

        private TReturn GetFlagForDefaultUser<TReturn>(string flagName, Func<object> loadFunc)
        {
            if (_cacheFlagValuesForDefaultUser)
            {
                return (TReturn)_defaultUserFlagCache.GetOrLoadNewObject(flagName, loadFunc);
            }

            return (TReturn)loadFunc();
        }

        private LaunchDarklyClient.User CreateUser(User user)
        {
            return new LaunchDarklyClient.User(user.Key)
            {
                Email = user.Email,
            };
        }

        private Dictionary<string, JToken> GetCustomAttributes(IDictionary<string, object> customParameters)
        {
            var customValues = new Dictionary<string, JToken>();

            if (customParameters == null)
                return customValues;

            foreach (var customParameter in customParameters)
            {
                customValues.Add(customParameter.Key, new JValue(customParameter.Value));
            }

            return customValues;
        }
    }
}
