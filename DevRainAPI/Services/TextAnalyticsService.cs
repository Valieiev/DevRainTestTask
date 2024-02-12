using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using System;
using System.Globalization;
using Azure.AI.TextAnalytics;
using DevRainAPI.Models;

namespace DevRainAPI.Services
{

    public static class TextAnalyticsService
    {
        private static readonly AzureKeyCredential credentials = new AzureKeyCredential(Environment.GetEnvironmentVariable("LANGUAGE_KEY", EnvironmentVariableTarget.Process));
        private static readonly Uri endpoint = new Uri(Environment.GetEnvironmentVariable("LANGUAGE_ENDPOINT", EnvironmentVariableTarget.Process));

        public static async Task SentimentAnalysis(Feedback feedback)
        {
            var client = new TextAnalyticsClient(endpoint, credentials);
            DocumentSentiment documentSentiment = await client.AnalyzeSentimentAsync(feedback.Text, options: new AnalyzeSentimentOptions()
            {
                IncludeOpinionMining = true
            });

            feedback.Sentiment = documentSentiment.Sentiment.ToString();
            feedback.PositiveScore = (decimal)documentSentiment.ConfidenceScores.Positive;
            feedback.NeutralScore = (decimal)documentSentiment.ConfidenceScores.Neutral;
            feedback.NegativeScore = (decimal)documentSentiment.ConfidenceScores.Negative;

        }

    }
}
