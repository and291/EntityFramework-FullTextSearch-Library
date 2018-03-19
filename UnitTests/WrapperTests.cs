using fts_lib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class WrapperTests
    {
        [TestMethod]
        public void WrapUnwrapEquals()
        {
            const string prefix = "-TESTPREFIX-";
            const string value = "hello there";

            var wrappedValue = ValueWrapper.Wrap(prefix, value);
            var unwrappedValue = ValueWrapper.Unwrap(prefix, wrappedValue);

            Assert.AreEqual(value, unwrappedValue);
        }

        [TestMethod]
        public void WrapMethodWrongParameterThrowsException()
        {
            const string emptyParam = "";
            const string filledParam = "param value";

            var actions = new List<Action>
            {
                () => ValueWrapper.Wrap(null, null),
                () => ValueWrapper.Wrap(filledParam, null),
                () => ValueWrapper.Wrap(null, filledParam),
                //
                () => ValueWrapper.Wrap(emptyParam, emptyParam),
                () => ValueWrapper.Wrap(filledParam, emptyParam),
                () => ValueWrapper.Wrap(emptyParam, filledParam)
            };

            foreach (var action in actions)
            {
                Assert.ThrowsException<ArgumentException>(action);
            }
        }

        [TestMethod]
        public void UnwrapMethodWrongParameterThrowsException()
        {
            const string emptyParam = "";
            const string filledParam = "param value";

            var actions = new List<Action>
            {
                () => ValueWrapper.Unwrap(null, null),
                () => ValueWrapper.Unwrap(filledParam, null),
                () => ValueWrapper.Unwrap(null, filledParam),
                //
                () => ValueWrapper.Unwrap(emptyParam, emptyParam),
                () => ValueWrapper.Unwrap(filledParam, emptyParam),
                () => ValueWrapper.Unwrap(emptyParam, filledParam)
            };

            foreach (var action in actions)
            {
                Assert.ThrowsException<ArgumentException>(action);
            }
        }
    }
}
