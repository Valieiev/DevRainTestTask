# DevRain Test Task

Test Task: Customer Feedback Analysis Web Application

Overview:
Develop a web application that collects customer feedback, and analyses the sentiment using Azure AI
Services, and displays the results in a user-friendly interface. This application will test back-end and
front-end coding skills, API integration, decision-making, problem-solving abilities, and basic
knowledge of Microsoft Azure and Azure AI Services.

## Requirements:

1. Back-End Development (ASP.NET Core): (Done: isolated Azure Function App)
2. Create a RESTful API using ASP.NET Core. (Done)
3. Implement endpoints for submitting feedback and retrieving analysis results. (Done)
4. Integrate Azure AI Services for sentiment analysis. (Done)
5. Implement secure and efficient data storage. (Done)
6. Front-End Development (Choice of Razor/Blazor or Angular/React): (Done: Blazor Static Web App)
7. Develop a sleek and responsive UI for submitting feedback and displaying results. (Done)
8. Implement forms for feedback submission with validation. (Done)
9. Display sentiment analysis results in a meaningful way (e.g., graphs, charts). (Done: Chart.js)


## API Integration and Use of Postman:
1. Demonstrate the API functionality using Postman collections and document the API endpoints. \
   >[!NOTE]\
   >The collection and environment are located in the PostmanCollection folder.
2. Ensure API can handle various scenarios gracefully (e.g., invalid input, server errors). (Done?)


## Decision Making and Problem-Solving:
1. Provide documentation explaining the choice of technologies and architecture. (NEED)
2. Describe how non-trivial challenges were addressed during development:
 - Since the Static web app and isolated feature app do not exchange user credentials, when a user logs in, the x-ms-client-principal header is added to requests for user information through Static Web Apps edge nodes. Therefore, we must parse the x-ms-client-principal on the function side to retrieve user credentials.
 - Azure AI Services for sentiment analysis rate a message positively, even if it describes a problem but the customer is ready to help solve it.


## Microsoft Azure and Azure AI Service:
1. Utilize Azure services for hosting the application.
 >[!NOTE]\
 > Link: https://yellow-ocean-0f2ed3f03.4.azurestaticapps.net/ \
 > Admin user credentials:\
 > Email: DevRain@valieievandriigmail.onmicrosoft.com\
 > Password: Lazu991528\
 > MFA is disabled, but still gives a warning when logging in. If something goes wrong, please contact me: \
 > www.linkedin.com/in/andrii-valieiev-8aa133151.

2. Integrate Azure AI Services for sentiment analysis of customer feedback. (Done)

## Additional Features (Bonus):
1. Implement authentication and authorization. (Done)
2. Allow for real-time updates in the UI when new feedback is analyzed. (Not implemented, as analyses are performed while new feedback is being processed)


