# 📚 Comprehensive Guide to Relational Databases and SQL

## 📋 Table of Contents
- [Introduction to Relational Databases](#introduction-to-relational-databases)
- [Tables: Rows and Columns](#tables-rows-and-columns)
- [SQL Query Structure](#sql-query-structure)
- [Joins](#joins)
- [Normalization Rules](#normalization-rules)
- [Keys and Indexes](#keys-and-indexes)
- [Relationships](#relationships)
- [Stored Procedures](#stored-procedures)
- [Triggers](#triggers)
- [Transactions](#transactions)
- [Real-World Application: E-commerce Database](#real-world-application-e-commerce-database)

## 🔍 Introduction to Relational Databases

Relational databases organize data into structured tables with predefined relationships between them. They are based on the relational model proposed by E.F. Codd in 1970 and have become the backbone of most modern data management systems.

**Key characteristics:**
- Data is stored in tables (relations)
- Tables are linked through defined relationships
- SQL (Structured Query Language) is used to interact with the data
- ACID properties ensure data integrity (Atomicity, Consistency, Isolation, Durability)

Popular relational database management systems (RDBMS) include:
- MySQL
- PostgreSQL
- SQL Server
- Oracle
- SQLite

## 📊 Tables: Rows and Columns

Tables are the fundamental building blocks of relational databases.

### Structure

- **Table**: A collection of related data organized in rows and columns
- **Column (Field)**: A vertical component that stores a specific type of data
- **Row (Record or Tuple)**: A horizontal component that represents a single, complete data entity
- **Cell**: The intersection of a row and column, containing a specific value

### Example

Consider a `Customers` table:

| CustomerID | FirstName | LastName | Email                | City      |
|------------|-----------|----------|----------------------|-----------|
| 1          | John      | Smith    | john.smith@email.com | New York  |
| 2          | Emma      | Johnson  | emma.j@email.com     | Chicago   |
| 3          | Michael   | Williams | mw2023@email.com     | Los Angeles |

In this table:
- Each **column** (CustomerID, FirstName, LastName, Email, City) holds a specific type of data
- Each **row** represents a single customer
- Each **cell** contains a specific value for a customer attribute

## 📝 SQL Query Structure

SQL (Structured Query Language) is the standard language for interacting with relational databases.

### Main Components of an SQL Query

#### 1. SELECT
Specifies the columns to retrieve from the database.

```sql
SELECT column1, column2, ...
SELECT * -- Retrieves all columns
```

#### 2. FROM
Specifies the table(s) to query.

```sql
FROM table_name
```

#### 3. WHERE
Filters records based on a condition.

```sql
WHERE condition
```

Example:
```sql
SELECT FirstName, LastName
FROM Customers
WHERE City = 'Chicago';
```

#### 4. GROUP BY
Groups rows that have the same values into summary rows.

```sql
GROUP BY column1, column2, ...
```

Example:
```sql
SELECT City, COUNT(*) AS CustomerCount
FROM Customers
GROUP BY City;
```

#### 5. HAVING
Filters groups based on a specified condition (acts like WHERE but operates on grouped data).

```sql
HAVING condition
```

Example:
```sql
SELECT City, COUNT(*) AS CustomerCount
FROM Customers
GROUP BY City
HAVING COUNT(*) > 5;
```

#### 6. ORDER BY
Sorts the result set in ascending or descending order.

```sql
ORDER BY column1 [ASC|DESC], column2 [ASC|DESC], ...
```

Example:
```sql
SELECT * FROM Customers
ORDER BY LastName ASC, FirstName DESC;
```

### Complete Query Example

```sql
SELECT 
    City, 
    COUNT(*) AS CustomerCount,
    AVG(Orders.OrderTotal) AS AvgOrderValue
FROM 
    Customers
JOIN 
    Orders ON Customers.CustomerID = Orders.CustomerID
WHERE 
    Customers.Status = 'Active'
GROUP BY 
    City
HAVING 
    COUNT(*) > 3
ORDER BY 
    CustomerCount DESC;
```

This query:
1. Selects cities and calculates customer count and average order value
2. From the Customers table joined with Orders
3. Where customers are active
4. Groups results by city
5. Only includes cities with more than 3 customers
6. Orders results by customer count in descending order

## 🔄 Joins

Joins combine rows from two or more tables based on a related column.

### Types of Joins

### Types of Joins with Examples

Let's use two simple tables to illustrate the different types of joins:

**Customers Table:**

| CustomerID | Name     | City      |
|------------|----------|-----------|
| 1          | John     | New York  |
| 2          | Emma     | Chicago   |
| 3          | Michael  | Boston    |
| 4          | Sarah    | Dallas    |

**Orders Table:**

| OrderID | CustomerID | Product   | Amount |
|---------|------------|-----------|--------|
| 101     | 1          | Laptop    | 1200   |
| 102     | 3          | Phone     | 800    |
| 103     | 5          | Tablet    | 500    |
| 104     | 1          | Keyboard  | 100    |

Notice that:
- CustomerID 2 (Emma) and 4 (Sarah) don't have any orders
- OrderID 103 references CustomerID 5, which doesn't exist in the Customers table

#### 1. INNER JOIN
Returns only the rows where there is a match in both tables.

```sql
SELECT c.CustomerID, c.Name, o.OrderID, o.Product
FROM Customers c
INNER JOIN Orders o ON c.CustomerID = o.CustomerID;
```

**Result:**

| CustomerID | Name     | OrderID | Product   |
|------------|----------|---------|-----------|
| 1          | John     | 101     | Laptop    |
| 1          | John     | 104     | Keyboard  |
| 3          | Michael  | 102     | Phone     |

Note: Only customers with orders appear, and only orders with matching customers appear.

#### 2. LEFT JOIN (LEFT OUTER JOIN)
Returns all records from the left table (Customers) and the matched records from the right table (Orders).

```sql
SELECT c.CustomerID, c.Name, o.OrderID, o.Product
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID;
```

**Result:**

| CustomerID | Name     | OrderID | Product   |
|------------|----------|---------|-----------|
| 1          | John     | 101     | Laptop    |
| 1          | John     | 104     | Keyboard  |
| 2          | Emma     | NULL    | NULL      |
| 3          | Michael  | 102     | Phone     |
| 4          | Sarah    | NULL    | NULL      |

Note: All customers appear, even those without orders (Emma and Sarah).

#### 3. RIGHT JOIN (RIGHT OUTER JOIN)
Returns all records from the right table (Orders) and the matched records from the left table (Customers).

```sql
SELECT c.CustomerID, c.Name, o.OrderID, o.Product
FROM Customers c
RIGHT JOIN Orders o ON c.CustomerID = o.CustomerID;
```

**Result:**

| CustomerID | Name     | OrderID | Product   |
|------------|----------|---------|-----------|
| 1          | John     | 101     | Laptop    |
| 3          | Michael  | 102     | Phone     |
| NULL       | NULL     | 103     | Tablet    |
| 1          | John     | 104     | Keyboard  |

Note: All orders appear, even those with no matching customer (OrderID 103).

#### 4. FULL JOIN (FULL OUTER JOIN)
Returns all records when there is a match in either the left or right table.

```sql
SELECT c.CustomerID, c.Name, o.OrderID, o.Product
FROM Customers c
FULL JOIN Orders o ON c.CustomerID = o.CustomerID;
```

**Result:**

| CustomerID | Name     | OrderID | Product   |
|------------|----------|---------|-----------|
| 1          | John     | 101     | Laptop    |
| 1          | John     | 104     | Keyboard  |
| 2          | Emma     | NULL    | NULL      |
| 3          | Michael  | 102     | Phone     |
| 4          | Sarah    | NULL    | NULL      |
| NULL       | NULL     | 103     | Tablet    |

Note: All customers and all orders appear, with NULL values where there's no match.

#### 5. CROSS JOIN
Returns the Cartesian product of the tables (all possible combinations).

```sql
SELECT c.CustomerID, c.Name, o.OrderID, o.Product
FROM Customers c
CROSS JOIN Orders o;
```

**Result:** (showing first 8 rows of 16 total)

| CustomerID | Name     | OrderID | Product   |
|------------|----------|---------|-----------|
| 1          | John     | 101     | Laptop    |
| 1          | John     | 102     | Phone     |
| 1          | John     | 103     | Tablet    |
| 1          | John     | 104     | Keyboard  |
| 2          | Emma     | 101     | Laptop    |
| 2          | Emma     | 102     | Phone     |
| 2          | Emma     | 103     | Tablet    |
| 2          | Emma     | 104     | Keyboard  |
| ...        | ...      | ...     | ...       |

Note: Every customer is paired with every order regardless of any relationship.

#### 6. SELF JOIN
Joins a table to itself as if it were two tables. For example, finding employees and their managers:

**Employees Table:**

| EmployeeID | Name     | ManagerID |
|------------|----------|-----------|
| 1          | Alice    | 3         |
| 2          | Bob      | 3         |
| 3          | Charlie  | 4         |
| 4          | David    | NULL      |
| 5          | Eve      | 1         |

```sql
SELECT e.Name AS Employee, m.Name AS Manager
FROM Employees e
LEFT JOIN Employees m ON e.ManagerID = m.EmployeeID;
```

**Result:**

| Employee   | Manager   |
|------------|-----------|
| Alice      | Charlie   |
| Bob        | Charlie   |
| Charlie    | David     |
| David      | NULL      |
| Eve        | Alice     |

Note: The Employees table is joined to itself, allowing us to see each employee with their manager's name.

### Real-world Example

Imagine we want to find all customers and their orders with specific details:

```sql
SELECT 
    c.CustomerID,
    c.FirstName,
    c.LastName,
    o.OrderID,
    o.OrderDate,
    o.TotalAmount
FROM 
    Customers c
LEFT JOIN 
    Orders o ON c.CustomerID = o.CustomerID
ORDER BY 
    c.LastName, o.OrderDate DESC;
```

This will show all customers (even those who haven't placed orders) with their order details if available.

## 📐 Normalization Rules

Database normalization is a systematic approach to organizing data in a relational database. It's designed to reduce data redundancy, improve data integrity, and make the database more efficient. Let's explore each normalization form in detail with examples.

### Understanding Normalization: The Why and How

Before diving into the specific normal forms, it's important to understand why normalization matters:

- **Eliminates redundancy**: Reduces duplicate data, saving storage space
- **Prevents anomalies**: Avoids insertion, update, and deletion problems
- **Improves query performance**: Makes certain types of queries more efficient
- **Enhances data integrity**: Ensures data is consistent and accurate

The normalization process involves breaking down large tables into smaller, more focused tables and defining relationships between them. This creates a more structured and logical database design.

### First Normal Form (1NF)

The first step in normalization focuses on atomicity - ensuring each piece of data is as small and indivisible as possible.

**Requirements for 1NF:**
1. Each table cell must contain only a single value (atomic values)
2. Each column must contain the same type of information
3. Each row must be unique (typically ensured with a primary key)
4. The order of rows and columns doesn't matter

**Real-world Example: Customer Orders**

Consider an order system where we store multiple products in a single field:

**Before 1NF - Orders Table (Non-normalized):**

| OrderID | Customer  | Products                                | OrderDate  |
|---------|-----------|----------------------------------------|------------|
| 1       | John Doe  | Laptop, Mouse, Keyboard                | 2023-01-15 |
| 2       | Jane Smith| Monitor, Webcam                        | 2023-01-16 |
| 3       | John Doe  | Headphones, USB Drive, External Drive  | 2023-01-20 |

**Problems with this design:**
- The "Products" column contains multiple values
- We can't easily search for a specific product
- Adding or removing a single product from an order is complex
- We can't store product-specific details like price or quantity

**After 1NF - Orders Table:**

| OrderID | Customer  | OrderDate  |
|---------|-----------|------------|
| 1       | John Doe  | 2023-01-15 |
| 2       | Jane Smith| 2023-01-16 |
| 3       | John Doe  | 2023-01-20 |

**After 1NF - OrderItems Table:**

| OrderID | Product       |
|---------|---------------|
| 1       | Laptop        |
| 1       | Mouse         |
| 1       | Keyboard      |
| 2       | Monitor       |
| 2       | Webcam        |
| 3       | Headphones    |
| 3       | USB Drive     |
| 3       | External Drive|

Now we have:
- Each cell contains only one value
- Each OrderID-Product combination is unique
- We can easily add or remove products from orders
- We can search for specific products

### Second Normal Form (2NF)

2NF builds upon 1NF by removing partial dependencies - attributes that depend on only part of the primary key.

**Requirements for 2NF:**
1. The table must be in 1NF
2. All non-key attributes must depend on the entire primary key, not just part of it

2NF is only relevant when you have a composite primary key (a key consisting of two or more columns). If your table has a single-column primary key, it's automatically in 2NF if it's already in 1NF.

**Real-world Example: Order Items with Product Details**

Let's expand our previous example:

**1NF Table with Issues (not in 2NF):**

| OrderID | Product  | ProductPrice | ProductCategory | Customer    | OrderDate  |
|---------|----------|--------------|----------------|------------|------------|
| 1       | Laptop   | 1200.00      | Electronics    | John Doe   | 2023-01-15 |
| 1       | Mouse    | 25.00        | Accessories    | John Doe   | 2023-01-15 |
| 2       | Monitor  | 350.00       | Electronics    | Jane Smith | 2023-01-16 |

In this table, the primary key is the combination of (OrderID, Product). However:
- ProductPrice and ProductCategory depend only on the Product (not the full key)
- Customer and OrderDate depend only on the OrderID (not the full key)

These are partial dependencies and violate 2NF.

**After 2NF - Orders Table:**

| OrderID | Customer   | OrderDate  |
|---------|------------|------------|
| 1       | John Doe   | 2023-01-15 |
| 2       | Jane Smith | 2023-01-16 |

**After 2NF - Products Table:**

| Product  | ProductPrice | ProductCategory |
|----------|--------------|-----------------|
| Laptop   | 1200.00      | Electronics     |
| Mouse    | 25.00        | Accessories     |
| Monitor  | 350.00       | Electronics     |

**After 2NF - OrderItems Table:**

| OrderID | Product  |
|---------|----------|
| 1       | Laptop   |
| 1       | Mouse    |
| 2       | Monitor  |

Now we have:
- Customer and OrderDate depend on the entire primary key of the Orders table
- ProductPrice and ProductCategory depend on the entire primary key of the Products table
- OrderItems links the two tables with a composite key (OrderID, Product)

This design eliminates redundancy and makes maintenance easier.

### Third Normal Form (3NF)

3NF addresses transitive dependencies - when a non-key attribute depends on another non-key attribute.

**Requirements for 3NF:**
1. The table must be in 2NF
2. No non-key attribute should depend on another non-key attribute (all attributes must depend directly on the primary key)

**Real-world Example: Customer Orders with Location**

Consider this 2NF table:

**2NF Table with Issues (not in 3NF):**

| OrderID | Customer    | CustomerZipCode | City         | State      | OrderDate  |
|---------|-------------|-----------------|--------------|------------|------------|
| 1       | John Doe    | 10001           | New York     | NY         | 2023-01-15 |
| 2       | Jane Smith  | 10001           | New York     | NY         | 2023-01-16 |
| 3       | Bob Johnson | 60601           | Chicago      | IL         | 2023-01-20 |
| 4       | Alice Brown | 60601           | Chicago      | IL         | 2023-01-25 |

In this table:
- The primary key is OrderID
- CustomerZipCode depends on Customer (which depends on OrderID)
- City and State depend on CustomerZipCode (not directly on OrderID)

This creates a transitive dependency: OrderID → Customer → CustomerZipCode → City, State

**After 3NF - Orders Table:**

| OrderID | Customer    | CustomerZipCode | OrderDate  |
|---------|-------------|-----------------|------------|
| 1       | John Doe    | 10001           | 2023-01-15 |
| 2       | Jane Smith  | 10001           | 2023-01-16 |
| 3       | Bob Johnson | 60601           | 2023-01-20 |
| 4       | Alice Brown | 60601           | 2023-01-25 |

**After 3NF - ZipCodes Table:**

| ZipCode | City         | State |
|---------|--------------|-------|
| 10001   | New York     | NY    |
| 60601   | Chicago      | IL    |

Now:
- City and State depend directly on ZipCode (the primary key of the ZipCodes table)
- All attributes in the Orders table depend directly on the OrderID
- We've eliminated the redundancy of storing city and state multiple times

This helps maintain data integrity - if we need to update a city name, we update it in just one place.

### Boyce-Codd Normal Form (BCNF)

BCNF is a stronger version of 3NF that addresses certain anomalies not resolved by 3NF.

**Requirements for BCNF:**
1. The table must be in 3NF
2. For every dependency X → Y, X must be a superkey (a superkey is any set of columns that can uniquely identify a row)

BCNF becomes relevant when there are multiple candidate keys (columns or combinations of columns that could serve as the primary key).

**Real-world Example: Student Course Registration**

Consider a system tracking which students are taught by which professors for different courses:

**3NF Table with Issues (not in BCNF):**

| StudentID | Course    | Professor   |
|-----------|-----------|-------------|
| S1        | Database  | Dr. Smith   |
| S2        | Database  | Dr. Smith   |
| S3        | Networks  | Dr. Jones   |
| S4        | Database  | Dr. Smith   |
| S5        | AI        | Dr. Chen    |

Functional dependencies:
- (StudentID, Course) → Professor (a student takes a course with only one professor)
- (Professor, Course) → StudentID (a professor teaches a course to many students)

Here, (StudentID, Course) is the primary key, but we also have (Professor, Course) as a candidate key, and this creates a problem because Professor depends on Course but Course is not a superkey.

**After BCNF - CourseProf Table:**

| Course    | Professor   |
|-----------|-------------|
| Database  | Dr. Smith   |
| Networks  | Dr. Jones   |
| AI        | Dr. Chen    |

**After BCNF - StudentCourse Table:**

| StudentID | Course    |
|-----------|-----------|
| S1        | Database  |
| S2        | Database  |
| S3        | Networks  |
| S4        | Database  |
| S5        | AI        |

Now:
- Each course is taught by exactly one professor
- If a professor changes for a course, we update only one record
- We maintain the many-to-many relationship between students and courses

BCNF helps prevent update anomalies when there are multiple candidate keys.

### Fourth Normal Form (4NF)

4NF addresses multi-valued dependencies, which occur when one attribute has multiple independent values for another attribute.

**Requirements for 4NF:**
1. The table must be in BCNF
2. It should not have any multi-valued dependencies

**Real-world Example: Student Skills and Languages**

Consider a table tracking student skills and languages:

**BCNF Table with Issues (not in 4NF):**

| StudentID | Skill           | Language   |
|-----------|-----------------|------------|
| S1        | Programming     | English    |
| S1        | Programming     | Spanish    |
| S1        | Database Design | English    |
| S1        | Database Design | Spanish    |
| S2        | Web Development | French     |
| S2        | Web Development | German     |

Here, the student's skills and languages are independent of each other - the fact that a student knows programming doesn't relate to whether they know English. This is a multi-valued dependency.

**After 4NF - StudentSkills Table:**

| StudentID | Skill           |
|-----------|-----------------|
| S1        | Programming     |
| S1        | Database Design |
| S2        | Web Development |

**After 4NF - StudentLanguages Table:**

| StudentID | Language   |
|-----------|------------|
| S1        | English    |
| S1        | Spanish    |
| S2        | French     |
| S2        | German     |

Now:
- Skills and languages are stored separately
- We avoid redundancy and potential inconsistencies
- Adding a new skill doesn't require repeating all languages

### Fifth Normal Form (5NF) or Project-Join Normal Form (PJNF)

5NF deals with join dependencies that can't be decomposed further.

**Requirements for 5NF:**
1. The table must be in 4NF
2. It cannot be decomposed into smaller tables without losing information when reconstructed via joins

**Real-world Example: Suppliers, Parts, and Projects**

Consider tracking which suppliers provide which parts for which projects:

**4NF Table with Issues (not in 5NF):**

| Supplier | Part  | Project |
|----------|-------|---------|
| S1       | P1    | J1      |
| S1       | P2    | J1      |
| S2       | P1    | J2      |
| S2       | P3    | J1      |

This table represents a three-way relationship. If we know that:
- Supplier S1 can supply part P1
- Supplier S1 works on project J1
- Project J1 uses part P1

We assume S1 supplies P1 for J1, but this might not always be true.

**After 5NF - We decompose into three tables:**

**SupplierParts Table:**

| Supplier | Part  |
|----------|-------|
| S1       | P1    |
| S1       | P2    |
| S2       | P1    |
| S2       | P3    |

**SupplierProjects Table:**

| Supplier | Project |
|----------|---------|
| S1       | J1      |
| S2       | J1      |
| S2       | J2      |

**PartProjects Table:**

| Part  | Project |
|-------|---------|
| P1    | J1      |
| P1    | J2      |
| P2    | J1      |
| P3    | J1      |

This decomposition allows us to capture all valid combinations without redundancy. We can then join these tables to reconstruct the original data when needed.

### Denormalization: When to Break the Rules

While normalization is generally beneficial, there are times when deliberately breaking these rules (denormalization) can improve performance:

1. **Read-heavy applications**: If your database is primarily used for reporting or analysis with few updates, denormalization can reduce the number of joins needed
2. **Performance bottlenecks**: For frequently accessed data, having some redundancy might be faster than complex joins
3. **Calculated fields**: Pre-calculating and storing values that are frequently queried can improve performance

**Example of Denormalization:**

Consider an e-commerce reporting system tracking daily sales totals:

**Normalized Approach:**
```sql
SELECT 
    DATE(order_date) AS sale_date,
    SUM(quantity * price) AS daily_revenue
FROM 
    orders o
JOIN 
    order_items oi ON o.order_id = oi.order_id
WHERE 
    order_date BETWEEN '2023-01-01' AND '2023-01-31'
GROUP BY 
    DATE(order_date);
```

**Denormalized Approach:**
Create a daily_sales table with pre-calculated totals:

```sql
SELECT 
    sale_date,
    daily_revenue
FROM 
    daily_sales
WHERE 
    sale_date BETWEEN '2023-01-01' AND '2023-01-31';
```

The trade-off is between query performance (denormalized) and data integrity/storage efficiency (normalized). Modern databases are often designed with a balanced approach, normalizing most data but denormalizing specific tables for performance-critical operations.

### Normalization Best Practices

1. **Start with 3NF**: Aim for at least Third Normal Form for most databases
2. **Consider BCNF**: Move to BCNF when dealing with multiple candidate keys
3. **Balance with performance**: Be willing to denormalize when performance requires it
4. **Document your decisions**: Keep track of where and why you've denormalized
5. **Use views**: Create views that join normalized tables to simplify queries
6. **Consider database capabilities**: Modern databases have optimization features that can mitigate some normalization penalties

## 🔑 Keys and Indexes

### Primary Keys
A primary key uniquely identifies each record in a table.

Characteristics:
- Must contain UNIQUE values
- Cannot contain NULL values
- Should be immutable (rarely changed)
- Can be a single column or multiple columns (composite key)

Example:
```sql
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100) UNIQUE
);
```

### Foreign Keys
A foreign key is a field in one table that refers to the primary key in another table.

Example:
```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
```

### Indexes
Indexes improve the speed of data retrieval operations but may slow down data insertion and updates.

Types of indexes:
- **Single-column indexes**
- **Composite indexes** (multiple columns)
- **Unique indexes** (no duplicate values)
- **Clustered indexes** (determines physical order of data)
- **Non-clustered indexes** (logical ordering using pointers)

Example:
```sql
-- Creating a simple index
CREATE INDEX idx_lastname
ON Customers(LastName);

-- Creating a composite index
CREATE INDEX idx_name
ON Customers(LastName, FirstName);

-- Creating a unique index
CREATE UNIQUE INDEX idx_email
ON Customers(Email);
```

### When to use indexes:
- Columns used frequently in WHERE clauses
- Columns used in JOIN conditions
- Columns used in ORDER BY or GROUP BY clauses

## 🔗 Relationships

Relationships define how tables are connected to each other.

### Types of Relationships

#### 1. One-to-One (1:1)
One record in the first table is related to exactly one record in the second table.

Example: One person has one passport number.

```sql
CREATE TABLE Person (
    PersonID INT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Passport (
    PassportID INT PRIMARY KEY,
    PersonID INT UNIQUE,
    PassportNumber VARCHAR(20),
    ExpiryDate DATE,
    FOREIGN KEY (PersonID) REFERENCES Person(PersonID)
);
```

#### 2. One-to-Many (1:N)
One record in the first table is related to multiple records in the second table.

Example: One customer can place many orders.

```sql
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
```

#### 3. Many-to-Many (M:N)
Multiple records in the first table are related to multiple records in the second table.

Example: Students and courses (a student can take many courses, and a course can have many students).

```sql
CREATE TABLE Students (
    StudentID INT PRIMARY KEY,
    Name VARCHAR(100)
);

CREATE TABLE Courses (
    CourseID INT PRIMARY KEY,
    Title VARCHAR(100)
);

CREATE TABLE Enrollments (
    EnrollmentID INT PRIMARY KEY,
    StudentID INT,
    CourseID INT,
    EnrollmentDate DATE,
    FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);
```

## 📦 Stored Procedures

Stored procedures are prepared SQL code that you can save and reuse, similar to functions in programming languages. They're powerful database objects that are compiled once and stored in the database for repeated execution.

### Key Benefits of Stored Procedures

- **Performance**: Stored procedures are compiled once and cached in memory, making execution faster
- **Security**: Grant execution permissions without giving access to underlying tables
- **Reduced network traffic**: Send a single call instead of multiple SQL statements
- **Modularity**: Encapsulate complex logic and maintain it in one place
- **Consistency**: Enforce consistent business rules across applications

### Creating a Stored Procedure

The basic syntax varies slightly between different database systems:

**MySQL/MariaDB:**
```sql
DELIMITER //
CREATE PROCEDURE GetCustomerOrders(IN customerID INT)
BEGIN
    SELECT 
        o.OrderID, 
        o.OrderDate, 
        o.TotalAmount,
        oi.ProductID,
        p.ProductName,
        oi.Quantity,
        oi.UnitPrice
    FROM 
        Orders o
    JOIN 
        OrderItems oi ON o.OrderID = oi.OrderID
    JOIN 
        Products p ON oi.ProductID = p.ProductID
    WHERE 
        o.CustomerID = customerID
    ORDER BY 
        o.OrderDate DESC;
END //
DELIMITER ;
```

**SQL Server:**
```sql
CREATE PROCEDURE GetCustomerOrders
    @CustomerID INT
AS
BEGIN
    SELECT 
        o.OrderID, 
        o.OrderDate, 
        o.TotalAmount,
        oi.ProductID,
        p.ProductName,
        oi.Quantity,
        oi.UnitPrice
    FROM 
        Orders o
    JOIN 
        OrderItems oi ON o.OrderID = oi.OrderID
    JOIN 
        Products p ON oi.ProductID = p.ProductID
    WHERE 
        o.CustomerID = @CustomerID
    ORDER BY 
        o.OrderDate DESC;
END;
```

**PostgreSQL:**
```sql
CREATE OR REPLACE PROCEDURE get_customer_orders(customer_id INT)
LANGUAGE plpgsql
AS $
BEGIN
    SELECT 
        o.order_id, 
        o.order_date, 
        o.total_amount,
        oi.product_id,
        p.product_name,
        oi.quantity,
        oi.unit_price
    FROM 
        orders o
    JOIN 
        order_items oi ON o.order_id = oi.order_id
    JOIN 
        products p ON oi.product_id = p.product_id
    WHERE 
        o.customer_id = customer_id
    ORDER BY 
        o.order_date DESC;
END;
$;
```

### Executing a Stored Procedure

Calling a stored procedure is straightforward:

```sql
-- MySQL, SQL Server
CALL GetCustomerOrders(101);

-- PostgreSQL
CALL get_customer_orders(101);
```

### Input and Output Parameters

Stored procedures can accept input parameters and return output parameters:

```sql
DELIMITER //
CREATE PROCEDURE CalculateOrderTotal(
    IN orderID INT, 
    OUT totalAmount DECIMAL(10,2)
)
BEGIN
    SELECT SUM(Quantity * UnitPrice) 
    INTO totalAmount
    FROM OrderItems 
    WHERE OrderID = orderID;
END //
DELIMITER ;
```

Executing with output parameter:

```sql
-- MySQL
SET @total = 0;
CALL CalculateOrderTotal(1001, @total);
SELECT @total AS OrderTotal;

-- SQL Server
DECLARE @total DECIMAL(10,2);
EXEC CalculateOrderTotal @OrderID = 1001, @TotalAmount = @total OUTPUT;
SELECT @total AS OrderTotal;
```

### Control Flow in Stored Procedures

Stored procedures support programming constructs like variables, conditionals, and loops:

```sql
DELIMITER //
CREATE PROCEDURE UpdateInventory(IN orderID INT)
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE prod_id INT;
    DECLARE qty INT;
    
    -- Declare cursor to iterate through order items
    DECLARE cur CURSOR FOR 
        SELECT ProductID, Quantity FROM OrderItems WHERE OrderID = orderID;
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;
    
    -- Start transaction for data consistency
    START TRANSACTION;
    
    OPEN cur;
    
    read_loop: LOOP
        FETCH cur INTO prod_id, qty;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        -- Update inventory
        UPDATE Products 
        SET StockQuantity = StockQuantity - qty 
        WHERE ProductID = prod_id;
        
        -- Check if stock goes negative
        IF (SELECT StockQuantity FROM Products WHERE ProductID = prod_id) < 0 THEN
            ROLLBACK;
            SELECT CONCAT('Error: Not enough stock for product ID ', prod_id) AS Message;
            CLOSE cur;
            LEAVE read_loop;
        END IF;
    END LOOP;
    
    CLOSE cur;
    COMMIT;
    
    SELECT 'Inventory updated successfully' AS Message;
END //
DELIMITER ;
```

### Error Handling in Stored Procedures

Proper error handling is crucial for robust stored procedures:

```sql
DELIMITER //
CREATE PROCEDURE SafeUpdateCustomer(
    IN custID INT,
    IN newEmail VARCHAR(100)
)
BEGIN
    -- Declare handler for exceptions
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SELECT 'An error occurred, update cancelled' AS Message;
    END;
    
    -- Start transaction
    START TRANSACTION;
    
    -- Check if customer exists
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE CustomerID = custID) THEN
        SELECT 'Customer not found' AS Message;
        ROLLBACK;
    ELSE
        -- Validate email format (simple check)
        IF newEmail NOT LIKE '%_@_%.__%' THEN
            SELECT 'Invalid email format' AS Message;
            ROLLBACK;
        ELSE
            -- Perform update
            UPDATE Customers SET Email = newEmail WHERE CustomerID = custID;
            COMMIT;
            SELECT 'Customer updated successfully' AS Message;
        END IF;
    END IF;
END //
DELIMITER ;
```

### Advantages in Real-World Scenarios

1. **Complex Business Logic**: 
   Implement multi-step processes like order processing, which might involve:
   - Checking inventory availability
   - Calculating totals with discounts
   - Updating inventory
   - Recording the transaction
   - Generating notifications

2. **Data-Intensive Operations**:
   Perform complex calculations or data transformations where moving data to the application layer would be inefficient.

3. **Scheduled Tasks**:
   When combined with database schedulers, stored procedures can perform regular maintenance or data processing tasks.

4. **API Integration**:
   Provide a consistent API for different applications to interact with the database.

### Stored Procedures vs. User-Defined Functions

While similar, there are key differences:

**Stored Procedures:**
- Can perform multiple operations and return multiple result sets
- Used for actions (INSERT, UPDATE, DELETE)
- Can use transactions
- Cannot be used in SELECT statements

**User-Defined Functions:**
- Must return a value
- Cannot perform actions that modify the database
- Can be used within SQL statements
- More limited in functionality

### Best Practices

1. **Use meaningful names** that indicate what the procedure does
2. **Comment your code** for future maintenance
3. **Handle errors** gracefully with appropriate feedback
4. **Include input validation** to prevent bad data
5. **Use transactions** for operations that need to be atomic
6. **Limit scope** - procedures should do one thing well
7. **Consider performance** for frequently executed procedures
8. **Control access** through appropriate permissions
9. **Maintain versioning** when updating procedures

## 💼 Transactions

Transactions ensure that a series of SQL operations are treated as a single unit of work that either completes entirely or not at all.

### ACID Properties

- **Atomicity**: All operations complete successfully or none do
- **Consistency**: Database remains in a consistent state before and after transaction
- **Isolation**: Transactions operate independently of one another
- **Durability**: Completed transactions persist even during system failures

### Transaction Control Commands

- **BEGIN or START TRANSACTION**: Starts a new transaction
- **COMMIT**: Saves changes permanently
- **ROLLBACK**: Undoes changes
- **SAVEPOINT**: Creates points within a transaction to which you can roll back
- **SET TRANSACTION**: Changes transaction characteristics

### Example: Fund Transfer Between Accounts

```sql
START TRANSACTION;

-- Deduct from account A
UPDATE Accounts
SET Balance = Balance - 500
WHERE AccountID = 'A';

-- Add to account B
UPDATE Accounts
SET Balance = Balance + 500
WHERE AccountID = 'B';

-- Check if both accounts have non-negative balance
IF EXISTS (SELECT * FROM Accounts WHERE AccountID IN ('A', 'B') AND Balance < 0) THEN
    ROLLBACK;
ELSE
    COMMIT;
END IF;
```

## 🛒 Real-World Application: E-commerce Database

Let's design a simplified e-commerce database system to illustrate the concepts.

### Table Structure

```sql
-- Customers table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Phone VARCHAR(20),
    RegistrationDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Addresses table (normalized from Customers)
CREATE TABLE Addresses (
    AddressID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    AddressType VARCHAR(20) NOT NULL, -- 'Billing' or 'Shipping'
    Street VARCHAR(100) NOT NULL,
    City VARCHAR(50) NOT NULL,
    State VARCHAR(50) NOT NULL,
    PostalCode VARCHAR(20) NOT NULL,
    Country VARCHAR(50) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Category VARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE
);

-- Orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ShippingAddressID INT NOT NULL,
    BillingAddressID INT NOT NULL,
    OrderStatus VARCHAR(20) NOT NULL DEFAULT 'Pending',
    TotalAmount DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (ShippingAddressID) REFERENCES Addresses(AddressID),
    FOREIGN KEY (BillingAddressID) REFERENCES Addresses(AddressID)
);

-- OrderItems table (many-to-many relationship between Orders and Products)
CREATE TABLE OrderItems (
    OrderItemID INT PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

-- Payment table
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY,
    OrderID INT NOT NULL,
    PaymentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    PaymentMethod VARCHAR(50) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Status VARCHAR(20) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
);
```

### Create Indexes

```sql
-- Improve query performance
CREATE INDEX idx_customer_email ON Customers(Email);
CREATE INDEX idx_product_category ON Products(Category);
CREATE INDEX idx_order_customer ON Orders(CustomerID);
CREATE INDEX idx_order_status ON Orders(OrderStatus);
CREATE INDEX idx_orderitems_order ON OrderItems(OrderID);
CREATE INDEX idx_orderitems_product ON OrderItems(ProductID);
```

### Example Queries

1. Find all customers who have placed orders above $500:

```sql
SELECT 
    c.CustomerID,
    c.FirstName,
    c.LastName,
    COUNT(o.OrderID) AS OrderCount,
    SUM(o.TotalAmount) AS TotalSpent
FROM 
    Customers c
JOIN 
    Orders o ON c.CustomerID = o.CustomerID
GROUP BY 
    c.CustomerID, c.FirstName, c.LastName
HAVING 
    SUM(o.TotalAmount) > 500
ORDER BY 
    TotalSpent DESC;
```

2. Find the top 5 best-selling products:

```sql
SELECT 
    p.ProductID,
    p.Name,
    SUM(oi.Quantity) AS TotalSold,
    SUM(oi.Quantity * oi.UnitPrice) AS Revenue
FROM 
    Products p
JOIN 
    OrderItems oi ON p.ProductID = oi.ProductID
JOIN 
    Orders o ON oi.OrderID = o.OrderID
WHERE 
    o.OrderStatus = 'Completed'
GROUP BY 
    p.ProductID, p.Name
ORDER BY 
    TotalSold DESC
LIMIT 5;
```

3. Find customers who haven't placed an order in the last 6 months:

```sql
SELECT 
    c.CustomerID,
    c.FirstName,
    c.LastName,
    c.Email,
    MAX(o.OrderDate) AS LastOrderDate
FROM 
    Customers c
LEFT JOIN 
    Orders o ON c.CustomerID = o.CustomerID
GROUP BY 
    c.CustomerID, c.FirstName, c.LastName, c.Email
HAVING 
    MAX(o.OrderDate) < DATE_SUB(CURRENT_DATE, INTERVAL 6 MONTH)
    OR MAX(o.OrderDate) IS NULL
ORDER BY 
    LastOrderDate;
```

### Create a Trigger

Create a trigger to update product stock when an order is placed:

```sql
DELIMITER //
CREATE TRIGGER tr_UpdateStock
AFTER INSERT ON OrderItems
FOR EACH ROW
BEGIN
    UPDATE Products
    SET StockQuantity = StockQuantity - NEW.Quantity
    WHERE ProductID = NEW.ProductID;
    
    -- Check if stock is low and log it
    IF (SELECT StockQuantity FROM Products WHERE ProductID = NEW.ProductID) < 10 THEN
        INSERT INTO LowStockAlerts (ProductID, RemainingStock, AlertDate)
        VALUES (NEW.ProductID, (SELECT StockQuantity FROM Products WHERE ProductID = NEW.ProductID), NOW());
    END IF;
END//
DELIMITER ;
```

### Create a Transaction

Place an order with multiple items using a transaction:

```sql
START TRANSACTION;

-- Insert order
INSERT INTO Orders (CustomerID, ShippingAddressID, BillingAddressID, OrderStatus, TotalAmount)
VALUES (101, 203, 204, 'Pending', 650.75);

-- Get the OrderID
SET @OrderID = LAST_INSERT_ID();

-- Insert order items
INSERT INTO OrderItems (OrderID, ProductID, Quantity, UnitPrice)
VALUES 
    (@OrderID, 1, 2, 199.99),
    (@OrderID, 3, 1, 149.99),
    (@OrderID, 7, 3, 33.59);

-- Process payment
INSERT INTO Payments (OrderID, PaymentMethod, Amount, Status)
VALUES (@OrderID, 'Credit Card', 650.75, 'Completed');

-- Commit if everything is successful
COMMIT;
```

## 📈 Conclusion

This guide covered the fundamental concepts of relational databases and SQL, including:

- Table structure with rows and columns
- SQL query components
- Different types of joins
- Normalization rules
- Keys and indexes
- Database relationships
- Stored procedures for encapsulating logic
- Triggers for automating responses to events
- Transactions for ensuring data integrity

By understanding these concepts and applying them in practice, you can design efficient, normalized database schemas and write effective SQL queries to manage and retrieve data according to your application's needs.

Remember that database design is both an art and a science—while following normalization rules and best practices is important, you should always consider the specific requirements and performance needs of your application when making design decisions.
