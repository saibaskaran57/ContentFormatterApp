# ContentFormatterApp

An application that provides different types of content-negotiation using .NET Core 2.0. Below are an example of request/response for different content formats(e.g. JSON,XML and MessagePack).

Request Headers
------------------------
`Content-Type:application/xml|json|x-msgpack`

`Accept:application/xml|json|x-msgpack`

XML Request body
------------------------
```
<?xml version="1.0"?>
<Request>
  <Id>1</Id>
</Request>
```

JSON Response body
------------------------
```
{
    "message": "OK"
}
```
