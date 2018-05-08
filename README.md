# Working Time Coding Test

# Framework
I opted to use ASP.NET Core 2 as the framework across the entire solution, with separate Web API and MVC projects. Although some of the original .NET framework technologies are still not available, the number has reduced significantly in Core 2. 

# ORM
I opted to use Entity Framework for mapping, mostly because it was familiar but also because we can assume it is extensively tested, making our application more maintainable.
I did encounter a problem initially, hoping to just scaffold the code using the 'Db-Scaffold', but the lack of primary key on the Employee_Works_Shift table prevented this from working. The entity type configurations needed to be configured manually.

# Data Access
I have used the Repository Pattern as a way of providing data access, to allow for persistence ignorance, making it easier to swap out the underlying data store if ever needed.
By using interfaces, I was able to easily mock up a repository for unit testing purposes.
I have only created an EmployeeRepository for this project as I was able to access an employee's shifts using LINQ. 

# Design Considerations
In a real world application, I imagine the amount of shifts an employee will work can grow to be quite large. Accounting for this, I didn't want to just provide a single endpoint for an employee, returning all of their shifts. 
To handle this, I added a model to store the concept of an employee's work month (or MonthYear). I then added an API endpoint to specifically return these given an Employee ID.

# Website
To keep the UI clean and to stop the page from doing constant postbacks to the controller, I opted to use a bit of jQuery to handle events when the selected employee/month changed, and MVC partial views to reload the content. By using partial views, this meant I could make use of strongly-typed view models.

# With a bit more time...
I haven't used any mocking frameworks before, but I think these may have come in handy for the unit tests I had set up, particularly as I have manually created some mock objects anyway. 

I also haven't focused much on the layout or appearance of the webpage. I haven't added any custom CSS files to the project, but this is something that would need to be done for a real-world application.

I have been a bit lazy in leaving javascript in the html files, mostly due to the small amount used. Best practice would be to move this into separate js files, allowing for the files to be cached and minified, both improving load times to the user, and making the code more maintainable from a developer perspective.

