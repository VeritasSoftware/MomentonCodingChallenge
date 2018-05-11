# Momenton Coding Challenge

The solution has a **micro service architecture**.

It consists of

*   An ASPNET Core 2.0 Web API (micro service) back end
*   An Angular 5 CLI app front end

The Web API has

*   a Company controller
    *   This contains a GET API called hierachy

        | ----- API ----- | ---- Verb ---- | ----- Route ---- | ----- Sample Url ------ |
        | ---------- | -------- | --------- | ----------- |
        | ---- hierarchy ------ | ---- GET ----- | ----- /api/Company/hierarchy ------ | ------- http://localhost:64800/api/Company/hierarchy ----- |

*   an Employee Repository
    *   This computes the company hierachy based on data

The Angular 5 CLI app front end has

*   A component called company-hierarchy
    *   This calls the API using HttpClient
    *   Displays the result    

**Screenshot:**

![Screenshot](https://github.com/VeritasSoftware/MomentonCodingChallenge/blob/master/momenton.web/Screenshot.JPG)