using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Xunit;

namespace TeresasTheoryQuestion
{
    public class Tests
    {
        public static RuntimeTheoryData RuntimeData = new RuntimeTheoryData();
        
        [Theory]    
        [MemberData(nameof(RuntimeData))]
        public void Test1(string name, bool enabled)
        {
            if (enabled)
            { 
                Assert.DoesNotContain("!", name);
            }
            
            Assert.True(true);
        }
    }

    public class RuntimeTheoryData : TheoryData<string, bool>
    {
        public RuntimeTheoryData()
        {
            var oddMinute = (DateTime.Now.Minute % 2) == 1;
            var jsonData = File.ReadAllText(oddMinute ? "version1.json" : "version2.json");
            var testData = JsonConvert.DeserializeObject<TestInstruction[]>(jsonData);
            foreach (var instruction in testData)
            {
                Add(instruction.Name, instruction.Enabled);
            }
        }
    }

    public class TestInstruction
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
    }
}