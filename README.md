# homebook-sdk
Developer SDK and CLI (dotnet tool) for HomeBook. Provides tooling to bootstrap, and prepare a local HomeBook development environment.

## install

install via dotnet tool

```
dotnet tool install -g homebook-sdk
```

this will install the `hbd` command globally.

## check

check your local environment for required dependencies like docker, dotnet, etc.

```
hbd check
```

## init

### database

```
hbd init database --type|-t [postgresql|mysql]
```

* --type|-t: specify the database type to initialize (postgresql, mysql)
