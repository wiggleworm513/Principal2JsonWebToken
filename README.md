# Principal2JsonWebToken
This is a library for creating a Json Web Token (JWT) using information from a Claims Principal.

This started due to a need to create a JWT based off Windows-Authentication, and not liking the complexity of existing solutions.

The library contains the following items:

1) AppHelper - This provides access to the user and to configuration values.
2) SecurityHelper - This uses the AppHelper to create a JWT using the user's name, set's if the user is authenticated (because we shouldn't just trust that the user inside of the token is authenticated), and an optional list of roles for the user.
3) Demo WebApp - This is where the Windows-Auth is used, and the SecurityHelper is invoked.
4) Demo WebApi - This is an end service that consumes the JWT, validates and allows for access to various functionality exposed through EndPoints.
