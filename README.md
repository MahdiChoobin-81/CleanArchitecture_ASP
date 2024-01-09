
# Technologies used in the Project
  **- Clean Architecture**
  
  **- Value Objects (Domain Driven Design)**
  
  **- CQRS Pattern**
  
  **- using Result class Insteed of Throwing Exceptions.(FluentResult library)**
  
   **- Repository And Unit Of Work Patterns**
   
   **- Dependency Injection**
   
   **- Solid Principle's : Single-Responsibility**
   
   **- OOP concepts: Inheritance, Abstraction**
   
   **- Custom Generic Classes.**
   
   **- Migrations**
   
   **- Record**
   
   **- Dto**
   
   **- Enum**


  

# Project Capabilities
  **- CRUD Operation.**
  
  **- Database Relationships : Many_to_Many And One_to_Many.**
  
  **- All Endpoint's, CQRS structure and Repositories(Except Remove method) Are Asynchronous.**
  

  # Program rules
  ### Unique Fields :
    - Username
    - Email
    - MovieName
    - GenreName
    - ActorName
    - LanguageName
    - CountryName
    
  ### Fields Constraints :
  
  > [!NOTE]
  > All of Them are Non-nullable.

    - FullName Max Length = 50.
    - Password Max Length = 30 | Min Length = 8.
    - FullName Max Length = 50.
    - CountryName Max Length = 60.
    - GenreName Max Length = 50.
    - LanguageName Max Length = 50.
    - Email Max Length = 100 | It must be in Email Format.
    - MovieDescription Max Length = 800.
    - MovieName Max Length = 120.
    - MovieRate(type : byte) Max Length = 5.
    - ReleaseDate(type : DateTime) = Cannot be more Than Current Time.
    - Image(type : byte) Max Length = 120 | It must have one of the extensions jpg, png or jpeg.

    
    
