// ***********************************************************************
// Copyright (c) 2014 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Execution;

#if NETSTANDARD1_3
using BF = NUnit.Compatibility.BindingFlags;
#else
using BF = System.Reflection.BindingFlags;
#endif

namespace NUnit.TestUtilities
{
    /// <summary>
    /// Fakes provides static methods for creating test dummies of various kinds
    /// </summary>
    public static class Fakes
    {
        #region GetTestMethod

        public static FakeTestMethod GetTestMethod(Type type, string name)
        {
            return new FakeTestMethod(type, name);
        }

        public static FakeTestMethod GetTestMethod(object obj, string name)
        {
            return new FakeTestMethod(obj, name);
        }

        #endregion

        #region GetWorkItem

        public static FakeWorkItem GetWorkItem(Test test, TestExecutionContext context)
        {
            return new FakeWorkItem(test, context);
        }

        public static FakeWorkItem GetWorkItem(Test test)
        {
            return GetWorkItem(test, new TestExecutionContext());
        }

        public static FakeWorkItem GetWorkItem(Type type, string name, TestExecutionContext context)
        {
            return GetWorkItem(GetTestMethod(type, name), context);
        }

        public static FakeWorkItem GetWorkItem(Type type, string name)
        {
            return GetWorkItem(GetTestMethod(type, name));
        }

        public static FakeWorkItem GetWorkItem(object obj, string name, TestExecutionContext context)
        {
            return GetWorkItem(obj.GetType(), name, context);
        }

        public static FakeWorkItem GetWorkItem(object obj, string name)
        {
            return GetWorkItem(obj.GetType(), name);
        }

        #endregion

        #region GetMethodInfo

        public static FakeMethodInfo GetMethodInfo()
        {
            var ret = new FakeMethodInfo();
            ret.TypeInfo = GetTypeInfo();
            (ret.TypeInfo as FakeTypeInfo).FullName = "FakeTypeInfo";
            ret.Name = "FakeMethodInfo";

            return ret;
        }
        #endregion

        #region GetTypeInfo

        public static FakeTypeInfo GetTypeInfo()
        {
            return new FakeTypeInfo();
        }
        #endregion
    }

    #region FakeTestMethod Class

    /// <summary>
    /// FakeTestMethod is used in tests to simulate an actual TestMethod
    /// </summary>
    public class FakeTestMethod : TestMethod
    {
        public FakeTestMethod(object obj, string name)
            : this(obj.GetType(), name) { }

        public FakeTestMethod(Type type, string name)
            : base(new MethodWrapper(type, type.GetMethod(name, BF.Public | BF.NonPublic | BF.Static | BF.Instance))) { }
    }

    #endregion

    #region FakeWorkItem Class

    /// <summary>
    /// FakeWorkItem is used in tests to simulate an actual WorkItem
    /// </summary>
    public class FakeWorkItem : WorkItem
    {
        public event System.EventHandler Executed;

        public FakeWorkItem(Test test, TestExecutionContext context)
            : base(test, TestFilter.Empty)
        {
            InitializeContext(context);
        }

        public override void Execute()
        {
            if (Executed != null)
                Executed(this, System.EventArgs.Empty);
        }

        protected override void PerformWork() { }
    }

    #endregion

    #region FakeMethodInfo Class

    /// <summary>
    /// FakeMethodInfo is used in tests to simulate an implementation if IMethodInfo
    /// </summary>
    public class FakeMethodInfo : IMethodInfo
    {
        public ITypeInfo TypeInfo { get; set; }

        public MethodInfo MethodInfo { get; set; }

        public string Name { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsPublic { get; set; }

        public bool ContainsGenericParameters { get; set; }

        public bool IsGenericMethod { get; set; }

        public bool IsGenericMethodDefinition { get; set; }

        public ITypeInfo ReturnType { get; set; }

        public T[] GetCustomAttributes<T>(bool inherit) where T : class
        {
            throw new NotImplementedException();
        }

        public Type[] GetGenericArguments()
        {
            throw new NotImplementedException();
        }

        public IParameterInfo[] GetParameters()
        {
            throw new NotImplementedException();
        }

        public object Invoke(object fixture, params object[] args)
        {
            throw new NotImplementedException();
        }

        public bool IsDefined<T>(bool inherit)
        {
            throw new NotImplementedException();
        }

        public IMethodInfo MakeGenericMethod(params Type[] typeArguments)
        {
            throw new NotImplementedException();
        }
    }

    #endregion

    #region FakeTypeInfo Class
    public class FakeTypeInfo : ITypeInfo
    {
        public Type Type { get; set; }

        public ITypeInfo BaseType { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        public Assembly Assembly { get; set; }

        public string Namespace { get; set; }

        public bool IsAbstract { get; set; }

        public bool IsGenericType { get; set; }

        public bool ContainsGenericParameters { get; set; }

        public bool IsGenericTypeDefinition { get; set; }

        public bool IsSealed { get; set; }

        public bool IsStaticClass { get; set; }

        public object Construct(object[] args)
        {
            throw new NotImplementedException();
        }

        public ConstructorInfo GetConstructor(Type[] argTypes)
        {
            throw new NotImplementedException();
        }

        public T[] GetCustomAttributes<T>(bool inherit) where T : class
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName()
        {
            throw new NotImplementedException();
        }

        public string GetDisplayName(object[] args)
        {
            throw new NotImplementedException();
        }

        public Type GetGenericTypeDefinition()
        {
            throw new NotImplementedException();
        }

        public IMethodInfo[] GetMethods(BF flags)
        {
            throw new NotImplementedException();
        }

        public bool HasConstructor(Type[] argTypes)
        {
            throw new NotImplementedException();
        }

        public bool HasMethodWithAttribute(Type attrType)
        {
            throw new NotImplementedException();
        }

        public bool IsDefined<T>(bool inherit)
        {
            throw new NotImplementedException();
        }

        public bool IsType(Type type)
        {
            throw new NotImplementedException();
        }

        public ITypeInfo MakeGenericType(Type[] typeArgs)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}
