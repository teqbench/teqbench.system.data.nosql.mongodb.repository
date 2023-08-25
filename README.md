# Overview
Generic MongoDB repository implementation and supporting configuration classes.

Implementation based on https://medium.com/@marekzyla95/mongo-repository-pattern-700986454a0e

Distributed as NuGet Package (locally); currently, not published to NuGet (because would be public and do not want that) and do not have a private NuGet server setup.

## NuGet
### Local 
- Build
	- macOS
		- From Visual Studio, right mouse click the project and select 'Pack'
		- From Terminal, ...	
	- Windows
- Publish
	- macOS
		- Must have NuGet CLI downloaded/installed and path updated.
		- From command prompt in the project's root directory, publish to local NuGet repo folder when package has changed: `> nuget add ./bin/{Debug|Release}/TradingToolbox.System.Data.NoSql.MongoDB.Repository.{version}.nupkg -source ../../NuGet-Packages`
- Push to source control to share with other engineers since do not have a private host to act as central repo.
	- Use 'feature' branches to share in-progress library packages; when ready, merge to 'main' branch.

