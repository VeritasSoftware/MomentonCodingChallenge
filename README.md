# Momenton Coding Challenge

The solution has a **micro service architecture**.

It consists of

*   An **ASPNET Core 2.0 Web API** back end micro service
*   An **Angular 5 CLI app** front end UI

The Web API has

*   an Entity project
    *   This contains the Entities used in the Repository. These are the data structures.

*   a Repository project
    *   This contains

        *   an Employee Context
            *   This contains a list of Employees. This Context is injected into the Repository.

        *   an Employee Repository
            *   This computes the company hierachy based on data. This Repository is injected into the Controller.

* an ASPNET Core 2.0 Web API project
    *   This contains
        
        *   a Company Controller
            *   This contains a GET API called hierachy.

                | API | Verb | Route | Sample Url |
                | ---------- | -------- | --------- | ----------- |
                | hierarchy | GET | /api/Company/hierarchy | http://localhost:64800/api/Company/hierarchy |

            *   The API calls into the Repository.                

*   an Unit Test project
    *   This contains unit tests for the Repository.

The Angular 5 CLI app front end has

*   a component called company-hierarchy

    *   This calls the API using HttpClient
    *   Displays the result    

**Data structure:**

The hierarchy is built into the data structure.

```cs
    /// <summary>
    /// Class Employee
    /// </summary>
    public class Employee
    {        
        public string EmployeeName { get; set; }

        public uint Id { get; set; }

        public uint? ManagerId { get; set; }        
    }


    /// <summary>
    /// Class EmployeeManager - Recursive
    /// </summary>
    public class EmployeeManager : Employee
    {
        private List<EmployeeManager> _manages = new List<EmployeeManager>();

        public List<EmployeeManager> Manages => _manages;        
    }
```

**The Web API response JSON is like:**

```javascript
{
  "manages": [
    {
      "manages": [
        {
          "manages": [],
          "employeeName": "Martin",
          "id": 220,
          "managerId": 100
        },
        {
          "manages": [],
          "employeeName": "Alex",
          "id": 275,
          "managerId": 100
        }
      ],
      "employeeName": "Alan",
      "id": 100,
      "managerId": 150
    },
    {
      "manages": [
        {
          "manages": [],
          "employeeName": "David",
          "id": 190,
          "managerId": 400
        }
      ],
      "employeeName": "Steve",
      "id": 400,
      "managerId": 150
    }
  ],
  "employeeName": "Jamie",
  "id": 150,
  "managerId": null
}
```

**UI Screenshot:**

![Screenshot](https://github.com/VeritasSoftware/MomentonCodingChallenge/blob/master/momenton.web/Screenshot.JPG)

**Development Environments:**

| Component | Environment |
| ------- | ------ |
| ASPNET Core 2.0 Web API | Visual Studio 2017 |
| Angular 5 CLI app | Visual Studio Code |