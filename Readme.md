### Description:
---

This is a service for [VolgaCTF 2016 Finals](https://volgactf.ru/) competition.

Backend rest-like web api application, based on [.NET Core](https://www.microsoft.com/net/core), [ASP.NET Core](http://www.asp.net/core) and [EntityFramework Core](https://docs.efproject.net/en/latest/) platfolms.
On build application assembled as self-contained for specified OS platform.

For storing application data used [PostgreSQL](https://www.postgresql.org/) 9.4 database.

### Functionality:
---

Application emulates a server side for storing viltual file system.
All actions like register user, login, add folder to user's filesystem, add file, etc served as HTTP POST/GET requests.

The task of competitors is research app functionality and discover three weakneses that lead to vulnerabiluty, that allow for any registered user reads files contents on another users.

### Set up:
---
- install dotnet core for you paltform
- install postgresql 9.4
- createuser -d -s -P cuser (password for cuser: cloudCloud)
- createdb -O cuser cloudbase
- git clone
- dotnet restore
- dotnet run
