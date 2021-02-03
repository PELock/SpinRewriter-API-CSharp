///////////////////////////////////////////////////////////////////////////////
//
// Spin Rewriter API for C# (NuGet)
//
// The only article spinner that truly understands the meaning of your content.
//
// With ENL technology, Spin Rewriter is the perfect tool for SEO specialists
// that need unique, human-quality content to rank higher on Google.
//
// Note: Spin Rewriter API server is using a 120-second timeout.
// Client scripts should use a 150-second timeout to allow for HTTP connection
// overhead.
//
// SDK Version    : v1.0
// Language       : C# (.NET for NuGet)
// Dependencies   : System.Json library from the Mono Project
//                  https://github.com/mono/mono/tree/master/mcs/class/System.Json/System.Json
// Website        : https://www.spinrewriter.com/
// Contact email  : info@spinrewriter.com
//
// C# SDK Author  : Bartosz Wójcik (https://www.pelock.com)
//
///////////////////////////////////////////////////////////////////////////////

using System;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Json;

namespace SpinRewriter
{
    /// <summary>
    /// The confidence level of the One-Click Rewrite process.
    /// </summary>
    public enum ConfidenceLevels
    {
        Low,
        Medium,
        High
    }

    /// <summary>
    /// Main Spin Rewrtier API class. Initialize it first with your email and API key.
    /// </summary>
    public class SpinRewriterAPI
    {
        private const string api_url = "http://www.spinrewriter.com/action/api";
        private NameValueCollection data = new NameValueCollection();

        /// <summary>
        /// Spin Rewriter API constructor, complete with authentication.
        /// </summary>
        /// <param name="email_address"></param>
        /// <param name="api_key"></param>
        public SpinRewriterAPI(string email_address, string api_key)
        {
            data["email_address"] = email_address;
            data["api_key"] = api_key;
        }

        /// <summary>
        /// Convert a boolean value into a lowercase string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Output boolean value in lowercase string format.</returns>
        private string parseBool(bool value)
        {
            return value ? "true" : "false";
        }

        /// <summary>
        /// Returns the API Quota (the number of made and remaining API calls for the 24-hour period).
        /// </summary>
        /// <returns>JSON response</returns>
        public JsonValue getQuota()
        {
            data["action"] = "api_quota";
            return makeRequest();
        }

        /// <summary>
        /// Returns the processed text with the {first option|second option} spinning syntax.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>JSON response</returns>
        public JsonValue getTextWithSpintax(string text)
        {
            data["action"] = "text_with_spintax";
            data["text"] = text;
            return makeRequest();
        }

        /// <summary>
        /// Returns one of the possible unique variations of the processed text.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>JSON response</returns>
        public JsonValue getUniqueVariation(string text)
        {
            data["action"] = "unique_variation";
            data["text"] = text;
            return makeRequest();
        }

        /// <summary>
        /// Returns one of the possible unique variations of given text that already contains valid spintax. No additional processing is done.
        /// </summary>
        /// <param name="text"></param>
        /// <returns>JSON response</returns>
        public JsonValue getUniqueVariationFromSpintax(string text)
        {
            data["action"] = "unique_variation_from_spintax";
            data["text"] = text;
            return makeRequest();
        }

        /// <summary>
        /// Sets the list of protected keywords and key phrases.
        /// </summary>
        /// <param name="protected_terms">Array of words</param>
        /// <returns></returns>
        public bool setProtectedTerms(string[] protected_terms)
        {
            data["protected_terms"] = "";

            // if the array is empty return false
            if (protected_terms.Length == 0) return false;

            // array of words
            foreach (string item in protected_terms)
            {
                string protected_term = item.Trim();

                if (protected_term.Length > 2)
                {
                    data["protected_terms"] += protected_term + "\n";
                }
            }
            data["protected_terms"] = data["protected_terms"].Trim();
            return true;
        }

