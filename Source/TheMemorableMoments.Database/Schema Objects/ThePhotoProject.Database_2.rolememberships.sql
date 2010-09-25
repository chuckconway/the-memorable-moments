EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'photogallery';


GO
EXECUTE sp_addrolemember @rolename = N'db_datawriter', @membername = N'photogallery';


GO
EXECUTE sp_addrolemember @rolename = N'db_datareader', @membername = N'photogallery';

