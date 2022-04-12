using MultiValueDictionary.Exceptions;
using MultiValueDictionary.src.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionary.src.Services
{
    public class MultiValueReadDictionaryService : IMultiValueDataReadDictionary
    {

        private ConcurrentDictionary<string, List<string>> _readWriteDictionary;

        public MultiValueReadDictionaryService(ConcurrentDictionary<string, List<string>> readWriteDictionary)
        {
            _readWriteDictionary = readWriteDictionary;
        }
        /// <summary>
        /// Get all keys from dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllKeys()
        {
            try
            {
                var keys = _readWriteDictionary.Keys;
                if (keys.Any())
                {
                    var result = string.Join("\n", keys);
                    return new MultiValueDictionaryResult($"all keys", true, result);
                }

                return new MultiValueDictionaryResult($"Empty Dictionary", true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }


        /// <summary>
        /// Get all the members for a given key from the dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllMembersForAKey(string key)
        {
            try
            {
                if (_readWriteDictionary.TryGetValue(key, out var values))
                {
                    var result = string.Join("\n", values);
                    return new MultiValueDictionaryResult($"all members for a key {key}", true, result);

                }

                return new MultiValueDictionaryResult($"ERROR, key not found", false);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }

        }

        /// <summary>
        /// Get all the members of a dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllMembers()
        {
            try
            {
                var values = _readWriteDictionary.SelectMany(x => x.Value).ToList();
                if (values.Any())
                {
                    var result = string.Join("\n", _readWriteDictionary.SelectMany(x => x.Value).ToList());
                    return new MultiValueDictionaryResult($"all members", true, result);

                }

                return new MultiValueDictionaryResult($"no members", true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }

        /// <summary>
        /// Get all the keys with its members from dictionary
        /// </summary>
        /// <returns></returns>
        public MultiValueDictionaryResult GetAllKeysWithMembers()
        {
            try
            {
                var result = string.Join("\n", _readWriteDictionary.SelectMany(x => x.Value.Select(r => x.Key + " : " + r)));

                if (!string.IsNullOrEmpty(result))
                {
                    return new MultiValueDictionaryResult($"all keys and members", true, result);
                }

                return new MultiValueDictionaryResult($"no keys and members", true);
            }
            catch (Exception ex)
            {
                throw new MultiValueDictionaryException(ex.ToString());
            }
        }
    }
}
