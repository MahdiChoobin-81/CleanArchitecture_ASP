[API_Document_HTML2.zip](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/files/14321888/API_Document_HTML2.zip)
# Technologies used in the Project
  **- Clean Architecture.**
  
  **- Value Objects (Domain Driven Design).**
  
  **- CQRS Pattern.**
  
  **- using Result class Insteed of Throwing Exceptions.(FluentResult library).**
  
   **- Repository And Unit Of Work Patterns.**
   
   **- Solid Principle's : Single-Responsibility, Open–closed,  Liskov substitution, Interface segregation, Dependency Inversion(using Dependency Injection).**
   
   **- OOP concepts: Inheritance, Abstraction.**
   
   **- Custom Generic Classes.**
   
   **- Migrations.**
   
   **- Record.**
   
   **- Dto.**
   
   **- Extension Method.**
   
   **- Enum.**


  

# Project Capabilities
  **- CRUD Operation (Considering the Uniqueness of Domain fields in Add and Update operations Without Throwing Exception).**
  
  **- Database Relationships : Many_to_Many And One_to_Many.**
  
  **- All Endpoint's, CQRS structure and Repositories(Except Remove method) Are Asynchronous.**


# Diagrams :
  > [!NOTE]
  > I Built Sequence Diagram just for **User** Domain because it's _simple_ and _Good Idea_ to see how my code works, but The Mechanism of the **Movie** Domain is more _**complicated**_.

### Sequence Diagram's :

   [Create User](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/SequenceDiagrams/Images/sequenceDiagram_user_create.png)
   
   [Update User](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/SequenceDiagrams/Images/sequenceDiagram_user_update.png)
   
   [Delete User](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/SequenceDiagrams/Images/sequenceDiagram_user_delete.png)
   
   [Get User By Id](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/SequenceDiagrams/Images/sequenceDiagram_user_getById.png)
   
   [Get All User's](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/SequenceDiagrams/Images/sequenceDiagram_user_getAll.png)
    
### Class Diagram's :

   [Domain's](https://github.com/MahdiChoobin-81/CleanArchitecture_ASP/blob/master/Diagrams/ClassDiagrams/classDiagram_Domains.png)




# Custom tools I made

**- FindDependentTableRecordsOfMovieTable class :**
 its a generic class that takes list of guids(our table ids) and another input is an interface named **IRepository**(its a super class for my all repositories); this class will check that all ids are exists using inputs, foreach loop and another generic class named **FindEntityRecordById**(this class will make error message if there's no any record, with our ids) and in the end if we had no any error we will return all records content(wich we find them through ids) in **Result**(fluentResult library) format with success status. 

**- FindEntityRecordById class :**
its a generic class with two input, one of them is an id of type **Id**(its a _ValueObject_) and another one is an interface called **IRepository**, actually its a supper class for all repositories. this class will send a request to The relevant repository and if there's no any record with the id, our class will make an error message and returns it in **Result**(fluentResult library) format with failed status. Otherwise the record will be returned in **Result** format with success status.

**- ObjectConverter class :**
  IT IS also a generic class😊 that has a method named **Convert**, our method takes an object and will convert it to type parameter( _T_ ) of the class. Done

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

    
    
