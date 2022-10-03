# Custom Dynamic Secrets Provider for AKeyless

This is a custom .NET 6 web API project to mock a dynamic secrets provider for my AKeyless [GitHub Action](https://github.com/LanceMcCarthy/akeyless-action) and my [Azure DevOps Task Extension](https://github.com/LanceMcCarthy/akeyless-extension-azdo).

- [Swagger](https://secretsmocker.azurewebsites.net/swagger/index.html)
- [AKeyless Sync API Controller](https://secretsmocker.azurewebsites.net/api/sync)

> The code has all been written from scratch using AKeyless's Web Target documentation at https://docs.akeyless.io/docs/custom-producer by converting the expected API json requests/results into .NET class objects. Then manually writing each POST request in the controller by following the design of the API workflow. This should work as a Web Target Provider for anyone as I do not enforce authentication using the AKeyless access-id.

