# MeetupApi
## Usage 
1. Set up MS SQL
2. Download and unzip MeetupApi 
3. In IdentityServer/appsetting.json
![image](https://github.com/GloomyFoodeater/MeetupApi/assets/46320298/e698a788-ca88-4f16-a45e-d0f15c2800cc)
   - Assign db connection string to "ConnectionStrings.DefaultConnection"
   - Ensure that "SecretKey" is assigned to some \<secret-key\>, i.e. "meetapapisecret"
5. Run identity server from terminal with urls to specify listening port 
```
.\IdentityServer.exe [--urls <identity-server-url>]
```
![image](https://github.com/GloomyFoodeater/MeetupApi/assets/46320298/e135b2b4-179b-44ce-80cf-f4b9af15ebc6)
5. In .\appsetting.json
   - Assign db connection string to "ConnectionStrings.DefaultConnection"
   - Ensure that "Audience" is "meetupApi"
   - Assign "Authority" to \<identity-server-url\>, i.e. "http://localhost:5000"
6. Run api from terminal with urls to specify listening port 
```
 .\MeetUpApi.exe [--urls <api-url>]
```
7. Make ```POST <identity-server-url>/connect/token``` with x-www-form-urlencoded body to obtain access token
![image](https://github.com/GloomyFoodeater/MeetupApi/assets/46320298/ca8840fa-7433-4e1a-910a-420260f748ab)
```
curl --location '<identity-server-url>/connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id=MeetupApi' \
--data-urlencode 'client_secret=\<secret-key\>' \
--data-urlencode 'grant_type=password' \
--data-urlencode 'username=\<user-name\>' \
--data-urlencode 'password=\<password\>'
```
Example of response
```
{"access_token":"eyJhbGciOiJSUzI1NiIsImtpZCI6IjI5OTAwRjYzMENBRDUxRTE0RUQ3QzIxMTU3RjBDNEM2IiwidHlwIjoiYXQrand0In0.eyJuYmYiOjE2OTk5NjY4NjcsImV4cCI6MTY5OTk3MDQ2NywiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MTAwIiwiYXVkIjoibWVldHVwQXBpIiwiY2xpZW50X2lkIjoiTWVldHVwQXBpIiwic3ViIjoiZWY5YWE4YzQtNjExOC00NDI5LWJhMmMtNTAwZWQ2MGVmNDFmIiwiYXV0aF90aW1lIjoxNjk5OTY2ODY3LCJpZHAiOiJsb2NhbCIsInJvbGUiOlsiYWRtaW4iLCJ1c2VyIl0sImp0aSI6IkVCMTA4NjA2M0JCMkY0NDQ0MDdFMkEwNUUwQTEzNDFBIiwiaWF0IjoxNjk5OTY2ODY3LCJzY29wZSI6WyJvcGVuaWQiLCJyb2xlcyJdLCJhbXIiOlsicHdkIl19.iSoy2wxclENaoV1IIZdaW0b5Dsd8ESyCAs__XU45ZNpj2ywNrDpFZ2QxQvykXY6pwJU9wV7a2QZzJfgUQbTqTZFuDXPuSdV6NvKIowPXKK_tXhWJegNTVTCMMCa7ru_RVOJC9rtHH0H-53qvBGmfMD2FHYPNdNjbyjWS7fd-UvTHrnNb6FoRPdkKX5nn9H2h1oSYDPXW45ELBgMwTI3HMu9q_219Zcqmcbtmshx8WXBie4ngKGPE2DEGziJdqUOio4mDWQv8vs19Q7c-aFqrEiEYgSBfQIhfxwRwK7pUlHF5HuK0NLEovy_92nJqIovJnKvVixuZRB9tlJpfJv4MTA","expires_in":3600,"token_type":"Bearer","scope":"openid roles"}
```

 List of \<user-name\> and \<password\> pairs can be obtained from Infrastructure.Services.IdentitySeeder

8. Make ```GET <api-url>/swagger/index.html``` to use api
9. Fill bearer field with access token
![image](https://github.com/GloomyFoodeater/MeetupApi/assets/46320298/2fcaadbd-d6b2-4f40-a4ce-9609913994fb)
11. Use api from swagger
![image](https://github.com/GloomyFoodeater/MeetupApi/assets/46320298/60486f5e-06e4-47f2-a68c-fce8bbe6d307)
> [!NOTE]
> All endpoints require role "user" and endpoints with side effects require role "admin"
