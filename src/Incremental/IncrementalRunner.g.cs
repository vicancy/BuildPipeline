namespace Microsoft.BuildPipeline.Incremental
{
    using System;
    using System.Runtime.CompilerServices;

    partial class IncrementalRunner
    {
        public Hashed<O> Run<I, O>(Func<I, O> invoke, Hashed<I> arg0, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I)args[0]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, O>(Func<I0, I1, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, O>(Func<I0, I1, I2, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, I3, O>(Func<I0, I1, I2, I3, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, I3, I4, O>(Func<I0, I1, I2, I3, I4, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, I3, I4, I5, O>(Func<I0, I1, I2, I3, I4, I5, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, I3, I4, I5, I6, O>(Func<I0, I1, I2, I3, I4, I5, I6, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public Hashed<O> Run<I0, I1, I2, I3, I4, I5, I6, I7, O>(Func<I0, I1, I2, I3, I4, I5, I6, I7, O> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, Hashed<I7> arg7, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6], (I7)args[7]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash), new Hashed<object>(arg7.Value, arg7.Hash) },
                new[] { typeof(O) });
            return new Hashed<O>((O)outputs[0].Value, outputs[0].Hash);
        }

        public (Hashed<O0>, Hashed<O1>) Run<I, O0, O1>(Func<I, (O0, O1)> invoke, Hashed<I> arg0, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I)args[0]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, O0, O1>(Func<I0, I1, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, O0, O1>(Func<I0, I1, I2, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, I3, O0, O1>(Func<I0, I1, I2, I3, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, I3, I4, O0, O1>(Func<I0, I1, I2, I3, I4, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, I3, I4, I5, O0, O1>(Func<I0, I1, I2, I3, I4, I5, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, I3, I4, I5, I6, O0, O1>(Func<I0, I1, I2, I3, I4, I5, I6, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>) Run<I0, I1, I2, I3, I4, I5, I6, I7, O0, O1>(Func<I0, I1, I2, I3, I4, I5, I6, I7, (O0, O1)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, Hashed<I7> arg7, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6], (I7)args[7]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash), new Hashed<object>(arg7.Value, arg7.Hash) },
                new[] { typeof(O0), typeof(O1) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I, O0, O1, O2>(Func<I, (O0, O1, O2)> invoke, Hashed<I> arg0, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I)args[0]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, O0, O1, O2>(Func<I0, I1, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, O0, O1, O2>(Func<I0, I1, I2, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, I3, O0, O1, O2>(Func<I0, I1, I2, I3, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, I3, I4, O0, O1, O2>(Func<I0, I1, I2, I3, I4, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, I3, I4, I5, O0, O1, O2>(Func<I0, I1, I2, I3, I4, I5, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, I3, I4, I5, I6, O0, O1, O2>(Func<I0, I1, I2, I3, I4, I5, I6, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>) Run<I0, I1, I2, I3, I4, I5, I6, I7, O0, O1, O2>(Func<I0, I1, I2, I3, I4, I5, I6, I7, (O0, O1, O2)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, Hashed<I7> arg7, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6], (I7)args[7]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash), new Hashed<object>(arg7.Value, arg7.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I, O0, O1, O2, O3>(Func<I, (O0, O1, O2, O3)> invoke, Hashed<I> arg0, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I)args[0]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, O0, O1, O2, O3>(Func<I0, I1, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, O0, O1, O2, O3>(Func<I0, I1, I2, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, I3, O0, O1, O2, O3>(Func<I0, I1, I2, I3, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, I3, I4, O0, O1, O2, O3>(Func<I0, I1, I2, I3, I4, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, I3, I4, I5, O0, O1, O2, O3>(Func<I0, I1, I2, I3, I4, I5, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, I3, I4, I5, I6, O0, O1, O2, O3>(Func<I0, I1, I2, I3, I4, I5, I6, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }

        public (Hashed<O0>, Hashed<O1>, Hashed<O2>, Hashed<O3>) Run<I0, I1, I2, I3, I4, I5, I6, I7, O0, O1, O2, O3>(Func<I0, I1, I2, I3, I4, I5, I6, I7, (O0, O1, O2, O3)> invoke, Hashed<I0> arg0, Hashed<I1> arg1, Hashed<I2> arg2, Hashed<I3> arg3, Hashed<I4> arg4, Hashed<I5> arg5, Hashed<I6> arg6, Hashed<I7> arg7, [CallerMemberName] string name = null)
        {
            var outputs = Run(name,
                args => new object[] { invoke((I0)args[0], (I1)args[1], (I2)args[2], (I3)args[3], (I4)args[4], (I5)args[5], (I6)args[6], (I7)args[7]) },
                new[] { new Hashed<object>(arg0.Value, arg0.Hash), new Hashed<object>(arg1.Value, arg1.Hash), new Hashed<object>(arg2.Value, arg2.Hash), new Hashed<object>(arg3.Value, arg3.Hash), new Hashed<object>(arg4.Value, arg4.Hash), new Hashed<object>(arg5.Value, arg5.Hash), new Hashed<object>(arg6.Value, arg6.Hash), new Hashed<object>(arg7.Value, arg7.Hash) },
                new[] { typeof(O0), typeof(O1), typeof(O2), typeof(O3) });
            return (new Hashed<O0>((O0)outputs[0].Value, outputs[0].Hash), new Hashed<O1>((O1)outputs[1].Value, outputs[1].Hash), new Hashed<O2>((O2)outputs[2].Value, outputs[2].Hash), new Hashed<O3>((O3)outputs[3].Value, outputs[3].Hash));
        }
    }
}