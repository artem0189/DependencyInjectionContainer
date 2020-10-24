using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using DependencyInjectionContainerLib;
using DependencyInjectionContainerLib.Attribute;

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

    public interface ITest2 { }

    public class TestImplTest2 : ITest2
    {
        public ITest1 t;

        public TestImplTest2()
        {

        }

        public TestImplTest2([DependecyKey(2)]ITest1 test1)
        {
            t = test1;
        }

        public TestImplTest2(int a, int b)
        {

        }
    }

    public class Tests
    {
        [Test]
        public void Test1()
        {
            var dependencies = new DependenciesConfiguration();
            dependencies.Register<ITest1, TestImpl1>(DependencyLifeTime.InstancePerDependency, 1);
            dependencies.Register<ITest2, TestImplTest2>(DependencyLifeTime.InstancePerDependency);
            var provider = new DependencyProvider(dependencies);
            var t = provider.Resolve<ITest2>();
        }
    }
}