## UserSecrets
Add usersecrest by doing the following steps:
1. Open powershell/terminal
1. Run the following code
> dotnet user-secrets init --project Api
> dotnet user-secrets init --project Web
1. Set the following variables in as usersecret entries (replace the {values}):
> dotnet user-secrets set "SMTPServer:UserName" "{Username}" --project Api
> dotnet user-secrets set "SMTPServer:Password" "{Password}" --project Api
> dotnet user-secrets set "DBPassword" "{Password}" --project Web


## Launch project
### Linux
1. Open terminal & enter the following code:
> dotnet build --configuration Release --project Api
2. Open Another terminal & enter the following code:
> dotnet build --configuration Release --project Web