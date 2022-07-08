using System;

namespace IMJunior
{
    class Program
    {
        static void Main(string[] args)
        {
            var orderForm = new OrderForm();
            var paymentHandler = new PaymentHandler();

            var systemId = orderForm.ShowForm();

            paymentHandler.InitPayment(systemId);
            paymentHandler.ShowPaymentResult(systemId);
        }
    }

    public class OrderForm
    {
        public string ShowForm()
        {
            Console.WriteLine("Мы принимаем: QIWI, WebMoney, Card");

            //симуляция веб интерфейса
            Console.WriteLine("Какое системой вы хотите совершить оплату?");
            return Console.ReadLine();
        }
    }

    public class PaymentHandler
    {
        private Dictionary<string, PaymentSystem> _paymentSystems = new Dictionary<string, PaymentSystem>
        {
            { "QIWI", new QIWIPaymentSystemFactory().Create() },
            { "WebMoney", new WebMoneyPaymentSystemFactory().Create() },
            { "Card", new CardPaymentSystemFactory().Create() }
        };

        public void InitPayment(string systemId)
        {
            _paymentSystems[systemId].InitPayment();
        }

        public void ShowPaymentResult(string systemId)
        {
            Console.WriteLine($"Вы оплатили с помощью {systemId}");

            _paymentSystems[systemId].ShowPaymentResult();

            Console.WriteLine("Оплата прошла успешно!");
        }
    }

    public abstract class PaymentSystem
    {
        public abstract void InitPayment();
        public abstract void ShowPaymentResult();
    }

    public class QIWI : PaymentSystem
    {
        public override void InitPayment()
        {
            Console.WriteLine("Перевод на страницу QIWI...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine("Проверка платежа через QIWI...");
        }
    }

    public class WebMoney : PaymentSystem
    {
        public override void InitPayment()
        {
            Console.WriteLine("Вызов API WebMoney...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine("Проверка платежа через WebMoney...");
        }
    }

    public class Card : PaymentSystem
    {
        public override void InitPayment()
        {
            Console.WriteLine("Вызов API банка эмитера карты Card...");
        }

        public override void ShowPaymentResult()
        {
            Console.WriteLine("Проверка платежа через Card...");
        }
    }

    public abstract class PaymentSystemFactory
    {
        public abstract PaymentSystem Create();
    }

    public class QIWIPaymentSystemFactory : PaymentSystemFactory
    {
        public override PaymentSystem Create() => new QIWI();
    }

    public class WebMoneyPaymentSystemFactory : PaymentSystemFactory
    {
        public override PaymentSystem Create() => new WebMoney();
    }

    public class CardPaymentSystemFactory : PaymentSystemFactory
    {
        public override PaymentSystem Create() => new Card();
    }
}
