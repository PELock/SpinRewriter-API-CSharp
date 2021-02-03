///////////////////////////////////////////////////////////////////////////////
//
// Spin Rewriter API for C# (NuGet) example of how to check
// the available quota for your account.
//
// Note: Spin Rewriter API server is using a 120-second timeout.
// Client scripts should use a 150-second timeout to allow for HTTP connection
// overhead.
//
// SDK Version    : v1.0
// Language       : C# (.NET for NuGet)
// Dependencies   : SpinRewriterAPI
// Website        : https://www.spinrewriter.com/
// Contact email  : info@spinrewriter.com
//
// C# SDK Author  : Bartosz Wójcik (https://www.pelock.com)
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using SpinRewriter;

namespace sample_sdk_api_quota
{
    class Program
    {
        static void Main(string[] args)
        {
            // your Spin Rewriter email address goes here
            var email_address = "test@example.com";

            // your unique Spin Rewriter API key goes here
            var api_key = "1ab234c#d5e67fg_h8ijklm?901n234";

            // Authenticate yourself.
            var spinrewriter_api = new SpinRewriterAPI(email_address, api_key);

            // The return value is a JsonValue array type object or null on error
            var response = spinrewriter_api.getQuota();

            if (response != null)
            {
                Console.WriteLine(response.ToString());

                // To access individual response values access them
                // like an array entries (declare its type at the front,
                // because JSON can return different types strings, bools etc.)
                //
                // if (response.ContainsKey("status"))
                // {
                //    string status = response["status"];
                //    Console.WriteLine((string)response["status"]);
                // }
                //
                // etc.
            }
            else
            {
                Console.WriteLine("Spin Rewriter API error");
            }

            Console.WriteLine("\nPress any key to quit...");
            Console.ReadKey();
        }
    }
}
