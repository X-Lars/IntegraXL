﻿using IntegraXL.Core;
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
        private static ConcurrentDictionary<Type, Dictionary<int, FieldInfo>> _CachedFields = new();

        /// <summary>
        /// Thread safe cache to store a model's dictionary of <see cref="OffsetAttribute"/> decorated properties.
        /// </summary>
        private static ConcurrentDictionary<Type, Dictionary<string, int>> _CachedProperties = new();


        //internal static FieldInfo? Field<T>(this IntegraModel<T> instance, int offset)
        //{
        //    var fields = CachedFields(instance);

        //    if(fields.TryGetValue(offset, out FieldInfo? value))
        //    {
        //        return value;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        internal static Dictionary<int, FieldInfo> CachedFields<T>(this IntegraModel<T> instance)
        {
            if (_CachedFields.TryGetValue(typeof(T), out Dictionary<int, FieldInfo>? fields))
            {
                return fields;
            }
            else
            {
                throw new IntegraException($"[{nameof(IntegraModelExtensions)}.{nameof(CachedFields)}({typeof(T).Name})]");
            }
        }

        public static Dictionary<string, int> CachedProperties<T>(this IntegraModel<T> instance)
        {
            if (_CachedProperties.TryGetValue(typeof(T), out Dictionary<string, int>? properties))
            {
                //Debug.Print($"{nameof(IntegraModelExtensions)}.{nameof(CachedProperties)}({instance.GetType().Name}) 0x{instance.GetTypeHash().ToString("X8")}");
                return properties;
            }
            else
            {
                Debug.Assert(properties != null);
                return null;
                throw new IntegraException($"[{nameof(IntegraModelExtensions)}.{nameof(CachedProperties)}({typeof(T).Name})]");
            }
        }

        // TODO: exclude templates
        public static bool Cache<T>(this IntegraModel<T> instance)// where TModel: IntegraModel
        {
            //int key = instance.GetTypeHash();

            if (_CachedFields.ContainsKey(typeof(T)))
            {
                return true;
            }

            // TODO: Remove double checked in base class
            var integraFields = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic)
                .Where(x => x.GetCustomAttribute<OffsetAttribute>() != null);

            if (!integraFields.Any())
                return false;

            Debug.Print($"[{nameof(IntegraModelExtensions)}.{nameof(Cache)}<{instance.GetType().Name}>()");

            Dictionary<int, FieldInfo> cachedFields = new();

            foreach (var field in integraFields)
            {
                Debug.Assert(field.GetCustomAttribute<OffsetAttribute>() != null);

                cachedFields.TryAdd(field.GetCustomAttribute<OffsetAttribute>().Value, field);

            }

            if (cachedFields.Any())
            {
                _CachedFields.TryAdd(typeof(T), cachedFields);
            }
            else
            {
                // If there are no fields to cache it is useless to cache properties
                return false;
            }


            var integraProperties = instance.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => x.GetCustomAttribute<OffsetAttribute>() != null);

            if (!integraProperties.Any())
                return false;

            Dictionary<string, int> cachedProperties = new();
            
            foreach (var property in integraProperties)
            {
                Debug.Assert(property.GetCustomAttribute<OffsetAttribute>() != null);

                cachedProperties.TryAdd(property.Name, property.GetCustomAttribute<OffsetAttribute>().Value);
            }

            if (cachedProperties.Any())
                _CachedProperties.TryAdd(typeof(T), cachedProperties);

            return true;
            //instance.IsCached = true;
        }
    }
}
