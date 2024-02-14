using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using DevRain.Shared;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DevRain.DevRainBlazor.Services
{
    public class FeedbackService
    {
        private HttpClient httpClient;
        
        public FeedbackService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task LeaveFeedback(DevRain.Shared.Feedback feedback)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/leaveFeedback", feedback);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(String.Format("Error occurred, the status code is: { 0 }", 
                response.StatusCode));
                }

            } catch (Exception ex)
            {
                throw new Exception("The server is not responding. Contact administrator.");
            }
        }

        public async Task<List<Feedback>> GetFeedbacks()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("/api/getFeedbacks");

                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        return new List<Feedback>();
                    } else
                    {
                        List<Feedback> feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(jsonResponse);
                        return feedbacks;
                    }    
                }
                else
                {
                    throw new Exception(String.Format("Error occurred, the status code is: { 0 }",
                response.StatusCode));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The server is not responding. Contact administrator." + ex.Message);
            }
        }

        public async Task<List<Feedback>> GetFeedbacks(string options)
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("/api/secured/getFeedbacks/?"+ options);    

                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        return new List<Feedback>();
                    }
                    else
                    {
                        List<Feedback> feedbacks = JsonConvert.DeserializeObject<List<Feedback>>(jsonResponse);
                        return feedbacks;
                    }
                }
                else
                {
                    throw new Exception(String.Format("Error occurred, the status code is: { 0 }",
                response.StatusCode));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The server is not responding. Contact administrator." + ex.Message);
            }
        }

        public async Task<List<FeedbackClientDataModel>> GetTopFeedbacks()
        {
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync("/api/getTopFeedbacks");

                if (response.IsSuccessStatusCode)
                {
                    String jsonResponse = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(jsonResponse))
                    {
                        return new List<FeedbackClientDataModel>();
                    }
                    else
                    {
                        List<FeedbackClientDataModel> feedbacks = JsonConvert.DeserializeObject<List<FeedbackClientDataModel>>(jsonResponse);
                        return feedbacks;
                    }
                }
                else
                {
                    throw new Exception(String.Format("Error occurred, the status code is: { 0 }",
                response.StatusCode));
                }

            }
            catch (Exception ex)
            {
                throw new Exception("The server is not responding. Contact administrator." + ex.Message);
            }
        }
    }
}
