# Incremental Build

# Goal

1. Enable incremental from the bottom up
2. Extend build pipeline by declaring inputs and outputs

# Design

## Stamp
- A stamp is any data with a crypto hash
- Primitive types are stamp: int, bool, string, enum ...
- A stamp can be serialized and deserlized

## Pure function
- A function takes some input data and produces some output data
- A function can contain multiple child functions, forming a function tree
- A function is deterministic, same inputs always produces exact same outputs
- When inputs are not changed, outputs can be directly read form cache
- A function has a name and a version. When the name and version is idenitical,
  the function should behave identical.
- A *leaf function* is a function that does not use `incremental` to cache results
- A *container function* is a function that uses `incremental` to cache results
  [or build up function pipeline].

## Incremental store
- `.incremental` folder resides in the root directory,
- Root directory is typically git repo root directory.
- `.incremental` stores data and function mapping
- `.incremental` itself is a bare git repo, it can be synced with a remote
  git server
- Data in `.incremental` is store as git blob objects, the name of each data is
  git hash.
- Function mapping is also a git blob object named `index`, it is a fast key
  value store, ideally using `leveldb` or `sqlite` depending on which one is faster.
- Keys and values are binary safe.
- Keys are formatted as `function-name:function-version:input-hash1:input-hash2...`:
- Values are formatted as `output-hash1:output-hash2:...`


- `PathString`: relative to working directory
- `FileStamp`: contains file last-modifed-time, hashed by `file-path:file-content`

```
main()
{
    var pipelines = {
        markdown : {
            markdown_to_markdown_ast,
            markdown_ast_to_markdown_html,
            markdown_html_to_templated_html,
        },
        yaml : { },
        csharp : { },
        swagger : { },
        java : { },
        go : { },
    };

    var config = load_config();
    foreach (var work in config) {
        var files = glob(config[work].src);
        var pipeline = pipelines[work];
        foreach (var file in files) {
            run_incremental(pipeline, files, config[work].dst);
        }
    }
}

markdown_to_markdown_ast(FileStamp md) -> MarkdownSyntaxNode

markdown_to_html(FileStamp md, FileStamp[] files)
             -> (FileStamp html, FileStamp[] references)
{

}

```