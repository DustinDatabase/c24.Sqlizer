## c24.Sqlizer

Tool that runs *.sql files against database. Use it for schema and/or data migration.

## Usage

	c24.Sqlizer.exe C:\MigrationScripts .\sqlexpress tests_database

Where:

- C:\MigrationScripts - directory with *.sql scripts
- .\sqlexpress - MS SQL server instance
- tests_database - database name
	
Additional(optional) parameters:

- /login - login for MS SQL server authentication
- /password - password for MS SQL server authentication
- /fileNamesPattern - regular expression to validate script file name
- /logDirectory - directory for logging output

Example:

	c24.Sqlizer.exe C:\MigrationScripts .\sqlexpress tests_database /login:sa /password:test1234 /logDirectory:C:\Logs
	
## Prerequisites

- sqlcmd.exe - command line interface for MS SQL server.
- script file name should start from a number. Example: 01\_script.sql
- script file name should have *.sql extension
- scripts directory should contain only *.sql scripts and no others
- scripts directory should have no subdirectories
- file scripts should be contiguous, without any gaps. 01\_script.sql and 03\_script.sql without 02\_script.sql won't work 