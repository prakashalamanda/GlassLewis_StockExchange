<h1>GlassLewis_StockExchange - API</h1>

<u><h3>Overview:</u></h3>

This project is built using ASP.NET Core Web API designed for managing company records. The API offers the following functionalities:

- Swagger URL - https://localhost:7068/swagger/index.html
- UI URL - https://localhost:7068/pages/index.html
- Implemented Jwt Authentication for authentication purpose
	- Register User - (https://localhost:7068/authorization - POST)
 	- Login User - (https://localhost:7068/authorization/login - POST)
- Retrieve All Companies: Obtain a list of all company records (https://localhost:7068/api/company/getCompanyCollection - GET)
- Retrieve a Company by ID: Fetch a company record using its unique ID (https://localhost:7068/api/company/getCompanyById/{id} - GET)
- Retrieve a Company by ISIN: Fetch a company record using its ISIN (https://localhost:7068/api/company/getCompanyByIsin/{isin} - GET)
- Create a Company: Add a new company record by specifying the Name, Ticker, Exchange, ISIN, and optionally a Website URL. Each company must have a unique ISIN, and the ISIN's first two characters must be non-numeric 
  (https://localhost:7068/api/company/createCompany - POST)
- Update a Company: Modify the details of an existing company (https://localhost:7068/api/company/updateCompany - PUT)
- This API is built using .NET Core 8, utilizing MS SQL for database operations, and an in-memory database for unit testing.
- Unit testing has been written using xUnit framework.

<u><h3>Steps to Run the Project:</u></h3>
- Extract the GlassLewis_StockExchange-main folder. This will have another folder GlassLewis_StockExchange which has the following contents:
 	- GlassLewis_StockExchange.sln: Solution file.
	- GlassLewis_StockExchange folder: Contains the main project files.
	- GlassLewis_StockExchange.Tests folder: Contains the unit testing project.
	- UI folder: Includes a sample HTML page to display the company collection.
	- Screenshots for reference: UI - Dashboard - Working Screenshot (UI Dashboard) and Swagger - Working Screenshot.jpg (Swagger UI).
- Open the GlassLewis_StockExchange.sln solution file.
- Build the application to ensure no compilation errors.
- In Visual Studio, navigate to Tools -> NuGet Package Manager -> Package Manager Console.
- In the Package Manager Console, run add-migration "Init" to generate the migration class for the database. (This step is required only if the migration is not done. Running this script will create a Migrations folder with Init & Snapshot files)
- After the migration step, run update-database to create the GL_StockExchange database 
- Verify that the database was created without errors.
- Run the GlassLewis_StockExchange application. This will open the Swagger UI at https://localhost:7068/swagger/index.html.
- UI dashboard can be accessed via https://localhost:7068/pages/index.html

<u><h3>Access the API from Swagger:</h3></u>
- In Swagger, start by running the Authorization POST endpoint to register a user:
	<code>{
	  "username": "string", // Minimum length: 1
	  "password": "string"  // Minimum length: 8, must include alphanumeric characters, at least one uppercase letter, and a special character.
	}
   </code>
- After registering, log in with the newly created user credentials using the Authorization/login POST endpoint.
  - The login response will include a Bearer token. Copy the token, click the Authorize button in Swagger, and paste the token.
  - The Company API endpoints will now be accessible for 30 minutes using the Bearer token.
- Use the CreateCompany API (api/Company/CreateCompany) to add a new company.
- Use the UpdateCompany API (api/Company/UpdateCompany) to edit an existing company record.
- Use the GetCompanyCollection API to retrieve all company records.
- Use the GetCompanyById API to fetch a company record by its ID.
- Use the GetCompanyByIsin API to fetch a company record by its ISIN.
- This setup should guide you through running and testing the Company API effectively.

![Swagger Image](https://github.com/prakashalamanda/GlassLewis_StockExchange/blob/main/GlassLewis_StockExchange/Swagger%20-%20Working%20Screenshot.jpg)


<u><h3>Access the GetCompanyCollection API from UI:</h3></u>
- Navigate to the url - https://localhost:7068/pages/index.html
- Paste the Bearer token generated from the Swagger page in the text area
- Click on 'Get Company details' button to retrieve the company collection

<b>Note - We can also test this from API tools like Postman etc.,</b> 

Thank you!

