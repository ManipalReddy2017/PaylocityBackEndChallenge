==================================================================================
Please follow below Instructions to run the application with out any issues:
====================================================================================

Step1: Download the complete code to your local drive.
Step2: Run the DB script file "PayrollSystem.sql" available in project folder to create the database 'PayrollSystem" with Tables, Test Data, Stored Procedures to run the application.
Step3 : Update the appsettings.json connnection string to point above "PayrollSystem" DB in below file.
  "ConnectionStrings": {
    "Payroll": "Data Source=localhost\\SQLEXPRESS;Initial Catalog=PayrollSystem;Integrated Security=True;"
  }

Step4: Run the "ApiTests" Project, It's should pass all the test cases if the DB Project is created successfully with test data.

Step4: Run the "AddEmployee(SaveEmployeeDto employeeDto)" service in "EmployeesController" in order to create the Add new Employee with Dependents.

Step5: Run the below Services in 'EnrolledBenefitsController' to view the Enrolled Benefits with various calculated Costs.
    GetEnrolledBenefits(int employeeId)
    GetEnrolledBenefits()

    Here is how it looks with Swagger file:
    ![image](https://github.com/ManipalReddy2017/PaylocityBackEndChallenge/assets/30785302/ad5c76a4-ea45-498f-9456-946a46ed26b4)

