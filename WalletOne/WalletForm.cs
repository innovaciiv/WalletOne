using System.Collections.Generic;

namespace GasUtils.Pay.WalletOne
{
    public class WalletForm
    {
        /// <summary>
        /// Идентификатор (номер кошелька) интернет-магазина, полученный при регистрации.
        /// </summary>
        public string WMI_MERCHANT_ID { get; set; }

        /// <summary>
        /// Сумма заказа — число округленное до 2-х знаков после «запятой», в качестве разделителя используется «точка». Наличие 2-х знаков после «запятой» обязательно.
        /// </summary>
        public string WMI_PAYMENT_AMOUNT { get; set; }

        /// <summary>
        /// Идентификатор валюты (ISO 4217):
        /// </summary>
        public string WMI_CURRENCY_ID { get; set; }

        /// <summary>
        /// Идентификатор заказа в системе учета интернет-магазина. Значение данного параметра должно быть уникальным для каждого заказа.
        /// </summary>
        public string WMI_PAYMENT_NO { get; set; }


        /// <summary>
        /// Описание заказа (список товаров и т.п.) — отображается на странице оплаты заказа, а также в истории платежей покупателя. Максимальная длина 255 символов.
        /// </summary>
        public string WMI_DESCRIPTION { get; set; }

        /// <summary>
        /// Адреса (URL) страниц интернет-магазина, на которые будет отправлен покупатель после успешной оплаты.
        /// </summary>
        public string WMI_SUCCESS_URL { get; set; }

        /// <summary>
        /// Адреса (URL) страниц интернет-магазина, на которые будет отправлен покупатель после неуспешной оплаты.
        /// </summary>
        public string WMI_FAIL_URL { get; set; }

        /// <summary>
        /// Срок истечения оплаты. Дата указывается в западно-европейском часовом поясе (UTC+0) и должна быть больше текущей (ISO 8601), например: 2013-10-29T11:39:26.
        /// </summary>
        public string WMI_EXPIRED_DATE { get; set; }

        /// <summary>
        /// С помощью этих параметров можно управлять доступными способами оплаты
        /// </summary>
        public List<string> WMI_PTDISABLED { get; set; }

        /// <summary>
        /// С помощью этих параметров можно управлять доступными способами оплаты
        /// </summary>

        public List<string> WMI_PTENABLED { get; set; }

        /// <summary>
        /// Логин плательщика по умолчанию. Значение данного параметра будет автоматически подставляться в поле логина при авторизации. Возможные форматы: электронная почта, номер телефона в международном формате.
        /// </summary>
        public string WMI_RECIPIENT_LOGIN { get; set; }

        /// <summary>
        /// Имя плательщика. Значения данных параметров будут автоматически подставляться в формы некоторых способов оплаты.
        /// </summary>
        public string WMI_CUSTOMER_FIRSTNAME { get; set; }

        /// <summary>
        /// Фамилия плательщика. Значения данных параметров будут автоматически подставляться в формы некоторых способов оплаты.
        /// </summary>
        public string WMI_CUSTOMER_LASTNAME { get; set; }

        /// <summary>
        /// Email плательщика. Значения данных параметров будут автоматически подставляться в формы некоторых способов оплаты.
        /// </summary>
        public string WMI_CUSTOMER_EMAIL { get; set; }

        /// <summary>
        /// Язык интерфейса определяется автоматически, но можно задать его: ru-RU — русский; en-US — английский.
        /// </summary>
        public string WMI_CULTURE_ID { get; set; }

        /// <summary>
        /// Подпись платежной формы, сформированная с использованием «секретного ключа» интернет-магазина. Необходимость проверки этого параметра устанавливается в настройках интернет-магазина. 
        /// </summary>
        public string WMI_SIGNATURE { get; set; }

        /// <summary>
        /// Позволяет настроить в инвойсе магазина обязательную доставку товара для определенной категории товаров. Обязательно указать значение «1» либо "true".
        /// </summary>
        public bool? WMI_DELIVERY_REQUEST { get; set; }

        /// <summary>
        /// Можно указать страну для доставки по умолчанию, например, Россия.
        /// </summary>
        public string WMI_DELIVERY_COUNTRY { get; set; }

        /// <summary>
        /// Можно указать регион либо область для доставки по умолчанию, например, Москва либо Московская область.
        /// </summary>
        public string WMI_DELIVERY_REGION { get; set; }

        /// <summary>
        /// Можно указать город для доставки по умолчанию, например, Москва.
        /// </summary>
        public string WMI_DELIVERY_CITY { get; set; }

        /// <summary>
        /// Адрес доставки указывается на русском языке.
        /// </summary>
        public string WMI_DELIVERY_ADDRESS { get; set; }

        /// <summary>
        /// Мобильный телефон пользователя, которому необходима доставка товаров.
        /// </summary>
        public string WMI_DELIVERY_CONTACTINFO { get; set; }

        /// <summary>
        /// Комментарий к заказу на доставку можно указать в произвольной форме на русском языке.
        /// </summary>
        public string WMI_DELIVERY_COMMENTS { get; set; }

        /// <summary>
        /// Номер заказа в учетной системе магазина.
        /// </summary>
        public string WMI_DELIVERY_ORDERID { get; set; }

        /// <summary>
        /// Дата и время доставки (от) в формате dd.mm.yyy hh:mm:ss
        /// </summary>
        public string WMI_DELIVERY_DATEFROM { get; set; }

        /// <summary>
        /// Дата и время доставки (до) в формате dd.mm.yyy hh:mm:ss
        /// </summary>
        public string WMI_DELIVERY_DATETILL { get; set; }

        /// <summary>
        /// Идентификатор сабмерчанта (для агрегаторов).
        /// </summary>
        public string WMI_PSP_MERCHANT_ID { get; set; }

        /// <summary>
        /// Все остальные поля платежной формы, не имеющие префикс «WMI_», будут сохранены и переданы в интернет-магазин.
        /// </summary>
        public Dictionary<string, string> MyShopParam { get; set; }

    }
}
