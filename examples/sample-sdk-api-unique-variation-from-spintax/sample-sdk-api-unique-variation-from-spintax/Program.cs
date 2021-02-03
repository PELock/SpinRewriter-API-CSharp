///////////////////////////////////////////////////////////////////////////////
//
// Spin Rewriter API for C# (NuGet) example of how to generate unique varition
// from the provided spintax template.
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

            // (optional) Sets whether Spin Rewriter should only use synonyms (where available) when generating spun text.
            spinrewriter_api.setUseOnlySynonyms(false);

            // (optional) Sets whether Spin Rewriter should intelligently randomize the order of paragraphs and lists when generating spun text.
            spinrewriter_api.setReorderParagraphs(false);

            // (optional) Sets whether Spin Rewriter should automatically enrich generated articles with headings, bullet points, etc.
            spinrewriter_api.setAddHTMLMarkup(false);

            // (optional) Sets whether Spin Rewriter should automatically convert line-breaks to HTML tags.
            spinrewriter_api.setUseHTMLLinebreaks(false);

            /*
             * IMPORTANT:
             *
             * When using the action 'unique_variation_from_spintax', your text
             * should already contain valid {first option|second option} spinning syntax.
             *
             * No additional processing is done on your text, Spin Rewriter simply
             * provides one of the unique variations of the given (already spun) text.
             */
            var text = "John {will|will certainly} {book|make a reservation for} a {room|hotel suite}.";

            // The return value is a JsonValue array type object or null on error
            var response = spinrewriter_api.getUniqueVariationFromSpintax(text);

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
