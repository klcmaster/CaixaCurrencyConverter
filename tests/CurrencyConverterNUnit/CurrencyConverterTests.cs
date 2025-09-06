using CurrencyConverter;

namespace CurrencyConverterMsTest
{
    // TAREFA
    // Testar Conversão A -> B
    // Testar Conversão A -> B, B -> A (colocar na tabela A->B e B->A)

    public class CurrencyConverterTests
    {
        [Test]
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
            Assert.That(amount, Is.EqualTo( result.Amount));
        }

        [Test]
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
            Assert.That(money1.Amount, Is.EqualTo( result.Amount));
        }
    }
}
