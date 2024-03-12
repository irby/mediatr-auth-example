POST http://{{api_domain}}/api/auth/login
{
    "username": "user",
    "password": "pass"
}
HTTP 200
[Captures]
access_token_user1: cookie "X-Access-Token"

POST http://{{api_domain}}/api/auth/login
{
    "username": "foo",
    "password": "bar"
}
HTTP 200
[Captures]
access_token_user2: cookie "X-Access-Token"

POST http://{{api_domain}}/api/auth/login
{
    "username": "admin",
    "password": "pass"
}
HTTP 200
[Captures]
access_token_admin: cookie "X-Access-Token"

PUT http://{{api_domain}}/api/users/8391955b-6796-4623-8392-a30c69ae45ae/role
Authorization: Bearer {{access_token_user1}}
{
    "role": 1
}
HTTP 403

# Should not be able to update own role
PUT http://{{api_domain}}/api/users/8391955b-6796-4623-8392-a30c69ae45ad/role
Authorization: Bearer {{access_token_admin}}
{
    "role": 1
}
HTTP 400

PUT http://{{api_domain}}/api/users/8391955b-6796-4623-8392-a30c69ae45ae/role
Authorization: Bearer {{access_token_admin}}
{
    "role": 1
}
HTTP 200

PUT http://{{api_domain}}/api/users/8391955b-6796-4623-8392-a30c69ae45ae/role
Authorization: Bearer {{access_token_admin}}
{
    "role": 0
}
HTTP 200
