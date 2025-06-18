### Two different versions
- OnDemandFetching branch: meets all requirements as given in the problem document. Here, fetching from the main server is delayed until required
- master branch: meets only few requirements. But demonstrates active caching. i.e. the application caches keeps with itself the updated copy of server's data, and hence is able to readily serve to its clients. It implies the signature of the API's are different

### Application structure

There are three projects:
- ExternalUserService: It's a class library with: 
    - UserService: The public API for this application
    - ExternalUserHttpClient: implementing IExternalUserClient
- ExternalUserService.CompositionRoot: composition root is composed (various services are registered with DI container)
- Utils
    - caching utility
    - option pattern to minimize the use of Null's

### How to run?

Three sets of tests are there in the xUnit test library ExternalUserService.Tests2
- ExternalUserHttpClientTests
- UserServiceTests_Basic
- UserServiceTests_ServerDoesntExist

All these can be run from the visual studio's Test Runner. They verify all the aspects except caching.