using fts_lib;
using fts_lib.Model;
using fts_lib.Predicates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class StorageTests
    {
        [TestMethod]
        public void AllRewritersAreListed()
        {
            var rewriterBaseType = typeof(Rewriter);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => rewriterBaseType.IsAssignableFrom(p) && rewriterBaseType != p);

            foreach (var type in types)
            {
                var found = Storage.Instance.ActiveRewriters.FindAll(x => x.GetType() == type);
                Assert.IsNotNull(found);
                Assert.AreEqual(1, found.Count);
            }
        }

        [TestMethod]
        public void PrefixesAreUnique()
        {
            var rewriters = Storage.Instance.ActiveRewriters;
            var isUnique = rewriters.Distinct().Count() == rewriters.Count;
            Assert.IsTrue(isUnique);
        }

        [TestMethod]
        public void FindCorrectRewriter()
        {
            //TODO подумать над проверкой любого числа реврайтеров
            const string searchQuery = "search query";
            Assert.IsInstanceOfType(
                FindRewriter(new RewriterContains().Wrap(searchQuery)), typeof(RewriterContains));
            Assert.IsInstanceOfType(
                FindRewriter(new RewriterFreetext().Wrap(searchQuery)), typeof(RewriterFreetext));
            Assert.IsNull(FindRewriter(searchQuery));

            Assert.ThrowsException<ArgumentNullException>(() => Storage.Instance.FindRewriter(null));
        }

        public Rewriter FindRewriter(string parameterValue)
        {
            var parameter = new SqlParameter { Value = parameterValue };
            return Storage.Instance.FindRewriter(parameter);
        }
    }
}
