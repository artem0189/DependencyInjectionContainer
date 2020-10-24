using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using DependencyInjectionContainerLib;

namespace NUnitTestDependencyInjectionContainer
{
    public interface ITest1 { }

    public class TestImpl1 : ITest1
    {
        public int a = 1;
    }

    public class TestImpl2 : ITest1
    {
        public int a = 2;
    }

    public interface ITest2<T> { }

    public class TestImpl1<T> : ITest2<T>
    {
        public TestImpl1()
        {

        }

        public TestImpl1(ITest1 test1)
        {

        }

        public TestImpl1(int a, int b)
        {

        }
    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dependencies = new DependenciesConfiguration();
            dependencies.Register<ITest1, TestImpl1>(DependencyLifeTime.InstancePerDependency);
            var provider = new DependencyProvider(dependencies);
            var t = provider.Resolve<ITest1>();
        }
    }
}