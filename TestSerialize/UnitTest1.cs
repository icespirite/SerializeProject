using System;
using Xunit;
using SerializeProject;
using System.IO;

namespace TestSerialize
{
    public class UnitTest1
    {
        [Fact]
        public void SerializationTest()
        {
            Stream stream = new MemoryStream();
            ListRandom randomListChecking = new ListRandom();
            ListRandom randomListCheckable = new ListRandom();
            for (int i = 0; i < 8; i++)
            {
                randomListChecking.Add($"Some string {i + 1}");
            }
            randomListChecking.SetRandom();
            randomListChecking.Serialize(stream);
            stream.Position = 0;
            randomListCheckable.Deserialize(stream);
            Assert.Equal(8, randomListCheckable.Count);
            var currentNodeChecking = randomListChecking.Head;
            var currentNodeCheckable = randomListCheckable.Head;
            for (int i = 0; i < 8; i++)
            {
                Assert.Equal(currentNodeChecking.Data, currentNodeCheckable.Data);
                if (currentNodeChecking.Previous != null && currentNodeCheckable.Previous != null)
                    Assert.Equal(currentNodeChecking.Previous.Data, currentNodeCheckable.Previous.Data);

                Assert.Equal(currentNodeChecking.Random.Data, currentNodeCheckable.Random.Data);

                if (currentNodeChecking.Random.Previous != null && currentNodeCheckable.Random.Previous != null)
                    Assert.Equal(currentNodeChecking.Random.Previous.Data, currentNodeCheckable.Random.Previous.Data);

                if (currentNodeChecking.Random.Next != null && currentNodeCheckable.Random.Next != null)
                    Assert.Equal(currentNodeChecking.Random.Next.Data, currentNodeCheckable.Random.Next.Data);

                currentNodeChecking = currentNodeChecking.Next;
                currentNodeCheckable = currentNodeCheckable.Next;

            }


        }
    }
}
