# API-Gateway

/api-crm/products
/api-crm/products/{id}
.....
/api-crm/campaigns/{id}
....
/api-crm/quotes



Presentation
    ^
    |---------> Authorization / Logs / Error Handling
    |
    v
Business Logic - Gateway (Products, Campaigns, PBs, etc,)
    ^
    |
    | <---> Services <=HTTP/GET=> Backing Services(Products, PBs, etc)
    |                                /products
    |                                /products/{id}
    |
    V
Database Layer (Does not exists)
    ^
    X
    v
FILE:JSON // MYSQL // OTHER