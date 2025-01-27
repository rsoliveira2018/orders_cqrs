# orders_cqrs
Web API accessing a relational database for storing data while managing a NoSQL database for reading data using CQRS.

# lack of frontend - how to deal with the request to validate functionality
For creating orders it is necessary to inform the customer id through the route while passing a list of product ids, quantity and unit price.
Please, check Swagger for better understanding on how to make the requests. The swagger.json file can also be found in the repository "swagger" folder. Copy its content and place it in the https://editor.swagger.io/ for some preview before running the project.

# examples
- Get Customers endpoint:
curl -X 'GET' \
  'https://editor.swagger.io/Customers' \
  -H 'accept: text/plain'

- Get Products endpoint:
curl -X 'GET' \
  'https://editor.swagger.io/Products' \
  -H 'accept: text/plain'

- Get Orders endpoint (for an order of Id '10'):
curl -X 'GET' \
  'https://editor.swagger.io/Orders/10' \
  -H 'accept: text/plain'

- Get OrderItems endpoint:
curl -X 'GET' \
  'https://editor.swagger.io/OrderItems' \
  -H 'accept: text/plain'

- The most relevant order creation:
curl -X 'POST' \
  'https://editor.swagger.io/Orders?customerId=100' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '[
  {
    "productId": 21,
    "quantity": 3,
    "unitPrice": 299.99
  },
  {
    "productId": 17,
    "quantity": 2,
    "unitPrice": 159.90
  }
]'
