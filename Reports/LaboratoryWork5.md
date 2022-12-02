# Topic: Web Authentication & Authorisation.

### Course: Cryptography & Security
### Author: Florescu Victor

----
&ensp;&ensp;&ensp; Authentication & authorization are 2 of the main security goals of IT systems and should not be used interchangibly. Simply put, during authentication the system verifies the identity of a user or service, and during authorization the system checks the access rights, optionally based on a given user role.

&ensp;&ensp;&ensp; There are multiple types of authentication based on the implementation mechanism or the data provided by the user. Some usual ones would be the following:
- Based on credentials (Username/Password);
- Multi-Factor Authentication (2FA, MFA);
- Based on digital certificates;
- Based on biometrics;
- Based on tokens.

&ensp;&ensp;&ensp; Regarding authorization, the most popular mechanisms are the following:
- Role Based Access Control (RBAC): Base on the role of a user;
- Attribute Based Access Control (ABAC): Based on a characteristic/attribute of a user.

### What is JSON Web Token?

&ensp;&ensp;&ensp; JSON Web Token (JWT) is an open standard (RFC 7519) that defines a compact and self-contained way for securely transmitting information 
between parties as a JSON object. This information can be verified and trusted because it is digitally signed. 
JWTs can be signed using a secret (with the HMAC algorithm) or a public/private key pair using RSA or ECDSA.

### What is the JSON Web Token structure?

&ensp;&ensp;&ensp; In its compact form, JSON Web Tokens consist of three parts separated by dots (.), which are:
<ol>
  <li>Header</li>
  <li>Payload</li>
  <li>Signature</li>
</ol>

# Header 

The header typically consists of two parts: the type of the token, which is JWT, and the signing algorithm being used, such as HMAC SHA256 or RSA.

# Payload 

The second part of the token is the payload, which contains the claims. 
Claims are statements about an entity (typically, the user) and additional data. There are three types of claims: registered, public, and private claims.

<ol>
  <li>Registered. These are a set of predefined claims which are not mandatory but recommended, to provide a set of useful, interoperable claims. Some of them are: iss (issuer), exp (expiration time), sub (subject), aud (audience), and others.</li>
  <li>Public. These can be defined at will by those using JWTs. But to avoid collisions they should be defined in the IANA JSON Web Token Registry or be defined as a URI that contains a collision resistant namespace.</li>
  <li>Private. These are the custom claims created to share information between parties that agree on using them and are neither registered or public claims.</li>
</ol>

# Signature

To create the signature part you have to take the encoded header, the encoded payload, a secret, the algorithm specified in the header, and sign that.
The signature is used to verify the message wasn't changed along the way, and, in the case of tokens signed with a private key, it can also verify that the sender of the JWT is who it says it is.

## Implementation description

&ensp;&ensp;&ensp; To complete the laboratory work, my responsibility was to implement authorization and authentication in a web application. To get things started, 
I had to [configure](#Configuration) my API to work with JWT Tokens and added some registered claims (issuer, audience, signing key). 

&ensp;&ensp;&ensp; When a user tries to log in (authenticate), he inserts his credentials (mail, password) which are checked in the database. If credentials are correct,
a new token is generated with the [Jwt service](#JwtService.GenerateToken) which is built using the registered claims and public claims of user (which are [generated](#JwtService.GenerateClaims)). 
Added claims are the email of the user, his unique identifier and his role. Everything is packed in a JWT token, encoded with HmacSha256 symmetric encryption algorithm and 
returned to the end-user. 

&ensp;&ensp;&ensp; After a successful login, I get a JWT token in the response body that I can use to make requests that require authorization. I attached a 
screenshot of a decoded token. Now I can make requests to some API endpoint's that are protected with the [Authorize] attribute. If I try to access them, I will get 
a Http 401 Unauthorized error. Now I can access authorized endpoints, but only specific to my own role as [Common get](#UsersController.CommonGet) has, but I cannot access 
[Admin get](#UsersController.AdminGet), because it is protected with the role "admin" which me as a user I do not possess. If I try to access it, I will get a Http 403 Forbidden
error. 'User' role is less priviled than 'admin' role, so in such a manner we can restrict which endpoints of the API a user can access.

# Configuration
```
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey!)),
            ValidateIssuerSigningKey = true
        };
    });
```

# JwtService.GenerateToken
```
var claims = GenerateClaims(user);
var token = new JwtSecurityToken(
    _jwtSettings.Issuer,
    _jwtSettings.Audience,
    signingCredentials: new SigningCredentials(
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SigningKey)),
        SecurityAlgorithms.HmacSha256),
    expires: DateTime.Now.AddHours(3),
    claims: claims
);

var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
return encodedToken;
```

# JwtService.GenerateClaims
```
var claims = new List<Claim>
{
    new(ClaimTypes.Email, user.Email),
    new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
    new(ClaimTypes.Role, user.Role.Name)
};

return claims;
```

# UsersController.AdminGet
```
[Route("all")]
[Authorize(Roles = "admin")]
public IActionResult GetAll()
{
    var allUsers = DummyDatabase.Users;
    return Ok(allUsers);
}
```

# UsersController.CommonGet
```
[HttpGet]
public IActionResult Get()
{
    var email = User.FindFirst(ClaimTypes.Email)?.Value;
    if (string.IsNullOrWhiteSpace(email))
        return BadRequest("Invalid token");

    var user = DummyDatabase.Users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

    return Ok(user);
}
```

## Screenshots

All requests are done via swagger. 

![image](https://user-images.githubusercontent.com/57410984/205298920-b3a18824-bd03-4f37-8407-82212ac734f2.png)

## Conclusions

In that laboratory work, I got familiar with authorization and authentication. 
