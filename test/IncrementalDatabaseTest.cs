namespace Incremental
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Xunit;

    public class IncrementalDatabaseTest
    {
        public static readonly TheoryData<Func<IncrementalDatabase>> Databases = new TheoryData<Func<IncrementalDatabase>>
        {
            () => new GitIncrementalDatabase("incremental-test-db"),
        };

        [Theory, MemberData(nameof(Databases))]
        public void put_lookup_function(Func<IncrementalDatabase> factory)
        {
            using (var db = factory())
            {
                var inputs = Enumerable.Range(0, 4).Select(i => new CryptoHash(Guid.NewGuid().ToByteArray())).ToArray();
                var outputs = Enumerable.Range(0, 2).Select(i => new CryptoHash(Guid.NewGuid().ToByteArray())).ToArray();
                var outputs2 = Enumerable.Range(0, 2).Select(i => new CryptoHash(Guid.NewGuid().ToByteArray())).ToArray();

                Assert.Empty(db.LookupFunction("unknown function", new CryptoHash[0]));

                db.PutFunction("a function name", inputs, outputs);
                Assert.Equal(outputs, db.LookupFunction("a function name", inputs));

                db.PutFunction("a function name", inputs, outputs2);
                Assert.Equal(outputs2, db.LookupFunction("a function name", inputs));
            }
        }

        [Theory, MemberData(nameof(Databases))]
        public void read_write_blob(Func<IncrementalDatabase> factory)
        {
            using (var db = factory())
            {
                Assert.Null(db.OpenReadBlob(new CryptoHash(Guid.NewGuid().ToByteArray())));

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

        [Fact]
        public void calculate_git_blob_hash()
        {
            Assert.Equal(
                "bd9dbf5aae1a3862dd1526723246b20206e5fc37",
                new CryptoHash(
                    GitBlobWriteStream.ComputeGitBlobHash(
                        new MemoryStream(Encoding.UTF8.GetBytes("what is up, doc?")))).ToString());
        }
    }
}
