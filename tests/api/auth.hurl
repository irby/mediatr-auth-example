POST http://{{api_domain}}/api/auth/login
{
    "username": "user",
    "password": "pass"
}
HTTP 200
[Captures]
access_token: cookie "X-Access-Token"

POST http://{{api_domain}}/api/auth/logout
Authorization: Bearer {{access_token}}
HTTP 200

POST http://{{api_domain}}/api/auth/logout
HTTP 401
