﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using heitech.ObjectExpander.Interfaces;
using Moq;
using heitech.ObjectExpander.ExtensionMap;
using heitech.ObjectExpander.Extender;
using System.Threading.Tasks;

namespace heitech.ObjectExpander.Tests.Extender
{
    [TestClass]
    public class ObjectExtenderTests
    {
        readonly object extender = new object();
        private readonly Mock<IAttributeMap> map = new Mock<IAttributeMap>();

        [TestInitialize]
        public void Init()
            => AttributeFactory.SetMap(() => map.Object);

        [TestMethod]
        public void ObjectExtender_RegisterActionDelegatesToAttributeMap()
        {
            bool wasInvoked = false;
            map.Setup(x => x.Add(extender, "key", It.IsAny<IExtensionAttribute>()))
                .Callback(() => wasInvoked = true);

            extender.RegisterAction("key", () => { });

            Assert.IsTrue(wasInvoked);
        }

        [TestMethod]
        public void ObjectExtender_RegisterFuncDelegatesToAttributesMap()
        {
            bool wasInvoked = false;
            map.Setup(x => x.Add(extender, "key", It.IsAny<IExtensionAttribute>()))
                .Callback(() => wasInvoked = true);

            extender.RegisterFunc("key", () => 42);

            Assert.IsTrue(wasInvoked);
        }

        [TestCleanup]
        public void TearDown()
        {
            AttributeFactory.SetMap(null);
            ObjectExtender.TestCleanup();
        }

        [TestMethod]
        public void ObjectExtender_AsyncThrowsNotImplementedException()
        {
            Assert.ThrowsException<NotImplementedException>(() => extender.RegisterAsyncAction("string", () => Task.CompletedTask));
            Assert.ThrowsException<NotImplementedException>(() => extender.RegisterFuncAsync<string, int>("string", () => Task.FromResult(0)));
        }
    }
}
