# Product Management Application

This project is a simple product management application built using ASP.NET Core. It consists of the following components:

- ProductManagementApp: ASP.NET Core Web API for managing products.
- ProductManagementFrontend: ASP.NET Core MVC application serving as the frontend for interacting with the API.
- ProductManagementApp.Tests: NUnit testing project for testing the backend API.

## ProductManagementApp (ASP.NET Core Web API)

ProductManagementApp is a RESTful API for managing products. It provides endpoints for performing CRUD operations on products.

### Technologies Used:

- ASP.NET Core
- C#
- NUnit (for testing)
- Moq (for mocking dependencies)

### Project Structure:

- **Controllers:** Contains the API controllers responsible for handling HTTP requests.
- **Models:** Contains the data models used within the application.
- **Services:** (Optional) Contains business logic or service classes if needed.
- **InMemoryDatabase.cs:** Provides an in-memory database implementation for storing product data during development/testing.

### Getting Started:

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build the solution.
4. Run the ProductManagementApp project.

### API Endpoints:

- **GET /products**: Retrieves all products.
- **GET /products/{id}**: Retrieves a specific product by ID.
- **POST /products**: Creates a new product.
- **PUT /products/{id}**: Updates an existing product.
- **DELETE /products/{id}**: Deletes a product.

## ProductManagementFrontend (ASP.NET Core MVC)

ProductManagementFrontend is a simple MVC application that interacts with the ProductManagementApp API to provide a user interface for managing products.

### Technologies Used:

- ASP.NET Core MVC
- C#
- MVC Views

### Project Structure:

- **Controllers:** Contains the MVC controllers responsible for rendering views and handling user interactions.
- **Views:** Contains Razor views for rendering HTML content.

### Getting Started:

1. Run the ProductManagementApp API.
2. Input the URL of the backend API, into Startup.cs(As default, it was https://localhost:44363)
2. Run the ProductManagementFrontend project.
3. Access the application through a web browser.
4. 

### Features:

- View a list of products.
- Add new products.
- Edit existing products.
- Delete products.

## ProductManagementApp.Tests (NUnit Testing)

ProductManagementApp.Tests is a testing project for the backend API. It contains NUnit tests to ensure the correctness of the API endpoints and business logic.

### Technologies Used:

- NUnit
- Moq

### Project Structure:

- **UnitTest1.cs:** Contains test cases for the ProductsController.

### Running Tests:

1. Build the solution.
2. Open Test Explorer in Visual Studio.
3. Click "Run All" to execute the tests.

## Contributing

Contributions are welcome! If you have suggestions, bug reports, or feature requests, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
