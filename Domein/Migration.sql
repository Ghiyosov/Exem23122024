


create table Products(
    ProductId serial primary key,
    Name varchar(250),
    Description text,
    Price decimal,
    StockQuantity int,
    CategoryName varchar(250),
    CreatedDate TIMESTAMP
);