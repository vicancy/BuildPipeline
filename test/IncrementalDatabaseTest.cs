namespace Incremental
{
    using System;
    using Xunit;

    public class IncrementalDatabaseTest
    {
        public static readonly TheoryData<Func<IncrementalDatabase>> Databases = new TheoryData<Func<IncrementalDatabase>>
        {
            () => new DefaultDatabase("");
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
