
# Web Application for Json parsing

 The primary objective of this project is to develop a web application capable of processing JSON-formatted configuration files **with varying levels of complexity and nesting**.\
 The system should read the file, transform the configuration into a programmatic model, and then store it in a relational database (specifically MSSQL).\
 The ultimate goal is to display the stored configuration from the database on the screen in the form of a hierarchical tree.

## Project Components
1. **Relational Database (MSSQL)**
   - Design and implement a relational database schema to store configurations.

2. **Web Application (ASP.NET Core 8 MVC)**
   - Develop a web application that retrieves configurations from the database and displays them in a hierarchical view (tree) on a web page.
## Getting Started
1.  Clone the repository to your local machine.
```bash
      git clone https://github.com/vr242kj/GFL.NET.git
```
3.  Open the solution in Visual Studio or your preferred IDE.
4.  Configure MSSQL Database
      - Set up a MSSQL database and update the connection string in the appsettings.json file.
      - In folder Repositories in file TableName.cs change the name of table you have used.
5.  Run the application.
      - Launch the web application and navigate to the specified URL (default: `https://localhost:5000`).
## Technologies Used
- ASP.NET Core 8 MVC
- MSSQL (Relational Database)
- JSON (Configuration File Format)
## How it Works
1. Upload JSON Configuration
   - Use the web interface to upload a JSON configuration file. I use Postman API. Here is my [Json collection](https://www.postman.com/aerospace-astronomer-15181326/workspace/library-postman/collection/15327265-658037de-857e-4988-b07d-88b38e522410?action=share&creator=15327265&active-environment=15327265-2bba51c3-b3f0-423b-98ea-d90559a57456)\
     **In requests change localhost value**.
2. Database Storage
   - The system reads the file, transforms the configuration into a programmatic model, and stores it in the MSSQL database.
3. Hierarchical View
   - The stored configuration can be viewed on the web page in a hierarchical tree structure.
