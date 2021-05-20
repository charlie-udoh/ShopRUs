# ShopRUs
This is an implementation of the Habari Case Challenge for ShopRUs

# Requirements
- Ensure that you have an instance of SQL Server installed
- Modify the connection string "ShopRUs" in appsettings.json by providing your SQL Server Hostname, User Id and Password
Please note that the database will be created automatically once the app is run

# To run the API
Visual Studio: Click on the run button and the app will be started automatically via IIS Express
CLI: cd to project directory and run the command "dotnet run"

# Endpoints

CUSTOMERS  
GET /api/customer/ (Gets all customers)  
GET /api/customer/{id} (Gets customer by Id)  
GET /api/customer/name/{name} (Gets customer by Name)  
POST /api/customer/ (Create a customer)  
Customer structure {  
  Id : number  
  Name : string  
  Email : string  
  Address : string  
  PhoneNumber: string  
  Role: string  
  DateRegistered : datetime  
}  

DISCOUNTS  
GET /api/discount/ (Gets all discounts)  
GET /api/discount/{id} (Gets discount by Id)  
GET /api/discount/{type} (Gets discount by Type)  
POST /api/discount/ (Create a discount)  
Discount structure {  
  Id : number  
  DiscountAppliesTo : string  
  DiscountValueType : string  
  DiscountValue : number  
  Units: number  
}  

INVOICES  
GET /api/invoice/{id} (Gets invoice by Id)  
POST /api/invoice (Create an invoice)  
Invoice structure {  
  CustomerId : number  
  InvoiceId : number  
  TotalAmount : number  
  DiscountAmount : number  
  InvoiceTotal : number  
  Customer : string  
  ShippingAddress : string  
  InvoiceDate : datetime  
  Items : {  
    ItemId : string  
    ItemDescription : string  
    ItemType : string  
    Quantity : number  
    UnitPrice : number  
  }  
}  
