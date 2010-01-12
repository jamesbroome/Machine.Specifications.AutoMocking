
Machine.Specifications.AutoMocking attempts to reduce noise in MSpec tests when creating mock objects, dependencies and the subject under test. 

It draws heavily (steals from) JP Boodhoo's developwithpassion.dll library for the abstractions around the mocking framework and the guts of the subject and dependency builders.

Currently supports Rhino.Mocks, but other frameworks should be coming...

Provides:

- An easy mechanism for creating and registering mock instances of the dependencies that the system under test requires (using RhinoMocks mocking framework).

 - A easy mechanism for creating mock objects for use in specifications (using RhinoMocks mocking framework).

 - Automatically creating and exposing an instance of system under test, with all registered dependencies, based on the 
specified interface (optional) and subject type.

 - ReSharper File Templates for specifications for subjects with and without a contract.