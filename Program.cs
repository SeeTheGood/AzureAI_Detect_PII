using Azure;
using System;
using Azure.AI.TextAnalytics;

namespace Detect_PII
{
    class Program
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential("INSERT_KEY_HERE");
        private static readonly Uri endpoint = new Uri("INSERT_ENDPOINT_HERE");

        // Detecting sensitive information (PII) from text 
        static void RecognizePII(TextAnalyticsClient client)
        {
            string document = "Call our office at 312-555-1234, or send an email to gerry@contoso.com.";
        
            PiiEntityCollection entities = client.RecognizePiiEntities(document).Value;
        
            Console.WriteLine($"Redacted Text: {entities.RedactedText}");
            if (entities.Count > 0)
            {
                Console.WriteLine($"Recognized {entities.Count} PII entit{(entities.Count > 1 ? "ies" : "y")}:");
                foreach (PiiEntity entity in entities)
                {
                    Console.WriteLine($"Text: {entity.Text}, Category: {entity.Category}, SubCategory: {entity.SubCategory}, Confidence score: {entity.ConfidenceScore}");
                }
            }
            else
            {
                Console.WriteLine("No entities were found.");
            }
        }

        static void Main(string[] args)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            RecognizePII(client);

            Console.Write("Press any key to exit.");
            Console.ReadKey();
        }

    }
}