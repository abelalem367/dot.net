using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
namespace arifpaynet;

public class ArifPay
{
    private string API_key;


    public ArifPay(string API_key)
    {
        this.API_key = API_key;
    }

    public async Task<JObject> Make_payment(JObject payment_info)
    {
        List<string> requiredFields = new List<string> { "cancelUrl", "successUrl", "errorUrl", "notifyUrl", "paymentMethods", "items", "beneficiaries" };
        List<string> missingFields = new List<string>();

        foreach (string url in requiredFields)
        {
            if (!payment_info.ContainsKey(url))
            {
                missingFields.Add(url);
            }
        }

        if (missingFields.Count > 0)
        {
            JObject missingFieldsObj = new JObject();

            foreach (string field in missingFields)
            {
                missingFieldsObj.Add(field, field + " is a required field please enter this field");
            }

            return missingFieldsObj;
        }
        else
        {
           // payment_info.Add("nonce", Guid.NewGuid().ToString());
           // payment_info.Add("expireDate", this.expireDate);

            if (missingFields.Count == 0)
            {
                string url = "https://gateway.arifpay.org/api/sandbox/checkout/session";

                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("x-arifpay-key", this.API_key);

                        StringContent content = new StringContent(payment_info.ToString(), Encoding.UTF8, "application/json");

                        HttpResponseMessage response = await client.PostAsync(url, content);
                        if ((int)response.StatusCode != (int)HttpStatusCode.OK)
                        {
                            
                            JObject error = new JObject();
                            error.Add("status", (int)response.StatusCode);
                            error.Add("message", JObject.Parse( await response.Content.ReadAsStringAsync()));
                            return error;
                        }
                        string result = await response.Content.ReadAsStringAsync();
                        return JObject.Parse(result);
                    }
                }
                catch (Exception e)
                {
                   Console.WriteLine(e);
                }
            }
             JObject r = new JObject();
             r.Add("status", "fail");
             r.Add("message", "error");
             return r;
        }
    }
}

