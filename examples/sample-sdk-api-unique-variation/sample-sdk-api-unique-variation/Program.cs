///////////////////////////////////////////////////////////////////////////////
//
// Spin Rewriter API for C# (NuGet) example of how to generate unique variation.
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

            /*
             * (optional) Set a list of protected terms.
             * You can use one of the following formats:
             * - protected terms are separated by the '\n' (newline) character
             * - protected terms are separated by commas (comma-separated list)
             * - protected terms are stored in a string[] array
             */
            var protected_terms = "John, Douglas Adams, then";
            //var protected_terms = "John\nDouglas\nAdams\nthen";
            //string[] protected_terms =  { "John", "Douglas", "Adams", "then" };

            // (optional) Set whether the One-Click Rewrite process automatically protects Capitalized Words outside the article's title.
            spinrewriter_api.setAutoProtectedTerms(false);

            // (optional) Set the confidence level of the One-Click Rewrite process.
            spinrewriter_api.setConfidenceLevel(ConfidenceLevels.Medium);

            // (optional) Set whether the One-Click Rewrite process uses nested spinning syntax (multi-level spinning) or not.
            spinrewriter_api.setNestedSpintax(true);

            // (optional) Set whether Spin Rewriter rewrites complete sentences on its own.
            spinrewriter_api.setAutoSentences(false);

            // (optional) Set whether Spin Rewriter rewrites entire paragraphs on its own.
            spinrewriter_api.setAutoParagraphs(false);

            // (optional) Set whether Spin Rewriter writes additional paragraphs on its own.
            spinrewriter_api.setAutoNewParagraphs(false);

            // (optional) Set whether Spin Rewriter changes the entire structure of phrases and sentences.
            spinrewriter_api.setAutoSentenceTrees(false);

            // (optional) Sets whether Spin Rewriter should only use synonyms (where available) when generating spun text.
            spinrewriter_api.setUseOnlySynonyms(false);

            // (optional) Sets whether Spin Rewriter should intelligently randomize the order of paragraphs and lists when generating spun text.
            spinrewriter_api.setReorderParagraphs(false);

            // (optional) Sets whether Spin Rewriter should automatically enrich generated articles with headings, bullet points, etc.
            spinrewriter_api.setAddHTMLMarkup(false);

            // (optional) Sets whether Spin Rewriter should automatically convert line-breaks to HTML tags.
            spinrewriter_api.setUseHTMLLinebreaks(false);

            // Make the actual API request and save the response as a native JSON object.
            var text = "John will book a room. Then he will read a book by Douglas Adams.";

            // The return value is a JsonValue array type object or null on error
            var response = spinrewriter_api.getUniqueVariation(text);

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
