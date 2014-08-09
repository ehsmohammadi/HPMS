cd packages\FluentMigrator.1.1.1.0\tools
migrate -a ..\..\..\MITD.PMSAdmin.Persistence.NH\bin\Debug\MITD.PMSAdmin.Persistence.NH.dll -connection "data source=.\sqlexpress;initial catalog=PMSDB;integrated security=True;multipleactiveresultsets=True;" -db SqlServer2008 -t=rollback -version %1 
cd ..\..\..\