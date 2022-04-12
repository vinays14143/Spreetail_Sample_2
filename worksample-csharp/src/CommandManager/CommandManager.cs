using MultiValueDictionary.src.Enums;
using MultiValueDictionary.src.Model;
using MultiValueDictionary.src.Services;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace MultiValueDictionary.src.CommandManager
{
    public interface ICommandManager
    {
        MultiValueDictionaryResult MultiValueDictionaryOperation(MultiValueDictionaryCommand input, string key, string value);
    }
    public class CommandManager : ICommandManager
    {
        private readonly IMultiValueDataReadDictionary _multiValueDataReadDictionary;

        private readonly IMultiValueDataWriteDictionary _multiValueDataWriteDictionary;

        public CommandManager(MultiValueReadDictionaryService multiValueDataReadDictionary, MultiValueWriteDictionaryService multiValueDataWriteDictionary)
        {
            _multiValueDataReadDictionary = multiValueDataReadDictionary;
            _multiValueDataWriteDictionary = multiValueDataWriteDictionary;
        }
        public CommandManager(IMultiValueDataReadDictionary multiValueDataReadDictionary, IMultiValueDataWriteDictionary multiValueDataWriteDictionary)
        {
            _multiValueDataReadDictionary = multiValueDataReadDictionary;
            _multiValueDataWriteDictionary = multiValueDataWriteDictionary;
        }
        /// <summary>
        /// acts as client to get the requested dictionary operation results
        /// </summary>
        /// <param name="input"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public MultiValueDictionaryResult MultiValueDictionaryOperation(MultiValueDictionaryCommand input, string key, string value)
        {
            switch(input)
            {
                case MultiValueDictionaryCommand.KEYS:
                    return _multiValueDataReadDictionary.GetAllKeys();

                case MultiValueDictionaryCommand.MEMBERS:
                    return _multiValueDataReadDictionary.GetAllMembersForAKey(key);

                case MultiValueDictionaryCommand.ADD:
                    return _multiValueDataWriteDictionary.AddMemberForAKey(key, value);

                case MultiValueDictionaryCommand.REMOVE:
                    return _multiValueDataWriteDictionary.RemoveMembersAndKey(key,value);

                case MultiValueDictionaryCommand.REMOVEALL:
                    return _multiValueDataWriteDictionary.RemoveAllMembersAndKey(key);

                case MultiValueDictionaryCommand.CLEAR:
                    return _multiValueDataWriteDictionary.RemoveAllMembersAndKeys();

                case MultiValueDictionaryCommand.KEYEXISTS:
                    return _multiValueDataWriteDictionary.IfKeyExists(key);

                case MultiValueDictionaryCommand.MEMBEREXISTS:
                    return _multiValueDataWriteDictionary.IfMemberExists(key,value);

                case MultiValueDictionaryCommand.ALLMEMBERS:
                    return _multiValueDataReadDictionary.GetAllMembers();

                case MultiValueDictionaryCommand.ITEMS:
                    return _multiValueDataReadDictionary.GetAllKeysWithMembers();
            }
            return new MultiValueDictionaryResult("Invalid operation", false);
        }
    }
}
