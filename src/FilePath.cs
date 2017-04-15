namespace Incremental
{
    using System;
    using System.IO;

    /// <summary>
    /// Crypto hash is calculated based on raw file content and relative file path
    /// </summary>
    public abstract class FilePath : FileContent
    {
        public string RelativePath { get; }

        public FilePath(string relativePath)
            => RelativePath = Path.IsPathRooted(relativePath) 
                ? throw new ArgumentException(nameof(relativePath), "Expect a relative path")
                : relativePath;

        public static FilePath Load(string directory, string relativePath)
            => new PhysicalFilePath(directory, relativePath);
    }

    class PhysicalFilePath : FilePath
    {
        public readonly string FullPath;

        public override Stream OpenRead() => File.OpenRead(FullPath);

        public PhysicalFilePath(string directory, string relativePath) : base(relativePath)
            => FullPath = Path.Combine(directory, relativePath);
    }
}
