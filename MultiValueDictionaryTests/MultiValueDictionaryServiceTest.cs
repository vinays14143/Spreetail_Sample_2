using Moq;
using MultiValueDictionary.src.CommandManager;
using MultiValueDictionary.src.Enums;
using MultiValueDictionary.src.Services;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultiValueDictionaryTests
{
    [TestFixture]
    public class MultiValueDictionaryServiceTest
    {
        private Dictionary<string, string> _testData = new Dictionary<string, string>();
        private Mock<IMultiValueDataReadDictionary> _mockMultiValueDataReadDictionary;
        private IMultiValueDataWriteDictionary _multiValueDataWriteDictionary;
        private IMultiValueDataReadDictionary _multiValueDataReadDictionary;
        private static ConcurrentDictionary<string, List<string>> _readWriteDictionary;

    
        [OneTimeSetUp]
        public void InitialSetup()
        {
            _mockMultiValueDataReadDictionary = new Mock<IMultiValueDataReadDictionary>();
            _readWriteDictionary = new ConcurrentDictionary<string, List<string>>();
            _multiValueDataWriteDictionary = new MultiValueWriteDictionaryService(_readWriteDictionary);
            _multiValueDataReadDictionary = new MultiValueReadDictionaryService(_readWriteDictionary);
        }

        [SetUp]
        public void Setup()
        {
            AddData();
        }

        [Test]
        public void GetAllKeys_From_Dictionary()
        {
            _mockMultiValueDataReadDictionary.Setup(x => x.GetAllKeys()).Returns(new MultiValueDictionary.src.Model.MultiValueDictionaryResult($"Empty Dictionary", true));
            var commandManager = new CommandManager(_multiValueDataReadDictionary, _multiValueDataWriteDictionary);
            var result = commandManager.MultiValueDictionaryOperation(MultiValueDictionaryCommand.KEYS, "", "");
            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public void AddKeys_To_Dictionary()
        {
            var result = _multiValueDataWriteDictionary.AddMemberForAKey("spreetrail","test");
            var keys = _multiValueDataReadDictionary.GetAllKeys();
            Assert.IsTrue(result.IsSuccess);
            Assert.Contains("spreetrail", keys.OutputValue.Split('\n'));
        }

        [Test]
        public void AddKeys_Return_False_If_Member_Already_Exists()
        {
            var result = _multiValueDataWriteDictionary.AddMemberForAKey("foo", "bar");
            Assert.IsFalse(result.IsSuccess);
        }

        [Test]
        public void Remove_Memebers_Returns_False_If_Member_Exists_For_A_Key()
        {
            var result = _multiValueDataWriteDictionary.RemoveMembersAndKey("foo", "baz");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("ERROR, memeber baz does not exists", result.Message);

        }

        [Test]
        public void Remove_Memebers_Removes_LastMember_With_Key()
        {
            var result = _multiValueDataWriteDictionary.RemoveMembersAndKey("foo", "bar");
            Assert.IsTrue(result.IsSuccess);
            Assert.AreEqual($"Last member and its key is removed", result.Message);
            var keys = _multiValueDataReadDictionary.GetAllKeys();
            Assert.IsFalse(keys.OutputValue.Split('\n').Contains("foo"));
        }

        [TearDown]
        public void Test_Tear_Down()
        {
            _multiValueDataWriteDictionary.RemoveAllMembersAndKeys();
            _testData.Clear();
        }
        private void AddData()
        {
            
            _testData.TryAdd("foo", "bar");
            _testData.TryAdd("boom", "pow");
            _testData.TryAdd("bang", "baz");
            foreach(var item in _testData)
            {
                _multiValueDataWriteDictionary.AddMemberForAKey(item.Key, item.Value);
            }
        }
    }
}