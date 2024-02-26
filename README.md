## Envirment variables
### Linux
In terminal set variables:
> export {key}="{value}"

> export SMTPServerUserName="{value}"
> export SMTPServerPassword="{value}"
> export DBPassword="{value}"

### Windows
In powershell terminal (or cmd)
> $Env:{key}="{value}"

> $Env:SMTPServerUserName="{value}"
> $Env:SMTPServerPassword="{value}"
> $Env:DBPassword="{value}"


## Launch project
### Linux
1. Open terminal & enter the following code:
> dotnet build
> dotnet run --project Api --urls=http://localhost:5000 | dotnet run --project Web --urls=http://localhost:6009

om hem in de background te runnen:
> ctrl+z
> bg

kill de background processen met:
> sudo lsof -i
> sudo kill -9 {PID}