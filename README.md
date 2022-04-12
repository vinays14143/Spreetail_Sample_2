# Spreetail_Sample_2
# App Overview

A basic console app built on .NETCore 5.0.

# Build

`dotnet build`

# Run

`dotnet run`

# Program

- Console app which uses Microsoft hosting extension package for Hosting and startup infrastructures for applications 
which creates singleton instance of console application.
- Contains CommandManager which manages various operations on dictionary.
- Read and Write MultiValues dictionary Service for read and write operation implementation for dictionary.
- Uses Microsoft Extension DependencyInjection for DI of Read and Write MultiValued Dictionary Services.
- Helper called ValidateInput class does the validation on input request.
- Uses Facade pattern hides the complexities of the read and write operations and provides an interface to the client using 
which the client can access all the operations.

# Test
Unit test for some of the read and write operations using Nunit and Moq unit testing packages.
