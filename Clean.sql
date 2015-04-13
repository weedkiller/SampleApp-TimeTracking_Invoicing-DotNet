truncate table OAuthTokens

select * from OAuthTokens
select * from Employee
truncate table Employee
select * from TimeActivity

UPDATE Employee
SET GivenName='Mouse', FamilyName='book'
WHERE Id='1';

UPDATE TimeActivity
SET Invoice_QboId=2
WHERE QboId=79;
