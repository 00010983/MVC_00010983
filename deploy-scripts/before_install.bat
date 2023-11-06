set FOLDER=%HOMEDRIVE%\inetpub\wwwroot

if exist %FOLDER% (
  rd /s /q "%FOLDER%"
)

mkdir %FOLDER%