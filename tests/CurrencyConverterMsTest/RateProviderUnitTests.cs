using CurrencyConverter;

namespace CurrencyConverterMsTest;

[TestClass]
public sealed class RateProviderUnitTests
{
    // Given[Preconditions]_When[State Under Test]_Then[Expected Behavior]
    // GivenSameCurrency_WhenConverting_ThenReturnsSameValue

    [TestMethod]
    public void GivenExistingRate_WhenGettingRates_ShouldReturnCorrectAmount()
    {
        // Arrange
        var amount = 5.45m;
        var rates = new Dictionary<(Currency From, Currency To), decimal> { { (Currency.USD, Currency.BRL), amount } };
        var sut = new RateProvider(rates);

        // Act
        var result = sut.GetRate(Currency.USD, Currency.BRL);

        // Assert
        Assert.AreEqual(result, amount);
    }

    [TestMethod]
    public void GivenUnexistingRate_WhenGettingRates_ShouldThrowException()
    {
        var sut = new RateProvider(new() { });

        Assert.ThrowsException<InvalidOperationException>(() => sut.GetRate(Currency.AED, Currency.BAM));
    }

}
