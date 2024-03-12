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

POST http://{{api_domain}}/api/posts
Authorization: Bearer {{access_token_user1}}
{
  "message": "Hello, World!"
}
HTTP 200
[Captures]
post_id: jsonpath "$['id']"

GET http://{{api_domain}}/api/posts/{{post_id}}
Authorization: Bearer {{access_token_user2}}
HTTP 200

DELETE http://{{api_domain}}/api/posts/{{post_id}}
Authorization: Bearer {{access_token_user2}}
HTTP 403

DELETE http://{{api_domain}}/api/posts/{{post_id}}
Authorization: Bearer {{access_token_user1}}
HTTP 200

GET http://{{api_domain}}/api/posts/{{post_id}}
Authorization: Bearer {{access_token_user2}}
HTTP 404


POST http://{{api_domain}}/api/posts
Authorization: Bearer {{access_token_user1}}
{
  "message": "Hello, World!"
}
HTTP 200
[Captures]
post_id: jsonpath "$['id']"

DELETE http://{{api_domain}}/api/posts/{{post_id}}
Authorization: Bearer {{access_token_admin}}
HTTP 200
