## implementation notes ##

1. Resource request samples  
Sample requests to test if authorization and authentication is working. Done in `SampleController`.

2. Authentication with credentials  
API - `POST /token/Issue`  
`ITokenIssuer` is the abstraction for issuing token.
`IdentityTokenIssuer` implements `ITokenIssuer` and uses .NET core identity to verify the user credentials.

3. Authentication with refresh token  
See 2
Verification of refresh token is done by database lookup.

4. Renew access token  
Renew and issue is done with the same workflow.
See 2

5. Access token generation  
`ITokenBuilder` is the abstraction for creating token and is implemented by `JwtTokenBuilder`. Output is a `Token` object that contains both JWT access token and crypto random refresh token.  
Configs for generating tokens are in `ITokenConfig`. A sample configuration is provided by `SampleTokenConfig`. We can have a implementation which reads the config from a configuration file or may be database.

6. Refresh token generation  
See 5  
The refresh tokens are stored in `RefreshToken` table. An entity model class with same name is there.

7. Encryption/decryption  
Encryption/decryption of JWT is done using `System.IdentityModel.Tokens.Jwt`. I created a static `CryptoRandomGenerator` class which is used to generate crypto random refresh token.    
`SampleTokenConfig` uses a symetric key. You can just replace it with asymatric key if you want. Asymatric key should be used if  your business service API is deployed separately from Authentication API.  
**Note: I just noticed that there is no abstraction for just creating the refresh token. You need to have a separate implementation of ITokenBuilder and have to reimplement JWT building too.**

8. Revoke refresh token  
My initial proposal was to revoke access token which does not make much sense. Revoke refresh token has been implemented.  
API - `GET /token/Revoke`

9. User management  
Done using Identity  
API - `/user/*`  
Implemented features:  
9.1 Fetch: `GET`  
9.2 Create: `POST`  
9.3 Delete: `DELETE`  
9.4 Update: `PUT`
9.5 Reset Password: `PUT /user/ResetPassword`  
9.6 Change Email: `PUT /user/ChangeEmail`  
9.6 Add roles: `PUT /user/AddRoles`  
9.6 Delete Roles: `PUT /user/DeleteRoles`  

10. Seed data  
See `Startup.Seed()`
