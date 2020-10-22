using System;
using System.Collections.Generic;
using NUnit.Framework;
using DependencyInjectionContainerLib;

namespace NUnitTestDependencyInjectionContainer
{
    interface ITest1 { }

    public class TestImpl1 : ITest1
    {
        public int a = 1;
    }

    public class TestImpl2 : ITest1
    {
        public int a = 2;
    }

    interface ITest2<T> { }

    public class TestImpl1<T> : ITest2<T>
    {

    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dependencies = new DependenciesConfiguration();
            dependencies.Register(typeof(ITest2<>), typeof(TestImpl1<>));
            var provider = new DependencyProvider(dependencies);
            var obj = provider.Resolve<ITest2<int>>();
        }
    }
}