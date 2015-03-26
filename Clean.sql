truncate table OAuthTokens

select * from OAuthTokens
select * from Employee
truncate table Employee

UPDATE Employee
SET GivenName='William', FamilyName='Worfing'
WHERE Id='1';
