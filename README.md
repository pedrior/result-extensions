[![Build Workflow Status](https://img.shields.io/github/actions/workflow/status/pedrior/result-extensions/build.yml?label=build)](https://github.com/pedrior/result-extensions/actions/workflows/build.yml)

# Result Extensions

A extremely simple library providing a discriminated union type powered with fluent extensions for .NET.

## Introduction

The Result pattern is a functional programming approach used to represent the outcome of a computation that can either
succeed with a value or fail with one or more errors. This can help make error handling more explicit and prevent
unexpected runtime errors. This project offers a simple discriminated union type for .NET, powered with fluent 
extension functions and support for ASP.NET Core's `IResult` and `IActionResult` response objects.

### What issue is this project attempting to address?

When talking about error handling in .NET, the first thing that comes to mind is exceptions. Exceptions are a powerful
mechanism for handling errors. When an exception is thrown, the method's execution is interrupted, and the runtime
searches for a catch block that can handle the exception. This approach, known as the fast-fail principle, proves
beneficial in numerous scenarios. However, exceptions have some drawbacks:

- __Performance__: Throwing and catching exceptions can incur a significant performance cost, as it involves stack
  unwinding, context switching, and memory allocation. This can be problematic in performance-critical scenarios.

- __Complexity__: Exceptions can disrupt the normal flow of control in your application, making the code harder to read
and understand. They can lead to non-linear code execution paths, which may complicate debugging and maintenance.

- __Overuse__: Over-reliance on exceptions for control flow or error handling can lead to poorly structured code. This
  can make it harder to reason about the code and can result in unexpected behavior.

- __Implicit__: Exceptions are not part of a method's signature, meaning that the caller must be aware of the exceptions
  that a method can throw.

- __Testing__: Testing code that throws exceptions can be challenging, as you need to set up the test environment to
  trigger or bypass the exception. This can make it harder to write unit tests and verify the behavior of the code.

The Result pattern is an alternative approach to error handling that addresses some of these issues. Instead of throwing
exceptions, a method returns a result object that encapsulates the outcome of the operation. This result object can
represent either a successful result or an error, along with additional information about the error. By making the error
explicit, the Result pattern can help improve code performance, readability, maintainability, and testability.

This doesn't mean we should replace exceptions entirely. Exceptions should be used for exceptional cases, not as a 
regular part of program execution.

## Getting Started

Just add the package to begin using it:

```shell
dotnet add package ResultExtensions
```

You might want to consider adding the ASP.NET Core extensions package as well:

```shell
dotnet add package ResultExtensions.AspNetCore
```

If you are using [FluentAssertions](https://github.com/fluentassertions/fluentassertions) to write tests, we offer a package with specialized assertions:

```shell
dotnet add package ResultExtensions.FluentAssertions
```

Now that the necessary packages have been added, let's explore the capabilities of the ResultExtensions.

## Features

Let's explore some of the features provided by the ResultExtensions library.

### Basic usage

#### Returns either a value or an error

```csharp
public Result<int> Divide(int dividend, int divisor)
{
    if (divisor is 0)
    {
        return Error.Failure("Cannot divide by zero.");
    }

    return dividend / divisor;
}
```

#### Returns either a value or multiple errors

```csharp
public Result<int> Validate(int value)
{
    var errors = new List<Error>();

    if (value < 0)
    {
        errors.Add(Error.Validation("Value must be greater than or equal to zero."));
    }

    if (value % 2 is not 0)
    {
        errors.Add(Error.Validation("Value must be an even number."));
    }

    return errors.Length is 0 ? value : errors;
}
```

### Creating a `Result<T>`

#### From implicit conversions

##### To a successful result

```csharp
Result<string> r1 = "Hello, World!";
Result<Point> r2 = new Point(10, 20);
```

##### To a failed result

```csharp
Result<string> r1 = Error.Validation("Must start with a letter.");
Result<string> r2 = new[] 
{ 
    Error.Validation("Must start with a letter."),
    Error.Validation("Must be at least 8 characters long.")
};
```

#### From static factory methods

##### To a successful result

```csharp
var r1 = Result<int>.Success(42);
var r2 = Result<Point>.Success(new Point(10, 20));
```

##### To a failed result

```csharp
var r1 = Result<string>.Failure(Error.Validation("Must start with a letter."));

var r2 = Result<string>.Failure(
    Error.Validation("Must start with a letter."),
    Error.Validation("Must be at least 8 characters long."));

var r3 = Result<string>.Failure(new[]
{
    Error.Validation("Must start with a letter."),
    Error.Validation("Must be at least 8 characters long.")
});

var r4 = Result<string>.Failure(new List<Error>
{
    Error.Validation("Must start with a letter."),
    Error.Validation("Must be at least 8 characters long.")
});

var r5 = Result<string>.Failure(ImmutableArray.Create(
    Error.Validation("Must start with a letter."),
    Error.Validation("Must be at least 8 characters long.")));
```

### Handling the result

#### Using the `Switch` method

The `Switch` method allows you to specify separate actions for the success and failure cases.

```csharp
Result<int> Process(int x)
{ ... }

Process(5).Switch(
    onSuccess: value => Console.WriteLine($"Success: {value}"),
    onFailure: error => Console.WriteLine($"Failure: {error}"));
```

#### Using the `Match` method

The `Match` method allows you to specify separate actions for the success and failure cases and return a new value.

```csharp
Result<int> Process(int x)
{ ... }

var str = Process(5).Match<string>(
    onSuccess: value => value.ToString(),
    onFailure: error => error.Message);
```

> Both `Switch` and `Match` methods have a `*All` variant that allows you to capture all errors in the failure case.

:construction: Work in progress...

## Documentation

All public types and members are documented. The documentation can be found in the source code.

## Inspired By

- [amantinband/error-or](https://github.com/amantinband/error-or)
- [ardalis/Result](https://github.com/ardalis/Result)
