using IntegraXL.Core;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;

namespace IntegraXL.Extensions
{
    internal static class IntegraModelExtensions
    {
        /// <summary>
        /// Thread safe cache to store a model's dictionary of <see cref="OffsetAttribute"/> decorated fields.
        /// </summary>
        private static ConcurrentDictionary<int, Dictionary<int, FieldInfo>> _CachedFields = new();

        /// <summary>
        /// Thread safe cache to store a model's dictionary of <see cref="OffsetAttribute"/> decorated properties.
        /// </summary>
        private static ConcurrentDictionary<int, Dictionary<string, int>> _CachedProperties = new();

        internal static Dictionary<int, FieldInfo> CachedFields(this IntegraModel instance)
        {
            if (_CachedFields.TryGetValue(instance.GetTypeHash(), out Dictionary<int, FieldInfo>? fields))
            {
                return fields;
            }
            else
            {
                throw new IntegraException($"[{nameof(IntegraModelExtensions)}.{nameof(CachedFields)}({instance.GetType().Name})]");
            }
        }

        public static Dictionary<string, int> CachedProperties(this IntegraModel instance)
        {
            if (_CachedProperties.TryGetValue(instance.GetTypeHash(), out Dictionary<string, int>? properties))
            {
                //Debug.Print($"{nameof(IntegraModelExtensions)}.{nameof(CachedProperties)}({instance.GetType().Name}) 0x{instance.GetTypeHash().ToString("X8")}");
                return properties;
            }
            else
            {
                //Debug.Assert(properties != null);
                return null;
                throw new IntegraException($"[{nameof(IntegraModelExtensions)}.{nameof(CachedProperties)}({instance.GetType().Name})]");
            }
        }

        // TODO: exclude templates
        public static void Cache(this IntegraModel instance)// where TModel: IntegraModel
        {
            int key = instance.GetTypeHash();

            if (_CachedFields.ContainsKey(key))
            {
                return;
            }

            // TODO: Remove double checked in base class
            var integraFields = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.GetCustomAttribute<OffsetAttribute>() != null);

            if (!integraFields.Any())
                return;

            Debug.Print($"[{nameof(IntegraModelExtensions)}] {nameof(Cache)}<{instance.GetType().Name}>() New Cache Entry 0x{key.ToString("X4")}");

            Dictionary<int, FieldInfo> cachedFields = new();

            foreach (var field in integraFields)
            {
                Debug.Assert(field.GetCustomAttribute<OffsetAttribute>() != null);

                cachedFields.TryAdd(field.GetCustomAttribute<OffsetAttribute>().Value, field);
            }

            if (cachedFields.Any())
            {
                _CachedFields.TryAdd(key, cachedFields);
            }
            else
            {
                // If there are no fields to cache it is useless to cache properties
                return;
            }


            var integraProperties = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetCustomAttribute<OffsetAttribute>() != null);

            if (!integraProperties.Any())
                return;

            Dictionary<string, int> cachedProperties = new();
            
            foreach (var property in integraProperties)
            {
                Debug.Assert(property.GetCustomAttribute<OffsetAttribute>() != null);

                cachedProperties.TryAdd(property.Name, property.GetCustomAttribute<OffsetAttribute>().Value);
            }

            if (cachedProperties.Any())
                _CachedProperties.TryAdd(key, cachedProperties);

            //instance.IsCached = true;
        }
    }
}
