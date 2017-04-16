// This script generates the "IncrementalRunner.g.cs"
//
// To run this script, execute the following in the "C# Interactive" window:
//
// #load "IncrementalRunner.csx"

using System.Linq;
using System.Text;
using System.IO;

using static System.Linq.Enumerable;

var inputCount = 8;
var outputCount = 4;
var code = new StringBuilder();

code.Append(@"namespace Incremental
{
    using System;
    using System.Runtime.CompilerServices;

    partial class IncrementalRunner
    {");

for (var o = 1; o <= outputCount; o++)
{
    for (var i = 1; i <= inputCount; i++)
    {
        var ti = i == 1 ? "I" : string.Join(", ", Range(0, i).Select(n => "I" + n));
        var to = o == 1 ? "O" : string.Join(", ", Range(0, o).Select(n => "O" + n));

        var invokeType = o == 1 ? $"Func<{ti}, {to}>" : $"Func<{ti}, ({to})>";
        var inputArgs = i == 1 ? "Hashed<I> arg0" : string.Join(", ", Range(0, i).Select(n => $"Hashed<I{n}> arg{n}"));
        var returnType = o == 1 ? "Hashed<O>" : "(" + string.Join(", ", Range(0, o).Select(n => $"Hashed<O{n}>")) + ")";
        var invoke = i == 1 ? "(I)args[0]" : string.Join(", ", Range(0, i).Select(n => $"(I{n})args[{n}]"));
        var inputs = string.Join(", ", Range(0, i).Select(n => $"new Hashed<object>(arg{n}.Value, arg{n}.Hash)"));
        var outputTypes = o == 1 ? "typeof(O)" : string.Join(", ", Range(0, o).Select(n => $"typeof(O{n})"));
        var outputs = o == 1 ? "new Hashed<O>((O)outputs[0].Value, outputs[0].Hash)" : "(" + string.Join(", ", Range(0, o).Select(n => $"new Hashed<O{n}>((O{n})outputs[{n}].Value, outputs[{n}].Hash)")) + ")";

        code.Append($@"
        public {returnType} Run<{ti}, {to}>({invokeType} invoke, {inputArgs}, [CallerMemberName] string name = null)
        {{
            var outputs = Run(name,
                args => new object[] {{ invoke({invoke}) }},
                new[] {{ {inputs} }},
                new[] {{ {outputTypes} }});
            return {outputs};
        }}
");
    }
}

code.Append(@"    }
}");

File.WriteAllText("IncrementalRunner.g.cs", code.ToString());