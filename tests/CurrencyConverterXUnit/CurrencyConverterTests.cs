using CurrencyConverter;

namespace CurrencyConverterMsTest
{
    // TAREFA
    // Testar Conversão A -> B
    // Testar Conversão A -> B, B -> A (colocar na tabela A->B e B->A)

    public class CurrencyConverterTests
    {
        [Fact]
        public void GivenExistingCurrency_WhenConvertingAnotherCurrency_ShouldReturnConvertedCurrencyAmount()
        {
            //Arrange
            var amount = 5.45m;
            var rates = new Dictionary<(Currency From, Currency To), decimal> { { (Currency.USD, Currency.BRL), amount } };
            var rateProvider = new RateProvider(rates);
            var converter = new CurrencyConverter.CurrencyConverter(rateProvider);
            Money money = new Money(1, Currency.USD);

            //Act
            var result = converter.Convert(money, Currency.BRL);

            //Assert
            Assert.Equal(amount, result.Amount);
        }

        [Fact]
        public void GivenCurrency_WhenConvertingAnyCurrencyAndConvertedBackPreviousCurrency_ShouldReturnInitialCurrencyAmount()
        {
            //Arrange
            var rates = new Dictionary<(Currency From, Currency To), decimal> {
                { (Currency.USD, Currency.BRL), 5m },
                { (Currency.BRL, Currency.USD), 0.2m }
            };
            var rateProvider = new RateProvider(rates);
            var converter = new CurrencyConverter.CurrencyConverter(rateProvider);
            Money money1 = new Money(1, Currency.USD);

            //Act
            var money2 = converter.Convert(money1, Currency.BRL);
            var result = converter.Convert(money2, Currency.USD);

            //Assert
            Assert.Equal(money1.Amount, result.Amount);
        }

        [Fact]
        public void GivenUnexistingRate_WhenConvertingCurrency_ShouldThrowException()
        {
            // Arrange
            var rateProvider = new RateProvider(new() { });
            var converter = new CurrencyConverter.CurrencyConverter(rateProvider);
            Money money = new Money(1, Currency.AED);
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => converter.Convert(money, Currency.BAM));
        }
    }
}
