namespace Incremental
{
    using System;
    using Xunit;

    public class IncrementalDatabaseTest
    {
        public static readonly TheoryData<Func<IncrementalDatabase>> Databases = new TheoryData<Func<IncrementalDatabase>>
        {
            () => new GitIncrementalDatabase(""),
        };

        [Fact]
        public void Test1()
        {

        }
    }
}