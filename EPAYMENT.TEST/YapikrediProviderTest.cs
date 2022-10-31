using EPAYMENT.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace EPAYMENT.TEST
{
    public class YapikrediProviderTest
    {

        [Fact]
        public void PaymentProviderFactory_CreateYapikrediPaymentProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient(); 

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var paymentProviderFactory = new Factory.PaymentProviderFactory(serviceProvider);
            var provider = paymentProviderFactory.Create(Models.Enums.PosEngineType.YAPIKREDI);

            var paymentGatewayResult = provider.GetPaymentParameters(new PaymentRequest
            {
                Username = "enis",
                StoreKey = "123123123",
                ClientId = "123",
                Password = "123",
                CardHolderName = "Enis Gürkan",
                CardNumber = "4309-5345-4803-4109",
                ExpireMonth = 12,
                ExpireYear = 21,
                CvvCode = "000",
                Installment = 1,
                TotalAmount = 1,
                CustomerIpAddress = "127.0.0.1",
                CurrencyIsoCode = "949",
                LanguageIsoCode = "tr",
                OrderNumber = Guid.NewGuid().ToString(),
                SuccessUrl = "http://www.google.com",
                FailUrl = "http://www.google.com",
            });

            Assert.True(paymentGatewayResult.Success);
        }
    
    }
}
