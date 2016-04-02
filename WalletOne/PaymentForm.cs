using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GasUtils.Pay.WalletOne
{
    /// <summary>
    /// 
    /// </summary>
    public class PaymentForm
    {
        public string PostAddress { get { return "https://wl.walletone.com/checkout/checkout/Index"; } }
        /// <summary>
        /// Секретный ключ интернет-магазина
        /// </summary>
        string merchantKey;
        public PaymentForm(string _merchantKey)
        {
            merchantKey = _merchantKey;
        }
        /// <summary>
        /// Формирование значения параметра WMI_SIGNATURE
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string GetSignature(WalletForm form)
        {
            var formField = GetFiled(form);
            return GetSignature(formField);

        }
        /// <summary>
        /// Возвращает форму оплаты в виде строки
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public string ProcessRequest(WalletForm form)
        {
            var formField = GetFiled(form);
            var signature = GetSignature(formField);

            // Добавление параметра WMI_SIGNATURE в словарь параметров формы
            formField.Add("WMI_SIGNATURE", signature);

            // Формирование платежной формы
            StringBuilder output = new StringBuilder();

            output.AppendLine("<form method=\"POST\" action=\"https://wl.walletone.com/checkout/checkout/Index\">");

            foreach (string key in formField.Keys)
            {
                output.AppendLine(String.Format("{0}: <input name=\"{0}\" value=\"{1}\"/>", key, formField[key]));
            }

            output.AppendLine("<input type=\"submit\"/></form>");

            return output.ToString();
        }
        /// <summary>
        /// Проверяет запрос от сервиса
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        public Answer GetAnswer(NameValueCollection form)
        {
            if (form.Get("WMI_SIGNATURE") == null || form.Get("WMI_PAYMENT_NO") == null || form.Get("WMI_ORDER_STATE") == null)
                return new Answer { Key = false, Value = GetAnswer(false, "Один из параметров не верен") };
            SortedDictionary<string, string> formField = new SortedDictionary<string, string>();
            foreach (var item in form.AllKeys.Where(x=>x!= "WMI_SIGNATURE"))
            {
                GetFormField(ref formField, form.Get(item), item);
            }
            var signature = GetSignature(formField);
            if (signature == form.Get("WMI_SIGNATURE"))
            {
                if (form.Get("WMI_ORDER_STATE").ToUpper() == "ACCEPTED")
                    return new Answer { Key = true, Value = form.Get("WMI_PAYMENT_NO") };
                else
                    return new Answer { Key = false, Value = GetAnswer(false, "Неверное состояние") };
            }
            else
                return new Answer { Key = false, Value = GetAnswer(false, "Неверная подпись") };
        }
        public string GetAnswer(bool result, string description)
        {
            var resultstring = result ? "OK" : "RETRY";
            string output = "";
            output = output + resultstring.ToUpper();
            if (!string.IsNullOrEmpty(description))
                output = output + "&" + description;
            return output;
        }
        public Dictionary<string, object> GetPostData(WalletForm walletForm)
        {
            Dictionary<string, object> postData = new Dictionary<string, object>();
            postData.Add("WMI_CULTURE_ID", walletForm.WMI_CULTURE_ID);
            postData.Add("WMI_CURRENCY_ID", walletForm.WMI_CURRENCY_ID);
            postData.Add("WMI_DELIVERY_ORDERID", walletForm.WMI_DELIVERY_ORDERID);
            postData.Add("WMI_FAIL_URL", walletForm.WMI_FAIL_URL);
            postData.Add("WMI_MERCHANT_ID", walletForm.WMI_MERCHANT_ID);
            postData.Add("WMI_PAYMENT_AMOUNT", walletForm.WMI_PAYMENT_AMOUNT);
            postData.Add("WMI_PAYMENT_NO", walletForm.WMI_PAYMENT_NO);
            postData.Add("WMI_SUCCESS_URL", walletForm.WMI_SUCCESS_URL);
            postData.Add("WMI_SIGNATURE", walletForm.WMI_SIGNATURE);
            if (walletForm.WMI_PTENABLED.Count>0)
            {
                postData.Add("WMI_PTENABLED", walletForm.WMI_PTENABLED.First());
            }
            return postData;
        }
        #region Вспомогательные методы

            /// <summary>
            /// Формирование значения параметра WMI_SIGNATURE
            /// </summary>
            /// <param name="formField"></param>
            /// <returns></returns>
        private string GetSignature(SortedDictionary<string, string> formField)
        {
            StringBuilder signatureData = new StringBuilder();
            foreach (string key in formField.Keys)
            {
                signatureData.Append(formField[key]);
            }

            // Формирование значения параметра WMI_SIGNATURE, путем 
            // вычисления отпечатка, сформированного выше сообщения, 
            // по алгоритму MD5 и представление его в Base64

            string message = signatureData.ToString() + merchantKey;
            Byte[] bytes = Encoding.GetEncoding(1251).GetBytes(message);
            Byte[] hash = new MD5CryptoServiceProvider().ComputeHash(bytes);
            string signature = Convert.ToBase64String(hash);
            return signature;
        }

        /// <summary>
        /// Формирует словарь
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        private SortedDictionary<string, string> GetFiled(WalletForm form)
        {

            /// <summary>
            /// Добавление полей формы в словарь, сортированный по именам ключей.
            /// </summary>
            SortedDictionary<string, string> formField = new SortedDictionary<string, string>();

            GetFormField(ref formField, form.WMI_CULTURE_ID, "WMI_CULTURE_ID");
            GetFormField(ref formField, form.WMI_CURRENCY_ID, "WMI_CURRENCY_ID");
            GetFormField(ref formField, form.WMI_CUSTOMER_EMAIL, "WMI_CUSTOMER_EMAIL");
            GetFormField(ref formField, form.WMI_CUSTOMER_FIRSTNAME, "WMI_CUSTOMER_FIRSTNAME");
            GetFormField(ref formField, form.WMI_CUSTOMER_LASTNAME, "WMI_CUSTOMER_LASTNAME");
            GetFormField(ref formField, form.WMI_DELIVERY_ADDRESS, "WMI_DELIVERY_ADDRESS");
            GetFormField(ref formField, form.WMI_DELIVERY_CITY, "WMI_DELIVERY_CITY");
            GetFormField(ref formField, form.WMI_DELIVERY_COMMENTS, "WMI_DELIVERY_COMMENTS");
            GetFormField(ref formField, form.WMI_DELIVERY_CONTACTINFO, "WMI_DELIVERY_CONTACTINFO");
            GetFormField(ref formField, form.WMI_DELIVERY_COUNTRY, "WMI_DELIVERY_COUNTRY");
            GetFormField(ref formField, form.WMI_DELIVERY_DATEFROM, "WMI_DELIVERY_DATEFROM");
            GetFormField(ref formField, form.WMI_DELIVERY_DATETILL, "WMI_DELIVERY_DATETILL");
            GetFormField(ref formField, form.WMI_DELIVERY_ORDERID, "WMI_DELIVERY_ORDERID");
            GetFormField(ref formField, form.WMI_DELIVERY_REGION, "WMI_DELIVERY_REGION");
            GetFormField(ref formField, form.WMI_DELIVERY_REQUEST, "WMI_DELIVERY_REQUEST");
            GetFormField(ref formField, form.WMI_EXPIRED_DATE, "WMI_EXPIRED_DATE");
            GetFormField(ref formField, form.WMI_FAIL_URL, "WMI_FAIL_URL");
            GetFormField(ref formField, form.WMI_MERCHANT_ID, "WMI_MERCHANT_ID");
            GetFormField(ref formField, form.WMI_PAYMENT_AMOUNT, "WMI_PAYMENT_AMOUNT");
            GetFormField(ref formField, form.WMI_PAYMENT_NO, "WMI_PAYMENT_NO");
            GetFormField(ref formField, form.WMI_PSP_MERCHANT_ID, "WMI_PSP_MERCHANT_ID");
            GetFormField(ref formField, form.WMI_PTDISABLED, "WMI_PTDISABLED");
            GetFormField(ref formField, form.WMI_PTENABLED, "WMI_PTENABLED");
            GetFormField(ref formField, form.WMI_RECIPIENT_LOGIN, "WMI_RECIPIENT_LOGIN");
            GetFormField(ref formField, form.WMI_SUCCESS_URL, "WMI_SUCCESS_URL");
            GetFormField(ref formField, form.MyShopParam); // Дополнительные параметры магазина тоже участвуют при формировании подписи!

            if (!string.IsNullOrEmpty(form.WMI_DESCRIPTION))
                formField.Add("WMI_DESCRIPTION", "BASE64:" + Convert.ToBase64String(Encoding.UTF8.GetBytes(form.WMI_DESCRIPTION)));

            return formField;
        }

        private void GetFormField(ref SortedDictionary<string, string> formField, string Value, string Name)
        {
            if (!String.IsNullOrEmpty(Value))
                formField.Add(Name, Value);
        }

        private void GetFormField(ref SortedDictionary<string, string> formField, CurrencyISO4217 Value, string Name)
        {
            var _value = Enum.GetName(typeof(CurrencyISO4217), Value);
            if (!String.IsNullOrEmpty(_value))
                formField.Add(Name, _value);
        }

        private void GetFormField(ref SortedDictionary<string, string> formField, bool? Value, string Name)
        {
            if (Value != null)
                formField.Add(Name, Value.ToString().ToLower());
        }

        private void GetFormField(ref SortedDictionary<string, string> formField, List<string> Value, string Name)
        {
            if (Value != null && Value.Count() > 0)
                foreach (var item in Value.OrderBy(x => x))
                {
                    if (!String.IsNullOrEmpty(item))
                        formField.Add(Name, item);
                }
        }

        private void GetFormField(ref SortedDictionary<string, string> formField, Dictionary<string, string> NameVAlue)
        {
            if (NameVAlue != null && NameVAlue.Count() > 0)
                foreach (var item in NameVAlue)
                {
                    if (!String.IsNullOrEmpty(item.Value))
                        formField.Add(item.Key, item.Value);
                }
        }

        #endregion
    }
}
