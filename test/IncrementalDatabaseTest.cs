namespace Incremental
{
    using System;
    using System.Linq;
    using Xunit;

    public class IncrementalDatabaseTest
    {
        public static readonly TheoryData<Func<IIncrementalDatabase>> Databases = new TheoryData<Func<IIncrementalDatabase>>
        {
            () => new GitIncrementalDatabase("incremental-test-db"),
        };

        [Theory, MemberData(nameof(Databases))]
        public void put_lookup_function(Func<IIncrementalDatabase> factory)
        {
            using (var db = factory())
            {
                var inputs = Enumerable.Range(0, 4).Select(i => new Hash(Guid.NewGuid().ToByteArray())).ToArray();
                var outputs = Enumerable.Range(0, 2).Select(i => new Hash(Guid.NewGuid().ToByteArray())).ToArray();
                var outputs2 = Enumerable.Range(0, 2).Select(i => new Hash(Guid.NewGuid().ToByteArray())).ToArray();

                Assert.Empty(db.LookupFunction("function name", "1", new Hash[0]));

                db.PutFunction("a function name", "1.0.1-ac8f0ea", inputs, outputs);
                Assert.Equal(outputs, db.LookupFunction("a function name", "1.0.1-ac8f0ea", inputs));

                db.PutFunction("a function name", "1.0.1-ac8f0ea", inputs, outputs2);
                Assert.Equal(outputs2, db.LookupFunction("a function name", "1.0.1-ac8f0ea", inputs));
            }
        }

        [Theory, MemberData(nameof(Databases))]
        public void read_write_blob(Func<IIncrementalDatabase> factory)
        {
            using (var db = factory())
            {
                Assert.Null(db.OpenReadBlob(new Hash(Guid.NewGuid().ToByteArray())));

                using (var writer = db.OpenWriteBlob())
                {
                    var bytes = new byte[] { 0, 0, 0, 0 };
                    writer.Write(new byte[] { 0, 0, 0, 0 }, 0, 4);

                    var hash = writer.CloseAndGetHash();
                    Assert.Equal("593f4708db84ac8fd0f5cc47c634f38c013fe9e4", hash.ToString());

                    using (var reader = db.OpenReadBlob(hash))
                    {
                        Assert.Equal(0, reader.ReadByte());
                        Assert.Equal(0, reader.ReadByte());
                        Assert.Equal(0, reader.ReadByte());
                        Assert.Equal(0, reader.ReadByte());
                        Assert.Equal(-1, reader.ReadByte());
                    }
                }
            }
        }
    }
}
