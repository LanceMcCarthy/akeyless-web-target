# Custom Dynamic Secrets Provider for AKeyless

This is a custom .NET 8 web API project to mock a dynamic secrets provider for my AKeyless [GitHub Action](https://github.com/LanceMcCarthy/akeyless-action) and my [Azure DevOps Task Extension](https://github.com/LanceMcCarthy/akeyless-extension-azdo).

- Production Host => [https://dynamicsecrets.dvlup.com/swagger/index.html](https://dynamicsecrets.dvlup.com/swagger/index.html) (running on RaspberryPi4 host!)
- Docker Hub => https://hub.docker.com/r/lancemccarthy/secretsmocker

> The code has all been written from scratch using AKeyless's Web Target documentation at https://docs.akeyless.io/docs/custom-producer by converting the expected API json requests/results into .NET class objects. Then manually writing each POST request in the controller by following the design of the API workflow. This should work as a Web Target Provider for anyone as I do not enforce authentication using the AKeyless access-id.

### Docker Deploy

If you want to run this in Linux instead of Windows, you have two quick options.

#### Option 1 - Docker you can spin up a container right now with the following command:

`docker run -d -p 8080:80 lancemccarthy/secretsmocker:latest`

#### Option 2 - Docker Compose

If you prefer, use docker compose. Create a docker-compose.yml file with the following content

```yml
version: '3.8'
services:
  app:
    image: 'lancemccarthy/secretsmocker:latest'
    restart: unless-stopped
    ports:
      - '8080:8080'
```

Then, in the same directory as the file, execute the following command:

`docker compose up -d`

## Runtime

Here's what the swagger page will present you with:

![image](https://github.com/LanceMcCarthy/akeyless-web-target/assets/3520532/1047f436-db1b-4537-affb-b54658796f87)
