--Truncates 
truncate table OAuthTokens
truncate table Customer
truncate table Item
truncate table Employee
truncate table TimeActivity
--Select

select * from OAuthTokens
select * from Employee
select * from Customer
select * from Item
select * from TimeActivity

--Update
UPDATE Employee
SET GivenName='Mouse', FamilyName='book'
WHERE Id='1';

UPDATE TimeActivity
SET Invoice_QboId=2
WHERE QboId=79;

Alter Table Employee
Add Id_new Int Identity(1, 1)
Go

Alter Table Employee Drop Column ID
Go
Exec sp_rename 'Employee.Id_new', 'ID', 'Column'

--Insert temporary data for testing.

select * from TimeActivity where Invoice_QboId is not null