        /// <summary>
        /// Sets the list of protected keywords and key phrases.
        /// </summary>
        /// <param name="protected_terms">Comma separated list or newline separated list</param>
        /// <returns></returns>
        public bool setProtectedTerms(string protected_terms)
        {
            data["protected_terms"] = "";

            if (protected_terms.Contains(","))
            {
                // comma separated list
                var protected_terms_explode = protected_terms.Split(',');

                foreach (string item in protected_terms_explode)
                {
                    string protected_term = item.Trim();
                    if (protected_term.Length > 2)
                    {
                        data["protected_terms"] += protected_term + "\n";
                    }
                }

                data["protected_terms"] = data["protected_terms"].Trim();
                return true;
            }
            else if (protected_terms.Trim().Contains("\n"))
            {
                // newline separated list (the officially supported format)
                protected_terms = protected_terms.Trim();

                if (protected_terms.Length > 0)
                {
                    data["protected_terms"] = protected_terms;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (protected_terms.Trim().Length > 2 && protected_terms.Trim().Length < 50)
            {
                // a single word or phrase (the officially supported format)
                data["protected_terms"] = protected_terms.Trim();
                return true;
            }

            // invalid format
            return false;
        }

        /// <summary>
        /// Sets whether the One-Click Rewrite process automatically protects Capitalized Words outside the article's title.
        /// </summary>
        /// <param name="auto_protected_terms"></param>
        /// <returns></returns>
        public bool setAutoProtectedTerms(bool auto_protected_terms)
        {
            data["auto_protected_terms"] = parseBool(auto_protected_terms);
            return true;
        }

        /// <summary>
        /// Sets the confidence level of the One-Click Rewrite process.
        /// </summary>
        /// <param name="confidence_level">Either one of ConfidenceLevels.Low, ConfidenceLevels.Medium, ConfidenceLevels.High, </param>
        /// <returns></returns>
        public bool setConfidenceLevel(ConfidenceLevels confidence_level)
        {
            switch (confidence_level)
            {
                case ConfidenceLevels.Low: data["confidence_level"] = "low"; break;
                case ConfidenceLevels.Medium: data["confidence_level"] = "medium"; break;
                case ConfidenceLevels.High: data["confidence_level"] = "high"; break;
            }

            return true;
        }

        /// <summary>
        /// Sets whether the One-Click Rewrite process uses nested spinning syntax (multi-level spinning) or not.
        /// </summary>
        /// <param name="nested_spintax"></param>
        /// <returns></returns>
        public bool setNestedSpintax(bool nested_spintax)
        {
            data["nested_spintax"] = parseBool(nested_spintax);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter rewrites complete sentences on its own.
        /// </summary>
        /// <param name="auto_sentences"></param>
        /// <returns></returns>
        public bool setAutoSentences(bool auto_sentences)
        {
            data["auto_sentences"] = parseBool(auto_sentences);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter rewrites entire paragraphs on its own.
        /// </summary>
        /// <param name="auto_paragraphs"></param>
        /// <returns></returns>
        public bool setAutoParagraphs(bool auto_paragraphs)
        {
            data["auto_paragraphs"] = parseBool(auto_paragraphs);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter writes additional paragraphs on its own.
        /// </summary>
        /// <param name="auto_new_paragraphs"></param>
        /// <returns></returns>
        public bool setAutoNewParagraphs(bool auto_new_paragraphs)
        {
            data["auto_new_paragraphs"] = parseBool(auto_new_paragraphs);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter changes the entire structure of phrases and sentences.
        /// </summary>
        /// <param name="auto_sentence_trees"></param>
        /// <returns></returns>
        public bool setAutoSentenceTrees(bool auto_sentence_trees)
        {
            data["auto_sentence_trees"] = parseBool(auto_sentence_trees);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter should only use synonyms (where available) when generating spun text.
        /// </summary>
        /// <param name="use_only_synonyms"></param>
        /// <returns></returns>
        public bool setUseOnlySynonyms(bool use_only_synonyms)
        {
            data["use_only_synonyms"] = parseBool(use_only_synonyms);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter should intelligently randomize the order of paragraphs and lists when generating spun text.
        /// </summary>
        /// <param name="reorder_paragraphs"></param>
        /// <returns></returns>
        public bool setReorderParagraphs(bool reorder_paragraphs)
        {
            data["reorder_paragraphs"] = parseBool(reorder_paragraphs);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter should automatically enrich generated articles with headings, bullet points, etc.
        /// </summary>
        /// <param name="add_html_markup"></param>
        /// <returns></returns>
        public bool setAddHTMLMarkup(bool add_html_markup)
        {
            data["add_html_markup"] = parseBool(add_html_markup);
            return true;
        }

        /// <summary>
        /// Sets whether Spin Rewriter should automatically convert line-breaks to HTML tags.
        /// </summary>
        /// <param name="use_html_linebreaks"></param>
        /// <returns></returns>
        public bool setUseHTMLLinebreaks(bool use_html_linebreaks)
        {
            data["use_html_linebreaks"] = parseBool(use_html_linebreaks);
            return true;
        }

        /// <summary>
        /// Sets the desired spintax format to be used with the returned spun text.
        /// </summary>
        /// <param name="spintax_format">One of the following '{|}', '{~}', '[|]', '[spin]'</param>
        /// <returns></returns>
        public bool setSpintaxFormat(string spintax_format)
        {
            data["spintax_format"] = spintax_format;
            return true;
        }

        /// <summary>
        /// Sends a request to the Spin Rewriter API and return a Promise with JSON encoded response.
        /// </summary>
        /// <returns>JSON response or null on error.</returns>
        private JsonValue makeRequest()
        {
            try
            {
                // default response
                string json = String.Empty;

                // create a web client to send the POST request
                var client = new WebClient();

                // send the request
                var response = client.UploadValues(api_url, "POST", data);

                // decode the answer from the UTF-8 JSON
                json = Encoding.UTF8.GetString(response);

                if (String.IsNullOrEmpty(json))
                {
                    return null;
                }

                // deserialize JSON response into user friendly result["key"]["value"] format
                var result = JsonValue.Parse(json);

                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
