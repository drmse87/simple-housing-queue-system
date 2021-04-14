# Simple Housing Queue System (bostadskösystem)
I decided to develop a Simple Housing Queue MVC application with [ASP.NET Core](https://asp.net), Entity Framework and Microsoft SQL Server Web (64-bit) 15.0.4003.23.

# Technical details
## Key concepts (entities)
### Rental object
The object that can be rented (either an apartment or a parking spot).
### Property
The property the rental object is located in/at.
### Area
The area the property is located in.
### Listing
Whenever a rental object is free, there is a listing which contains information about the apartment, the day the listing was published and when the application time expires. 
### Application
Users can apply for rental objects (or more specifically the listing). Applications store a reference to the listing and applicant and most importantly, the applicant’s number of waiting days (this will effectively be the waiting list). 
### Contract
The applicant with the
longest waiting time is awarded a contract, with a start and end date. An applicant can only have one active contract.

## Roles
### Applicants
Applicants should be able to:
* Register in the housing queue
* Display available listings
* Apply for rental objects

### Admins
Admins should be able to:
* Display all active contracts
* Display the applicant with the longest waiting time for a listing
* Award contracts

### Things that could have been done, but won't be done
* Admin: Add/Edit/Delete rental objects, areas, properties
* Admin: Terminating contracts
* Admin: Invoice system

## Database schema
* RentalObjects(RentalObjectID [PKEY], PropertyID [FKEY], Rent, RentalObjectType, Floor, FloorPlanUrl, ParkingSpotNumber, Rooms, Size)

* Properties(PropertyID [PKEY], AreaID [FKEY], StreetAddress, Description, PropertyPhotoUrl)

* Areas(AreaID [PKEY], Description, Name)

* Listings(ListingID [PKEY], RentalObjectID [FKEY], PublishDate, LastApplicationDate, MoveInDate, ListingClosureDate)

* Applicants/AspNetUsers(UserID [PKEY], FirstName, LastName, Email, PhoneNumber, RegistrationDate, StreetAddress)

* Applications(ApplicationID [PKEY], UserID [FKEY], ListingID [FKEY], ApplicationDate)

* Contracts(ContractID [PKEY], UserID [FKEY], RentalObjectID [FKEY], StartDate, EndDate, ContractAwardedDate)

### E/R diagram
![E/R Diagram](Docs/er-diagram-housing-queue.png "EF Diagram")

### SQL diagram
![SQL Diagram](Docs/sql-diagram-housing-queue.png "EF Diagram")

### To do list
* ~~Connect to SQL server using ASP.NET Core Secret Manager tool and SqlConnectionStringBuilder class~~
* ~~Add authentication/authorization/Identity~~
* ~~Add custom user data such as First Name, Last Name, Registration date to Identity model (https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0)~~
* ~~Scaffold Identity (Register/Login) according to https://docs.microsoft.com/en-us/aspnet/core/security/authentication/scaffold-identity?view=aspnetcore-5.0&tabs=netcore-cli#scaffold-identity-into-an-mvc-project-with-authorization~~
* ~~Add all entities and migrate them~~

#### Things that could have been done, but won't be done
* Bundling/minification?
* Adding Rent, Url, Address, Description, ApartmentSize types
* CSS (instead of standard bootstrapped design)

### SQL queries
Raw SQL queries will be used instead of LINQ (required by the assignment), either with FromSqlRaw() and ExecuteSqlRaw() methods but more likely with Keyless Entity Types (https://docs.microsoft.com/en-us/ef/core/modeling/keyless-entity-types?tabs=data-annotations):

```
var blogs = context.Blogs
    .FromSqlRaw("SELECT * FROM dbo.Blogs")
    .ToList();
```
#### Admins
##### ✓ How many rental objects are there in each area?
```
SELECT A.Name AS AreaName, COUNT(RentalObjectID) AS NumberOfRentalObjectsInArea
FROM RentalObjects AS RO
INNER JOIN Properties AS P 
	ON RO.PropertyID=P.PropertyID
INNER JOIN Areas AS A
	ON P.AreaID=A.AreaID
GROUP BY A.Name;
```

##### ✓ What rental objects have no active contracts (but could have been put up for listing)?
```
SELECT * FROM RentalObjects
WHERE RentalObjectID NOT IN 
	(SELECT RentalObjectID 
    FROM Contracts 
    WHERE EndDate IS NULL OR EndDate > GETDATE());
```

##### ✓ What rental objects have no active contracts and have not been put up for listing?
```
SELECT * FROM RentalObjects
WHERE RentalObjectID NOT IN 
	(SELECT RentalObjectID 
    FROM Contracts 
    WHERE EndDate IS NULL OR EndDate > GETDATE())
AND RentalObjectID NOT IN 
	(SELECT RentalObjectID 
    FROM Listings 
    WHERE LastApplicationDate > GETDATE() AND ListingClosureDate IS NULL)
```

#### ✓ Who has the longest waiting time for a listing (first order by longest queue time, then who applied first)?
```
SELECT UserId, FirstName, LastName, RegistrationDate, ApplicationDate, DATEDIFF(DAY, RegistrationDate, GETDATE()) AS QueueTime
FROM Applications AS A
INNER JOIN AspNetUsers AS U
	ON A.UserId=U.Id
WHERE ListingID = ''
ORDER BY QueueTime DESC, ApplicationDate ASC OFFSET 0 ROWS
```

#### ✓ Award contract (notice that the EndDate column is blank)
First close the listing:
```
UPDATE Listings
SET ListingClosureDate = currentDateTime
WHERE ListingID = ''
```
Then award the contract:
```
INSERT INTO Contracts (ContractID, UserId, RentalObjectID, 
                    StartDate, ContractAwardedDate) 
            VALUES (contractID, userId, 
                    rentalObjectId, startDate, contractAwardedDate)"
```

#### ✓ Create new listing
```
INSERT INTO Listings (ListingID, RentalObjectID, 
                    PublishDate, LastApplicationDate, MoveInDate)
            VALUES (listingId, rentalObjectId, 
                    publishDate, lastApplicationDate, 
                    moveInDate)"
```

#### ✓ List all active contracts and name of owner
```
SELECT U.FirstName, U.LastName, RO.RentalObjectID, C.StartDate, C.EndDate 
FROM Contracts as C
INNER JOIN AspNetUsers AS U
	ON C.UserId=U.Id
INNER JOIN RentalObjects as RO
	ON C.RentalObjectID=RO.RentalObjectID
WHERE EndDate IS NULL OR EndDate > GETDATE();
```

#### For all applicants (users)
##### ✓ Displaying all listings (still open for application), created as a view...
```
CREATE VIEW View_AllOpenListings AS
    SELECT A.Name, RO.Rooms, RO.Size, RO.Rent, 
    P.StreetAddress, P.PropertyPhotoUrl, L.ListingID, 
    L.PublishDate, L.LastApplicationDate, L.MoveInDate
    FROM Listings AS L
    INNER JOIN RentalObjects as RO
        ON RO.RentalObjectID=L.RentalObjectID
    INNER JOIN Properties AS P 
        ON P.PropertyID=RO.PropertyID
    INNER JOIN Areas AS A
        ON P.AreaID=A.AreaID
    WHERE L.LastApplicationDate > GETDATE() AND L.ListingClosureDate IS NULL
```

##### ✓ Display specific listing (details)
```
SELECT ListingID, L.RentalObjectID, Rent, RentalObjectType, 
        Floor, FloorPlanUrl, Rooms, Size, StreetAddress, 
        P.Description AS PropertyDescription, PropertyPhotoUrl, 
        A.Description AS AreaDescription, Name, PublishDate, 
        LastApplicationDate, MoveInDate 
        FROM Listings AS L
        INNER JOIN RentalObjects as RO
            ON RO.RentalObjectID=L.RentalObjectID
        INNER JOIN Properties AS P 
            ON P.PropertyID=RO.PropertyID
        INNER JOIN Areas AS A
            ON P.AreaID=A.AreaID
            WHERE L.ListingID = ''
```
#### For individual applicants (users)
##### ✓ Make an application
Check if already made application for listing:
```
SELECT * FROM Applications WHERE ListingID = '' AND UserID = ''
```

Check if already has active contract:
```
SELECT * FROM Contracts WHERE UserID = '' AND EndDate IS NULL OR EndDate > GETDATE();
```

Finally make application:
```
INSERT INTO Applications 
    (ApplicationID, UserID, ListingID, ApplicationDate) 
    VALUES ({applicationId}, {userId}, {listingId}, {applicationDate})
```

##### ✓ See all made applications
```
SELECT L.ListingID, RO.RentalObjectID, RO.Rooms, RO.Size, 
RO.Rent, P.StreetAddress, A.ApplicationDate, L.LastApplicationDate, L.MoveInDate
FROM Applications AS A
INNER JOIN Listings as L
    ON A.ListingID = L.ListingID
INNER JOIN RentalObjects as RO
    ON L.RentalObjectID = RO.RentalObjectID
INNER JOIN Properties as P
    ON RO.PropertyID = P.PropertyID
WHERE UserId = ''
```
##### ✓ What is applicant's position in the queue?
```
SELECT PlaceInQueue FROM
	(SELECT UserId, ROW_NUMBER() OVER (ORDER BY QueueTime DESC, ApplicationDate ASC) AS PlaceInQueue
	FROM
		(SELECT UserId, ApplicationDate, DATEDIFF(DAY, RegistrationDate, GETDATE()) AS QueueTime
		FROM Applications AS A
		INNER JOIN AspNetUsers AS U
			ON A.UserId=U.Id
		WHERE ListingID = ''
			) AS B
	) as A
WHERE UserId = ''
```

##### ✓ How many are queuing for a particular listing?
```
SELECT COUNT(UserId) AS ApplicantsCount
FROM Applications
WHERE ListingID = ''
GROUP BY UserId
```

#### Queries that will not be implemented
##### How many (open) listings are there in each area?
```
SELECT A.Name AS AreaName, COUNT(ListingID) AS NumberOfListingsInArea
FROM Listings AS L
INNER JOIN RentalObjects AS RO
	ON L.RentalObjectID=RO.RentalObjectID
INNER JOIN Properties AS P 
	ON RO.PropertyID=P.PropertyID
INNER JOIN Areas AS A
	ON P.AreaID=A.AreaID
WHERE LastApplicationDate > GETDATE() AND ListingClosureDate IS NULL
GROUP BY A.Name;
```

### .NET commandline
#### Migrations
EF Core migrations:

`dotnet ef migrations add InitialCreate`  
`dotnet ef database update`

#### Identity roles
Instead of creating a RoleController or something similar, an "Admin" role was simply manually added like so:

`
var role = new IdentityRole();
            role.Name = "Admin";
            await _roleManager.CreateAsync(role);
`

#### dotnet template
MVC template with Individual User Accounts was used:

`dotnet new mvc -au Individual`
