using System;
using Xunit;
using SerializeProject;
using System.IO;

namespace TestSerialize
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Stream stream = new MemoryStream();
            ListRandom random = new ListRandom();
            ListRandom random2 = new ListRandom();
            for (int i = 0; i < 8; i++)
            {
                random.Add($"Some string {i + 1}");
            }
            random.SetRandom();
            random.Serialize(stream);
            stream.Position = 0;
            random2.Deserialize(stream);
            Assert.Equal(8, random2.Count);
            var perem = random.Head;
            var perem2 = random2.Head;
            for (int i = 0; i < 8; i++)
            {
                Assert.Equal($"Some string {i + 1}", perem2.Data);
                if (i != 0)
                   Assert.Equal($"Some string {i}", perem2.Previous.Data);
                if (i != 7)
                   Assert.Equal($"Some string {i + 2}", perem2.Next.Data);
                   //Assert.Equal(perem.Random.Data, perem2.Random.Data);
                if (i != 0)
                   // Assert.Equal(perem.Random.Previous.Data, perem2.Random.Previous.Data);
                if (i != 7)
                   // Assert.Equal(perem.Random.Next.Data, perem2.Random.Next.Data);
                
                    perem = perem.Next;
                    perem2 = perem2.Next;
               
            }


        }
    }
}
