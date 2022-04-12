using MultiValueDictionary.src.Model;

namespace MultiValueDictionary.src.Services
{
    public interface IMultiValueDataReadDictionary
    {
        public MultiValueDictionaryResult GetAllKeys();

        public MultiValueDictionaryResult GetAllMembersForAKey(string key);

        public MultiValueDictionaryResult GetAllMembers();

        public MultiValueDictionaryResult GetAllKeysWithMembers();
    }
}
