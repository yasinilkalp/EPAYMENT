using EPAYMENT.Models;
using EPAYMENT.Providers;
using EPAYMENT.TEST.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using Xunit;

namespace EPAYMENT.TEST
{
    public class AssecoProviderTest
    {

        [Fact]
        public void PaymentProviderFactory_CreateAssecoPaymentProvider()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient();
             
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var paymentProviderFactory = new Factory.PaymentProviderFactory(serviceProvider);
            var provider = paymentProviderFactory.Create(Models.Enums.PosEngineType.ASSECO);

            var paymentGatewayResult = provider.GetPaymentParameters(new PaymentRequest
            {
                BankUrl = "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate",
                CardHolderName = "Enis G�rkan",
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

            string htmlForm = paymentProviderFactory.CreatePaymentForm(paymentGatewayResult.Parameters, paymentGatewayResult.PaymentUrl);

             
            Assert.True(paymentGatewayResult.Success);
        }

    }
}
