<h1>GlassLewis_StockExchange - API</h1>

<u><h3>Overview:</u></h3>

This project is a straightforward ASP.NET Core Web API designed for managing company records. The API offers the following functionalities:

- Retrieve All Companies: Obtain a list of all company records.
- Retrieve a Company by ID: Fetch a company record using its unique ID.
- Retrieve a Company by ISIN: Fetch a company record using its ISIN.
- Create a Company: Add a new company record by specifying the Name, Ticker, Exchange, ISIN, and optionally a Website URL. Each company must have a unique ISIN, and the ISIN's first two characters must be non-numeric.
- Update a Company: Modify the details of an existing company.
- This API is built using .NET Core 8, utilizing MS SQL for database operations, and an in-memory database for unit testing.

<u><h3>Steps to Run the Project:</u></h3>
- Extract the GlassLewis_StockExchange folder. The contents should include:

	- GlassLewis_StockExchange.sln: Solution file.
	- GlassLewis_StockExchange folder: Contains the main project files.
	- GlassLewis_StockExchange.Tests folder: Contains the unit testing project.
	- UI folder: Includes a sample HTML page to display the company collection.
	- README.md file.
	- Screenshots: UI - Dashboard - Working Screenshot (UI Dashboard) and Swagger - Working Screenshot.jpg (Swagger UI).
- Open the GlassLewis_StockExchange.sln solution file.

- Ensure that the solution builds successfully with no compilation errors.

- Navigate to Tools -> NuGet Package Manager -> Package Manager Console.

- In the Package Manager Console, run add-migration "Init" to generate the migration class for the database.

- After the migration step, run update-database to create the GL_StockExchange database.

- Verify that the database was created without errors.

- Run the GlassLewis_StockExchange application. This will open the Swagger UI at https://localhost:7068/swagger/index.html.

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

<u><h3>Access the API from UI Dashboard:</h3></u>

- Navigate to File Explorer to the following path - GlassLewis_StockExchange\UI\CompanyDashboard.html
- Open the Html file
- Paste the Bearer token generated from the Swagger page in the text area
- Click Company details

<b>Note - We can also test this from API tools like Postman etc.,</b>
