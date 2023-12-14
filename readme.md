# ArifPay

ArifPay is a C# library for making payments using ArifPay.

## Installation

This package is available on NuGet. You can install it using the following command:

```bash
dotnet add package arifpaynet

Usage

Here’s a basic example of how to use the ArifPay library:

using System;
using Newtonsoft.Json.Linq;
using arifpaynet;

public class Program
{
    public static void Main()
    {
        ArifPay arifPay = new ArifPay("your_api_key_here");

        //  create your request body using this format
        var requestbody = new Dictionary<string, object>()
        {
            {"items", new Dictionary<string, object>[] {new Dictionary<string,object>()
                {
                    {"name", "abel"},
                    {"quantity", 2},
                    {"price", 10},
                    {"description", "some description"},
                    {"image", "https://rb.gy/od65py"},
                }
                }
            },
            {"beneficiaries",new Dictionary<string, object>[] {new Dictionary<string,object>()
                {
                    {"accountNumber", "164910326"},
                    {"bank", "ABYSETAA"},
                    {"amount", 20},
                }
            }
            },
            {"cancelUrl","http://example.com"},
            {"errorUrl","http://example.com"},
            {"notifyUrl","http://example.com"},
            {"successUrl","http://example.com"},
            {"paymentMethods",new string[] { "Telebirr" }},
            {"lang","EN"},
            {"nonce",Guid.NewGuid().ToString()},
            {"expireDate","2025-12-02T03:45:12"}
        };

        JObject jsonObject = JObject.FromObject(requestbody);
        // finally make use of the Make_payment method
        JObject result = arifPay.Make_payment(jsonObject).Result;
        // you can see the result json
        Console.WriteLine(result);
    }
}
