using System.Linq;
using Common.Extensions.Object.DeepEquals.Options;
using Xunit;

namespace Common.Extensions.Object.DeepEquals.UnitTests.Options
{
    public class DeepComparisonOptionsTests
    {
        #region DefaultComparers

        [Fact]
        public void DefaultComparers_RetrievesNewListOfComparers()
        {
            // Arrange
            var listA = DeepComparisonOptions.DefaultComparers;
            var listB = DeepComparisonOptions.DefaultComparers;

            // Act/Assert
            Assert.False(listA.SequenceEqual(listB));
        }

        #endregion
    }
}
