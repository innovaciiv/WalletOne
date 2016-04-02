namespace GasUtils.Pay.WalletOne
{
    public class RequestPayForm
    {
        /// <summary>
        /// Идентификатор (номер кошелька) интернет-магазина.
        /// </summary>
        public string WMI_MERCHANT_ID { get; set; }

        /// <summary>
        /// Сумма заказа
        /// </summary>
        public string WMI_PAYMENT_AMOUNT { get; set; }

        /// <summary>
        /// Сумма удержанной комиссии
        /// </summary>
        public string WMI_COMMISSION_AMOUNT { get; set; }


        /// <summary>
        /// Идентификатор валюты заказа (ISO 4217)
        /// </summary>
        public string WMI_CURRENCY_ID { get; set; }

        /// <summary>
        /// Двенадцатизначный номер кошелька плательщика.
        /// </summary>
        public string WMI_TO_USER_ID { get; set; }

        /// <summary>
        /// Идентификатор заказа в системе учета интернет-магазина.
        /// </summary>
        public string WMI_PAYMENT_NO { get; set; }

        /// <summary>
        /// Идентификатор заказа в системе учета Единой кассы.
        /// </summary>
        public string WMI_ORDER_ID { get; set; }

        /// <summary>
        /// Описание заказа.
        /// </summary>
        public string WMI_DESCRIPTION { get; set; }

        /// <summary>
        /// Адреса (URL) страниц интернет-магазина, на которые будет отправлен покупатель после успешной или неуспешной оплаты.
        /// </summary>
        public string WMI_FAIL_URL { get; set; }

        /// <summary>
        /// Срок истечения оплаты в западно-европейском часовом поясе (UTC+0).
        /// </summary>
        public string WMI_EXPIRED_DATE { get; set; }

        /// <summary>
        /// Дата создания и изменения заказа в западно-европейском часовом поясе (UTC+0).
        /// </summary>
        public string WMI_CREATE_DATE { get; set; }

        /// <summary>
        /// Состояние оплаты заказа: Accepted  — заказ оплачен;
        /// </summary>
        public string WMI_ORDER_STATE { get; set; }

        /// <summary>
        /// Подпись уведомления об оплате, сформированная с использованием «секретного ключа» интернет-магазина.
        /// </summary>
        public string WMI_SIGNATURE { get; set; }
    }
}
